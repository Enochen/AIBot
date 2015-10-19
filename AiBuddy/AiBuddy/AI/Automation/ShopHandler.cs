namespace AiBuddy.AI.Automation
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Versioning;
    using System.Text;
    using AiBuddy.Properties;
    using EloBuddy;
    using EloBuddy.SDK;

    using Newtonsoft.Json;

    using TreeSharp;

    using Action = TreeSharp.Action;

    public class JsonChampionBuildFile
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("data")]
        public List<ChampionBuild> Data { get; set; }
    }

    public class ChampionBuild
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }

    internal class ShopHandler
    {
        private Composite _behaviour;

        private readonly JsonChampionBuildFile _buildInformation;

        public List<Item> Build { get; }

        public List<Item> CurrentBuild
        {
            get
            {
                return this.Build.Where(item => !Item.HasItem(item.Id)).ToList();
            }
        }

        public Item CurrentItem
        {
            get
            {
                return this.CurrentBuild.FirstOrDefault();
            }
        }

        public void Rebuild()
        {
            this._behaviour = new Decorator(
                ret => Shop.CanShop && this.CurrentItem != null,
                new PrioritySelector(ret => true, new Action(ret => { this.CurrentItem.Buy(); })));
        }

        public void OnTick()
        {
            this._behaviour.Tick(null);

            if (this._behaviour.LastStatus != RunStatus.Running)
            {
                this._behaviour.Stop(null);
                this._behaviour.Start(null);
            }
        }

        public ShopHandler()
        {
            var champBuild = Encoding.UTF8.GetString(Resources.ChampionBuild_5_15_1);

            try
            {
                this._buildInformation = JsonConvert.DeserializeObject<JsonChampionBuildFile>(champBuild);
                Console.WriteLine("ChampionBuild Version {0} loaded", this._buildInformation.Version);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to parse JSON file");
            }

            this.Build =
                this._buildInformation.Data.FirstOrDefault(entry => entry.Name == Player.Instance.ChampionName).Items;
            this.Rebuild();

            this._behaviour.Start(null);
        }
    }
}