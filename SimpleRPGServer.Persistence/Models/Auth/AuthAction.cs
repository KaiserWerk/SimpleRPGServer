namespace SimpleRPGServer.Persistence.Models.Auth;

public class AuthAction
{
    public ulong Id { get; set; }
    public ulong PlayerId { get; set; }
    public string Action { get; set; }
    public string Code { get; set; }
    public DateTime ValidUntil { get; set; }

    public AuthAction()
    { }
    public AuthAction(ulong id, string action, DateTime validUntil)
    {
        this.PlayerId = id;
        this.Action = action;
        this.ValidUntil = validUntil;
        this.Code = Guid.NewGuid().ToString();
    }
}
