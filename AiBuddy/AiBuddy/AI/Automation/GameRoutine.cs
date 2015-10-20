namespace AiBuddy.AI.Automation
{
    using EloBuddy;

    using TreeSharp;

    public abstract class GameRoutine
    {
        public abstract void OnLoad();

        public abstract void OnDraw();

        public abstract GameMapId MapId { get; }

        public abstract Composite MoveBehaviour { get; }

        public abstract Composite ShopBehaviour { get; }

        public abstract void Rebuild();
    }
}