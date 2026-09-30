using System;
using System.Collections.Generic;

namespace Cinema
{
    /// <summary>Репозиторий с данными в памяти</summary>
    public class InMemoryRepository
    {
        private List<Genre> _genres;
        private List<Movie> _movies;
        private List<Session> _sessions;

        public InMemoryRepository()
        {
            _genres = new List<Genre>
            {
                new Genre { Id = 1, Name = "Фантастика" },
                new Genre { Id = 2, Name = "Драма" },
                new Genre { Id = 3, Name = "Боевик" },
                new Genre { Id = 4, Name = "Комедия" },
                new Genre { Id = 5, Name = "Триллер" }
            };

            _movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Интерстеллар",      GenreId = 1, Duration = 169, Year = 2014 },
                new Movie { Id = 2, Title = "Начало",            GenreId = 1, Duration = 148, Year = 2010 },
                new Movie { Id = 3, Title = "Зелёная миля",      GenreId = 2, Duration = 189, Year = 1999 },
                new Movie { Id = 4, Title = "Форсаж",            GenreId = 3, Duration = 106, Year = 2001 },
                new Movie { Id = 5, Title = "Джентльмены удачи", GenreId = 4, Duration = 87,  Year = 1971 }
            };

            // данные уже уникальны — Title и Name не повторяются
            _sessions = new List<Session>
            {
                new Session { Id = 1, MovieId = 1, StartTime = new TimeSpan(18, 30, 0), Hall = 3, Price = 450 },
                new Session { Id = 2, MovieId = 2, StartTime = new TimeSpan(21, 0, 0),  Hall = 1, Price = 500 },
                new Session { Id = 3, MovieId = 3, StartTime = new TimeSpan(15, 0, 0),  Hall = 2, Price = 300 },
                new Session { Id = 4, MovieId = 4, StartTime = new TimeSpan(19, 0, 0),  Hall = 4, Price = 350 },
                new Session { Id = 5, MovieId = 5, StartTime = new TimeSpan(12, 0, 0),  Hall = 5, Price = 250 },
/*                new Session { Id = 5, MovieId = 5, StartTime = new TimeSpan(12, 0, 0),  Hall = 5, Price = -250 }*/  // тест для проверки 
            };
        }

        public List<Genre> GetGenres() { return _genres; }
        public List<Movie> GetMovies() { return _movies; }
        public List<Session> GetSessions() { return _sessions; }
    }
}