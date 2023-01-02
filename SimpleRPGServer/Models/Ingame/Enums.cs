namespace SimpleRPGServer.Models.Ingame
{
    public enum ItemType
    {
        Other = 0,
        AttackWeapon = 1,
        DefenseWeapon = 2,
        Spell = 3,
        Necklace = 4,
    }

    public enum MessageType
    {
        FieldSay,
        Shout,
        Group,
        Clan,
        FieldInfo,
        GlobalInfo,
    }
}
