namespace GuessNumberGame.Services
{
    public interface INumberGenerator
    {
        int Generate(int min, int max);
    }
}