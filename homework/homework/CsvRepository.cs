using System;
using System.Collections.Generic;
using System.IO;

namespace Cinema
{
    /// <summary>Репозиторий, читающий данные из CSV</summary>
    public class CsvRepository
    {
        private string _basePath;

        public CsvRepository(string basePath) { _basePath = basePath; }

        public List<Genre> GetGenres()
        {
            List<Genre> result = new List<Genre>();
            string[] lines = File.ReadAllLines(Path.Combine(_basePath, "genres.csv"));
            if (lines.Length < 2) return result;
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 2) continue;

                Genre g = new Genre();
                g.Id = int.Parse(parts[0]);
                g.Name = parts[1];

                // исправил правило Name жанра уникален — пропускаем дубликат
                if (NameExists(result, g.Name)) continue;

                result.Add(g);
            }
            return result;
        }

        public List<Movie> GetMovies()
        {
            List<Movie> result = new List<Movie>();
            string[] lines = File.ReadAllLines(Path.Combine(_basePath, "movies.csv"));
            if (lines.Length < 2) return result;
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 5) continue;

                Movie m = new Movie();
                m.Id = int.Parse(parts[0]);
                m.Title = parts[1];
                m.GenreId = int.Parse(parts[2]);
                m.Duration = int.Parse(parts[3]);
                m.Year = int.Parse(parts[4]);

                // правило Title фильма уникален — пропускаем дубликат
                if (TitleExists(result, m.Title)) continue;

                result.Add(m);
            }
            return result;
        }

        public List<Session> GetSessions()
        {
            List<Session> result = new List<Session>();
            string[] lines = File.ReadAllLines(Path.Combine(_basePath, "sessions.csv"));
            if (lines.Length < 2) return result;
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length < 5) continue;

                decimal price = decimal.Parse(parts[4]);

                // сделал правило Price >= 0 — пропускаем плохую строку
                if (price < 0)
                {
                    Console.WriteLine("Пропущен сеанс с отриицательной ценой");
                    continue;
                }

                Session s = new Session();
                s.Id = int.Parse(parts[0]);
                s.MovieId = int.Parse(parts[1]);
                // строгий формат HH:mm через ParseExact сделал чтобы нормально было 
                s.StartTime = TimeSpan.ParseExact(parts[2], @"hh\:mm", null);
                s.Hall = int.Parse(parts[3]);
                s.Price = price;

                result.Add(s);
            }
            return result;
        }

        // добавил хелпер для проверки уникальности Title (ПРАВИЛО ИЛИ УСЛОВИЕ С МОЕГО ВАРИАНТА )
        static bool TitleExists(List<Movie> movies, string title)
        {
            foreach (Movie m in movies)
                if (m.Title == title) return true;
            return false;
        }

        // добавил хелпер для проверки уникальности Name тоже добавил из за правила или условия по варианту =)))
        static bool NameExists(List<Genre> genres, string name)
        {
            foreach (Genre g in genres)
                if (g.Name == name) return true;
            return false;
        }
    }
}