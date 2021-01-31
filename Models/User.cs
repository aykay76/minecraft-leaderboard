using System;
using System.Text.Json.Serialization;

namespace minecraft_leaderboard.Models
{
    public class User
    {
        public string Name { get; set; }

        [JsonPropertyName("uuid")]
        public Guid ID { get; set; }
    }
}
