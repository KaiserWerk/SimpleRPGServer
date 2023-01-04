using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleRPGServer.Models.Ingame
{
    public class BaseItem
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemType Type { get; set; }
        public int GoldCost { get; set; }
        public int Durability { get; set; }
        public int AttackStrength { get; set; }
        public int DefenseStrength { get; set; }
        public int MaxCharges { get; set; }

    }
}
