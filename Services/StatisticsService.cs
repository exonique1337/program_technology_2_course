namespace GuessNumberGame.Services
{
    public class RandomGenerator : INumberGenerator
    {
        private Random _random = new Random();

        public int Generate(int min, int max)
        {
            return _random.Next(min, max + 1);
        }
    }
}