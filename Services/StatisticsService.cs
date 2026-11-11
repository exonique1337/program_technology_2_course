using System.Text.Json;
using GuessNumberGame.Models;

namespace GuessNumberGame.Services
{
    public class StatisticsService
    {
        private string _filePath = "stats.json";

        public void Save(GameResult result)
        {
            StatData data = Load();
            data.TotalGames++;
            if (result.IsWin)
            {
                data.Wins++;
            }

            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public StatData Load()
        {
            if (!File.Exists(_filePath))
                return new StatData();

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<StatData>(json);
        }
    }

    public class StatData
    {
        public int TotalGames { get; set; }
        public int Wins { get; set; }
    }
}