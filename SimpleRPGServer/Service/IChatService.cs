using SimpleRPGServer.Models.Ingame;
using SimpleRPGServer.Persistence.Models.Ingame;
using System.Collections.Generic;

namespace SimpleRPGServer.Service;

public interface IChatService
{
    void AddClanMessage(Player player, Clan clan, string message);
    void AddFieldInfoMessage(int x, int y, string message);
    void AddFieldMessage(Player player, int x, int y, string message);
    void AddGlobalInfoMessage(string message);
    void AddShoutMessage(Player player, string message);
    IEnumerable<ChatMessage> GetChatMessages(Player player);
    IEnumerable<ChatMessage> GetInfoMessages();
}