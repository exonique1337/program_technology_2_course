namespace Cinema
{
    /// <summary>Фильм (аналог Book с примера про библиотеку).
    /// Карточка о фильме — его описание.
    /// Id — номер фильма.
    /// Title — название фильма (по правилу: не уникально, поиск возвращает первую найденную).
    /// </summary>
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int Duration { get; set; }
        public int Year { get; set; }

        // ИСПРАВИЛ, а что исправил не понял, а нет понял я  упростил свойство (было с get { return ... })
        /// <summary>Длинный ли фильм (> 120 мин)</summary>
        public bool IsLong => Duration > 120;

        /// <summary>Инфа о фильме</summary>
        public string GetInfo()
        {
            return Title + " (" + Year + ", " + Duration + " мин)";
        }
    }
}