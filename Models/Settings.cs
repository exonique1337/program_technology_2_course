namespace GuessNumberGame.Models
{
    public class Settings
    {
        public Difficulty Level { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Attempts { get; set; }

        public Settings(Difficulty level)
        {
            Level = level;
            if (level == Difficulty.Easy)
            {
                Min = 1; Max = 10; Attempts = 5;
            }
            else if (level == Difficulty.Medium)
            {
                Min = 1; Max = 100; Attempts = 7;
            }
            else
            {
                Min = 1; Max = 1000; Attempts = 10;
            }
        }
    }
}