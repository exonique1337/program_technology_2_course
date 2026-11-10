using GuessNumberGame.Models;
using GuessNumberGame.Services;

namespace GuessNumberGame.Games
{
    public class GameEngine
    {
        private INumberGenerator _generator;

        // События по заданию
        public event Action<string> OnMessage;
        public event Action<bool, int> OnGameOver;

        public GameEngine(INumberGenerator generator)
        {
            _generator = generator;
        }

        public GameResult Play(Settings settings)
        {
            int target = _generator.Generate(settings.Min, settings.Max);
            OnMessage?.Invoke($"Загадал число от {settings.Min} до {settings.Max}. Попыток: {settings.Attempts}");

            for (int i = 1; i <= settings.Attempts; i++)
            {
                Console.Write("Введи число: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int guess))
                {
                    Console.WriteLine("Ошибка: введи число!");
                    i--;
                    continue;
                }

                if (guess == target)
                {
                    OnMessage?.Invoke("Ты угадал!!!");
                    OnGameOver?.Invoke(true, i);
                    return new GameResult { IsWin = true, Tries = i };
                }
                else if (guess < target)
                {
                    OnMessage?.Invoke("Мое число БОЛЬШЕ");
                }
                else
                {
                    OnMessage?.Invoke("Мое число МЕНЬШЕ");
                }
            }

            OnMessage?.Invoke($"Ты проиграл. Я загадал {target}");
            OnGameOver?.Invoke(false, settings.Attempts);
            return new GameResult { IsWin = false, Tries = settings.Attempts };
        }
    }
}