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
            Chat.Print($"<font color=\"#40c1ff\">[AIBuddy]:</font> <font color=\"#ffffff\">{this.MapId} loaded</font>");

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
                this.ShoppingBehaviour(),
                this.HealBehaviour(),
                this.FarmBehaviour());
        }

        public Composite ShoppingBehaviour()
        {
            return new Decorator(ret => Shop.CanShop, new Action(ret => { this.shopHandler.OnTick(); }));
        }

        public Composite HealBehaviour()
        {
            return new PrioritySelector(
                new Decorator(
                    ret => Player.Instance.HealthPercent < 50,
                    new Action(
                        ret =>
                            {
                                Console.WriteLine("Healing");
                                var healingBuff =
                                    ObjectManager.Get<GameObject>()
                                        .Where(o => o.IsEnemy && o.Name.ToLower().Contains("healingbuff"))
                                        .OrderBy(o => Player.Instance.Position.Distance(o.Position))
                                        .FirstOrDefault();

                                if (healingBuff != null)
                                {
                                    Player.IssueOrder(GameObjectOrder.MoveTo, healingBuff.Position);
                                    Console.WriteLine("Heal found");
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