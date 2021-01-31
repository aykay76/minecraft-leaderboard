using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using minecraft_leaderboard.Models;

namespace minecraft_leaderboard.Services
{
    public class DataService
    {
        public async Task<List<User>> GetUsers()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            string root = Environment.GetEnvironmentVariable("MINECRAFT_HOME");
            Stream source = File.OpenRead($"{root}/data/usercache.json");
            List<User> users = await JsonSerializer.DeserializeAsync<List<User>>(source, options);
            source.Close();

            return users;
        }

        private int Get(JsonElement input, string property)
        {
            int value = 0;
            JsonElement el;

            if (input.TryGetProperty(property, out el))
            {
                value = el.GetInt32();
            }

            return value;
        }

        public async Task<Stats> GetStats(Guid id)
        {
            string root = Environment.GetEnvironmentVariable("MINECRAFT_HOME");
            Stream source = File.OpenRead($"{root}/data/world/stats/{id}.json");

            JsonDocument doc = JsonDocument.Parse(source);
            Stats stats = new Stats();

            JsonElement kills = doc.RootElement.GetProperty("stats").GetProperty("minecraft:killed");

            stats.SpidersKilled = Get(kills, "minecraft:spider");
            stats.SkeletonsKilled = Get(kills, "minecraft:skeleton");
            stats.ChickensKilled = Get(kills, "minecraft:chicken");
            stats.PigsKilled = Get(kills, "minecraft:pig");
            stats.SalmonKilled = Get(kills, "minecraft:salmon");
            stats.SquidKilled = Get(kills, "minecraft:squid");
            stats.SheepKilled = Get(kills, "minecraft:sheep");
            stats.ZombiesKilled = Get(kills, "minecraft:zombie");
            stats.CowsKilled = Get(kills, "minecraft:cow");

            return stats;
        }
    }
}
