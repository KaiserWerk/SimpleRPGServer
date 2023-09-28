using SimpleRPGServer.Models.Auth;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SimpleRPGServer.Models.Ingame
{
    public class Player
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public long Gold { get; set; }
        public int ExperiencePoints { get; set; }
        public int MaxExperiencePoints { get; set; }
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public PlayerItem EquippedAttackWeapon { get; set; }
        public PlayerItem EquippedDefenseWeapon { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Locked { get; set; }
        public DateTime CreatedAt { get; set; }


        public ICollection<PlayerAbility> Abilities { get; set; }
        public ICollection<PlayerItem> Items { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; }
        public PlayerAbilityQueue AbilityQueue { get; set; }
        
        public Clan Clan { get; set; }

        public Player() { }
        public Player(string email, string displayName, string password)
        {
            this.Email = email;
            this.DisplayName = displayName;
            this.Password = password;
            this.MaxExperiencePoints = 10_000_000;
            this.CurrentHealth = 20;
            this.MaxHealth = 20;
            this.Strength = 22;
            this.Intelligence = 18;
            this.Locked = true;
            this.CreatedAt = DateTime.Now;
        }

        public int GetAttackStrength()
        {
            var x = this.Strength;
            if (this.EquippedAttackWeapon != null)
                x += this.EquippedAttackWeapon.AttackStrength;
            return x;
        }

        public int GetDefenseStrength()
        {
            var x = this.ExperiencePoints / 250;
            if (this.EquippedDefenseWeapon != null)
                x += this.EquippedDefenseWeapon.DefenseStrength;
            return x;
        }

        public PlayerData ToApiData()
        {
            return new PlayerData()
            {
                Email = this.Email,
                DisplayName = this.DisplayName,
                Gold = this.Gold,
                ExperiencePoints = this.ExperiencePoints,
                MaxExperiencePoints = this.MaxExperiencePoints,
                CurrentHealth = this.CurrentHealth,
                MaxHealth = this.MaxHealth,
                Strength = this.Strength,
                Intelligence = this.Intelligence,
                X = this.X,
                Y = this.Y,
            };
        }
    }

    public class PlayerData
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }
        [JsonPropertyName("gold")]
        public long Gold { get; set; }
        [JsonPropertyName("experience_points")]
        public int ExperiencePoints { get; set; }
        [JsonPropertyName("max_experience_points")]
        public int MaxExperiencePoints { get; set; }
        [JsonPropertyName("current_health")]
        public int CurrentHealth { get; set; }
        [JsonPropertyName("max_health")]
        public int MaxHealth { get; set; }
        [JsonPropertyName("strength")]
        public int Strength { get; set; }
        [JsonPropertyName("intelligence")]
        public int Intelligence { get; set; }
        [JsonPropertyName("x")]
        public int X { get; set; }
        [JsonPropertyName("y")]
        public int Y { get; set; }
    }
}
