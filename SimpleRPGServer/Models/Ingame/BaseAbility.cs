using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleRPGServer.Models.Ingame
{
    public class BaseAbility
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxLevel { get; set; }
        public TimeSpan TrainingDuration { get; set; }
    }
}
