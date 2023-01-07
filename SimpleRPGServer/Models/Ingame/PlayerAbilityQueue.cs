using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleRPGServer.Models.Ingame
{
    public class PlayerAbilityQueue
    {
        public long Id { get; set; }
        public Player Player { get; set; }
        public BaseAbility BaseAbility { get; set; }
        public DateTime StartedAt { get; set; }
        public int TrainingLevel { get; set; }
    }
}
