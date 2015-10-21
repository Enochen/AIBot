namespace AiBuddy.AI.Automation.Contexts
{
    using System;
    using System.Drawing;
    using System.Linq;

    using AiBuddy.AI.Logic;
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
            Chat.Print(
                string.Format(
                    "<font color=\"#40c1ff\">[AIBuddy]:</font> <font color=\"#ffffff\">{0} loaded</font>",
                    this.MapId));

            this.shopHandler = new ShopHandler();
            FindChampion.FindAndSetChampion();
            this.Rebuild();
            this.m_shopBehaviour.Start(null);
            this.m_moveBehaviour.Start(null);
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

        private Composite m_shopBehaviour;

        private Composite m_moveBehaviour;

        public override Composite MoveBehaviour
        {
            get
            {
                return this.m_moveBehaviour;
            }
        }

        public override Composite ShopBehaviour
        {
            get
            {
                return this.m_shopBehaviour;
            }
        }

        public override void Rebuild()
        {
            this.m_shopBehaviour = new PrioritySelector(this.BuyBehaviour());
            this.m_moveBehaviour = new PrioritySelector(
                this.HealBehaviour(),
                this.AttackStructureBehaviour(),
                this.FarmBehaviour(),
                this.FollowBehaviour());
        }

        public Composite BuyBehaviour()
        {
            return new Decorator(
                ret => true
                //&& new ItemData((uint)this.shopHandler.CurrentItem.Id).BasePrice < Player.Instance.Gold
                ,
                new Action(ret => { this.shopHandler.OnTick(); }));
        }

        public Composite FollowBehaviour()
        {
            return new PrioritySelector(
                new Decorator(
                    ret => Player.Instance.CanMove,
                    new Action(
                        ret =>
                            {
                                Console.WriteLine("Moving to Nearest Ally or Minion or Turret");
                                var ally =
                                    EntityManager.Heroes.Allies.Where(x => !x.IsDead && !x.IsInShopRange() && !x.IsMe)
                                        .OrderBy(x => x.ChampionsKilled)
                                        .FirstOrDefault();

                                var minion =
                                    EntityManager.MinionsAndMonsters.AlliedMinions.OrderByDescending(
                                        x => x.Distance(ObjectManager.Get<Obj_HQ>().FirstOrDefault())).ElementAt(3);

                                var turret =
    EntityManager.Turrets.Allies.OrderByDescending(
        x => x.Distance(ObjectManager.Get<Obj_HQ>().FirstOrDefault())).FirstOrDefault();

                                if (ally != null)
                                {
                                    Orbwalker.OrbwalkTo(ally.Position.Randomize());
                                    Console.WriteLine("Ally found, moving");
                                }
                                else if (minion != null)
                                {
                                    var position =
                                        minion.Position.Extend(ObjectManager.Get<Obj_HQ>().FirstOrDefault(), 250)
                                            .To3D()
                                            .Randomize();
                                    if (position.ToNavMeshCell().CalculateSafety() < NavigationSafety.Danger)
                                    {
                                        Orbwalker.OrbwalkTo(position);
                                    }
                                    Console.WriteLine("Minion found, moving to " + position);
                                }
                                else if (turret != null)
                                {
                                    Orbwalker.OrbwalkTo(turret.Position.Randomize());
                                    Console.WriteLine("Turret Found");
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
            return
                new PrioritySelector(
                    new Decorator(
                        ret =>
                        (Player.Instance.HealthPercent < 50 || Player.Instance.ManaPercent < 30)
                        && Player.Instance.CanMove && healingBuff != null && healingBuff.IsVisible,
                        new Action(
                            ret =>
                                {
                                    Console.WriteLine("Healing");
                                    var enemyTurret =
                                        EntityManager.Turrets.Enemies.Where(x => !x.IsDead)
                                            .OrderBy(x => x.Distance(Player.Instance))
                                            .FirstOrDefault();

                                    if ((enemyTurret != null && healingBuff.IsVisible)
                                        && healingBuff.Distance(enemyTurret) > enemyTurret.AttackRange * 1.9
                                        && healingBuff.IsInRange(Player.Instance, 1300))
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
                            o => o.Distance(Player.Instance) < Player.Instance.GetAutoAttackRange()),
                        //&& Player.Instance.CountEnemiesInRange(1000) < 1,
                        new Action(
                            a =>
                                {
                                    var target =
                                        EntityManager.MinionsAndMonsters.EnemyMinions.OrderBy(
                                            o => o.Distance(Player.Instance) < Player.Instance.AttackRange)
                                            .FirstOrDefault();

                                    if (target != null)
                                    {
                                        var position = target.Position.Randomize();

                                        if (position.ToNavMeshCell().CalculateSafety() > NavigationSafety.Average)
                                        {
                                            return;
                                        }
                                        Orbwalker.ForcedTarget = target;
                                        var minion = EntityManager.MinionsAndMonsters.AlliedMinions.OrderByDescending(
                                            x => x.Distance(ObjectManager.Get<Obj_HQ>().FirstOrDefault())).ElementAt(3);
                                        if (
                                            minion != null)
                                        {
                                            Orbwalker.OrbwalkTo(minion.Position.Randomize());
                                        }
                                        //Player.IssueOrder(GameObjectOrder.AutoAttack, target);
                                    }
                                })));
        }

        public Composite AttackStructureBehaviour()
        {
            var target =
                EntityManager.Turrets.Enemies.OrderBy(t => t.Distance(Player.Instance) < Player.Instance.AttackRange)
                    .FirstOrDefault();
            return
                new PrioritySelector(
                    new Decorator(
                        ret =>
                            {
                                return target != null
                                       && (EntityManager.Turrets.Enemies.Any(
                                           t => t.Distance(Player.Instance) < Player.Instance.GetAutoAttackRange())
                                           && target.Position.ToNavMeshCell().CalculateSafety()
                                           < NavigationSafety.Danger);
                            },
                        new Action(
                            a =>
                                {
                                    if (target == null
                                        || target.Position.ToNavMeshCell().CalculateSafety() > NavigationSafety.Average)
                                    {
                                        return;
                                    }
                                    //Orbwalker.ForcedTarget = target;
                                    //Orbwalker.OrbwalkTo(target.Position.Randomize());
                                    Player.IssueOrder(GameObjectOrder.AttackUnit, target);
                                })));
        }

        public Composite AttackEnemyBehavior()
        {
            var target =
                EntityManager.Heroes.Enemies.OrderBy(t => t.Distance(Player.Instance) < Player.Instance.AttackRange)
                    .FirstOrDefault();
            return
                new PrioritySelector(
                    new Decorator(
                        ret =>
                            {
                                return target != null
                                       && (EntityManager.Heroes.Enemies.Any(
                                           t => t.Distance(Player.Instance) < Player.Instance.GetAutoAttackRange())
                                           && target.Position.ToNavMeshCell().CalculateSafety()
                                           < NavigationSafety.Danger);
                            },
                        new Action(
                            a =>
                                {
                                    if (target == null
                                        || target.Position.ToNavMeshCell().CalculateSafety() > NavigationSafety.Average)
                                    {
                                        return;
                                    }
                                    //Orbwalker.ForcedTarget = target;
                                    //Orbwalker.OrbwalkTo(target.Position.Randomize());
                                    Player.IssueOrder(GameObjectOrder.AttackUnit, target);
                                })));
        }
    }
}