using GuessNumberGame.Games;
using GuessNumberGame.Models;
using GuessNumberGame.Services;

namespace GuessNumberGame
{
    class Program
    {
        static void Main(string[] args)
        {
            INumberGenerator rng = new RandomGenerator();
            StatisticsService stats = new StatisticsService();
            GameEngine game = new GameEngine(rng);

            // Подписываемся на события
            game.OnMessage += (msg) => Console.WriteLine(msg);

            game.OnGameOver += (isWin, tries) =>
            {
                var res = new GameResult { IsWin = isWin, Tries = tries };
                stats.Save(res);
            };

            while (true)
            {
                Console.WriteLine("\n1. Играть (Легко)");
                Console.WriteLine("2. Играть (Средне)");
                Console.WriteLine("3. Играть (Сложно)");
                Console.WriteLine("4. Статистика");
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");

                string choice = Console.ReadLine();
                if (choice == "0") break;

                if (choice == "1") game.Play(new Settings(Difficulty.Easy));
                else if (choice == "2") game.Play(new Settings(Difficulty.Medium));
                else if (choice == "3") game.Play(new Settings(Difficulty.Hard));
                else if (choice == "4")
                {
                    var data = stats.Load();
                    Console.WriteLine($"Всего игр: {data.TotalGames}, Побед: {data.Wins}");
                }
            }
        }
    }
}