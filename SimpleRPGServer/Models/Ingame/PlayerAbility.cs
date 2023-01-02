using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleRPGServer.Models.Ingame
{
    public class PlayerAbility
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        //[ForeignKey("Player")]
        public Player Player { get; set; }
        //[ForeignKey("BaseAbility")]
        public BaseAbility BaseAbility { get; set; }
        public int CurrentLevel { get; set; }


        public PlayerAbilityData ToApiData()
        {
            return new PlayerAbilityData() 
            {
                Id = this.Id,
                Name = this.BaseAbility.Name,
                Description = this.BaseAbility.Description,
                CurrentLevel = this.CurrentLevel,
            };
        }
    }

    public class PlayerAbilityData
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CurrentLevel { get; set; }
    }
}
