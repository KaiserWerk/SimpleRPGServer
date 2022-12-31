using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleRPGServer.Models.Ingame
{
    public class PlayerAbilityQueue
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        //[ForeignKey("Player")]
        public Player Player { get; set; }
       
        public BaseAbility BaseAbility { get; set; }
        public DateTime StartedAt { get; set; }
        public int TrainingLevel { get; set; }
    }
}
