namespace SimpleRPGServer.Models.Ingame
{
    public class FightResult
    {
        public bool NpcSurvived { get; set; }
        public int NpcHealthLeft { get; set; }
        public int PlayerHealthLeft { get; set; }
        public int ExperienceGained { get; set; }
    }
}
