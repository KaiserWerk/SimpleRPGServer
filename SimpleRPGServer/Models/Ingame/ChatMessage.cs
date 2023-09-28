﻿using System;

namespace SimpleRPGServer.Models.Ingame
{
    public class ChatMessage
    {
        public long Id { get; set; }
        public string SenderUser { get; set; }
        public string SenderClan { get; set; }
        public string Message { get; set; }
        public MessageType MessageType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public DateTime SentAt { get; set; }
    }
}
