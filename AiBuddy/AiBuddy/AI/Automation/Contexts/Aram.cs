﻿namespace AiBuddy.AI.Automation.Contexts
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Security.Cryptography;

    using AiBuddy.AI.Logic;
    using AiBuddy.AI.Maps.HowlingAbyss.Shop;
    using AiBuddy.Champions;

    using EloBuddy;
    using EloBuddy.SDK;

    using TreeSharp;

    using Action = TreeSharp.Action;
    
    internal class Aram : GameRoutine
    {
        private ShopHandler shopHandler;

        public override void OnLoad()
        {
            Chat.Print(string.Format("<font color=\"#40c1ff\">[AIBuddy]:</font> <font color=\"#ffffff\">{0} loaded</font>", this.MapId));

            this.shopHandler = new ShopHandler();
            FindChampion.FindAndSetChampion();
            this.Rebuild();
            this.m_primaryBehaviour.Start(null);
        }

        public override void OnDraw()
        {
            // Draw build
            var yOffset = 0;
            foreach (var shopItem in this.shopHandler.Build)
            {
                var color = Item.HasItem(shopItem.Id) ? Color.Gray : Color.White;

                Drawing.DrawText(25, yOffset += 15, color, shopItem.Id.ToString());
            }

            // Draw vertices
            Navigation.WorldCell();
        }

        public override GameMapId MapId
        {
            get
            {
                return GameMapId.HowlingAbyss;
            }
        }

        private Composite m_primaryBehaviour;

        public override Composite PrimaryBehaviour
        {
            get
            {
                return this.m_primaryBehaviour;
            }
        }

        public override void Rebuild()
        {
            this.m_primaryBehaviour = new PrioritySelector(
                
                this.HealBehaviour(),
                this.ShopBehaviour(),
                this.FarmBehaviour(),
                this.MoveBehaviour());
        }

        public Composite ShopBehaviour()
        {
            return new Decorator(ret => Shop.CanShop/* && this.shopHandler.finishedShop()*/, new Action(
                ret =>
                    {
                        Console.WriteLine("Shopping");
                        this.shopHandler.OnTick();
                    }));
        }

        public Composite MoveBehaviour()
        {
            return new PrioritySelector(
                            new Decorator(
                                ret => Player.Instance.CanMove,
                                new Action(
                                    ret =>
                                    {
                                        Console.WriteLine("Moving to Nearest Ally or Minion");
                                        var ally =
                                            EntityManager.Heroes.Allies.Where(x => !x.IsDead && !x.IsInShopRange() && !x.IsMe)
                                                .OrderBy(x => x.ChampionsKilled)
                                                .FirstOrDefault();
                                        
                                        var minion =
                                            EntityManager.MinionsAndMonsters.AlliedMinions.OrderByDescending(
                                                x => x.Distance(ObjectManager.Get<Obj_HQ>().FirstOrDefault())).FirstOrDefault();

                                        if (ally != null)
                                        {
                                            Orbwalker.OrbwalkTo(ally.Position.Randomize());
                                            Console.WriteLine("Ally found, moving");
                                        }
                                        else if(minion != null)
                                        {
                                            var position =
                                                minion.Position.Extend(
                                                    ObjectManager.Get<Obj_HQ>().FirstOrDefault(),
                                                    250).To3D().Randomize();
                                            if(Navigation.CalculateSafety(position.ToNavMeshCell()) < NavigationSafety.Average)
                                            Orbwalker.OrbwalkTo(position);
                                            //Console.WriteLine("Minion found, moving");
                                            Console.WriteLine(minion.Position.Extend(ObjectManager.Get<Obj_HQ>().FirstOrDefault(), 250).To3D() +" vs " + minion.Position);
                                        }
                                        else
                                        {
                                            Console.WriteLine("No one found ;(");
                                        }
                                    })));
        }

        public Composite HealBehaviour()
        {
            var healingBuff =
                ObjectManager.Get<GameObject>()
                    .Where(o => o.IsEnemy && o.Name.ToLower().Contains("healingbuff"))
                    .OrderBy(o => Player.Instance.Position.Distance(o.Position))
                    .FirstOrDefault();
            return new PrioritySelector(
                new Decorator(
                    ret => Player.Instance.HealthPercent < 50 && Player.Instance.CanMove && healingBuff != null,
                    new Action(
                        ret =>
                            {
                                Console.WriteLine("Healing");
                                var enemyTurret =
                                    EntityManager.Turrets.Enemies.Where(x => !x.IsDead)
                                        .OrderBy(x => x.Distance(Player.Instance))
                                        .FirstOrDefault();

                                if ((enemyTurret != null && healingBuff != null) && healingBuff.Distance(enemyTurret) > enemyTurret.AttackRange * 1.9 && healingBuff.IsInRange(Player.Instance, 1300))
                                {
                                    Player.IssueOrder(GameObjectOrder.MoveTo, healingBuff.Position);
                                    Console.WriteLine(enemyTurret.Position);
                                }
                                else
                                {
                                    Console.WriteLine("No heal found");
                                }
                            })));
        }

        public Composite FarmBehaviour()
        {
            return
                new PrioritySelector(
                    new Decorator(
                        ret =>
                        EntityManager.MinionsAndMonsters.EnemyMinions.Any(
                            o => o.Distance(Player.Instance) < Player.Instance.AttackRange),
                        new Action(
                            a =>
                                {
                                    var target =
                                        EntityManager.MinionsAndMonsters.EnemyMinions.OrderBy(
                                            o => o.Distance(Player.Instance) < Player.Instance.AttackRange)
                                            .FirstOrDefault();

                                    if (target == null)
                                    {
                                        return;
                                    }
                                    Orbwalker.ForcedTarget = target;
                                    Orbwalker.OrbwalkTo(target.Position);
                                })))
            ;
        }
    }
}