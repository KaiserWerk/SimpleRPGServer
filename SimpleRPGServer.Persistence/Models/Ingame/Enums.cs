namespace SimpleRPGServer.Persistence.Models.Ingame;

public enum ItemType
{
    Other,
    AttackWeapon,
    DefenseWeapon,
    Spell,
    Necklace,
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

public enum ItemLocation
{
    Inventory,
    Bank,
    Dropped,
}
