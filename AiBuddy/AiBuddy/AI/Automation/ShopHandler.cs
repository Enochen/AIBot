using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EloBuddy;
using EloBuddy.SDK;
using AiBuddy.Properties;
using TreeSharp;
using Newtonsoft.Json;

using Action = TreeSharp.Action;

namespace AiBuddy.AI.Automation
{
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

    class ShopHandler
    {
        private Composite _behaviour;
        private readonly List<Item> _build;

        private readonly JsonChampionBuildFile _buildInformation;

        public List<Item> Build
        {
            get { return _build; }
        }

        public List<Item> CurrentBuild
        {
            get { return this._build.Where(item => !Item.HasItem(item.Id)).ToList(); }
        }

        public Item CurrentItem
        {
            get { return CurrentBuild.FirstOrDefault(); }
        }

        public void Rebuild()
        {
            this._behaviour = new Decorator(
                ret => Shop.CanShop && this.CurrentItem != null,
                new PrioritySelector(
                        ret => true,
                        new Action(ret =>
                        {
                            CurrentItem.Buy();
                        })
                ));
        }

        public void OnTick()
        {
            _behaviour.Tick(null);

            if (_behaviour.LastStatus != RunStatus.Running)
            {
                _behaviour.Stop(null);
                _behaviour.Start(null);
            }
        }

        public ShopHandler()
        {
            var champBuild = Encoding.UTF8.GetString(Resources.ChampionBuild_5_15_1);

            try
            {
                _buildInformation = JsonConvert.DeserializeObject<JsonChampionBuildFile>(champBuild);
                Console.WriteLine("ChampionBuild Version {0} loaded", _buildInformation.Version);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to parse JSON file");
            }

            this._build =
                _buildInformation.Data.FirstOrDefault(entry => entry.Name == Player.Instance.ChampionName).Items;
            this.Rebuild();

            this._behaviour.Start(null);
        }
    }
}
