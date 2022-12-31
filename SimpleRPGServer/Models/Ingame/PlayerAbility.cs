using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleRPGServer.Models.Ingame
{
    public class PlayerAbility
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        //[ForeignKey("Player")]
        public Player Player { get; set; }
        //[ForeignKey("BaseAbility")]
        public BaseAbility BaseAbility { get; set; }
        public int CurrentLevel { get; set; }
    }
}
