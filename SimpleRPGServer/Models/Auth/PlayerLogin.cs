﻿using SimpleRPGServer.Models.Ingame;
using System;
using System.Text.Json.Serialization;

namespace SimpleRPGServer.Models.Auth
{
    public class PlayerLogin
    {
        [JsonIgnore]
        public long Id { get; set; }
        [JsonIgnore]
        public Player Player { get; set; }
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("valid_until")]
        public DateTime ValidUntil { get; set; }
    }
}
