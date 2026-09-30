namespace Cinema
{
    /// <summary>Жанр фильма (аналог Publisher)</summary>
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        /// <summary>Название жанра</summary>
        public string Info
        {
            get { return Name; }
        }
    }
}