using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleRPGServer.Models.Ingame
{
    public class PlayerItem
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        //[ForeignKey("PlayerId")]
        public Player Player { get; set; }
        //[ForeignKey("BaseItemId")]
        public BaseItem BaseItem { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CurrentDurability { get; set; }
        public int MaxDurability { get; set; }

    }
}
