﻿using System.Collections.Generic;

namespace SimpleRPGServer.Models.Ingame
{
    public class BaseNpc
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int GrantExperience { get; set; }
        public int AttackStrength { get; set; }
        public int Health { get; set; }
        public string ImageFilename { get; set; }
        public long GoldDrop { get; set; }
        public BaseItem DroppedItem { get; set; }
        public int DropChance { get; set; }
        public int SpawnXStart { get; set; }
        public int SpawnXEnd { get; set; }
        public int SpawnYStart { get; set; }
        public int SpawnYEnd { get; set; }
    }
}
