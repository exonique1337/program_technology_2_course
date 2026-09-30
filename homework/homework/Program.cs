using Cinema;
using System;
using System.Collections.Generic;

namespace homework
/// Домашняя работа Boichuk K.A group 210b
/// Variant 2 - Cinema
/// 
/// добавил try/catch вокруг switch.
/// Оба репозитория могут бросить исключение  InMemory если данных нет,
/// CSV если файл битый или не найден. Ловим всё разом, чтобы не дублировать вот и все думаю логична если нет то я тупой
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var _session = new Session(12,23,new TimeSpan(),34,-20);
            Console.WriteLine(_session.GetInfo());
            /// выбор пользователя 
            Console.WriteLine("1 - Через InMemoryRepository");
            Console.WriteLine("2 - Через CSV");
            Console.Write("выбери как пойдем дальше дружище: ");
            string choice = Console.ReadLine();

            List<Genre> genres;
            List<Movie> movies;
            List<Session> sessions;

            // try/catch вокруг всего switch
            try
            {
                switch (choice)
                {
                    case "1":
                        InMemoryRepository mem = new InMemoryRepository();
                        genres = mem.GetGenres();
                        movies = mem.GetMovies();
                        sessions = mem.GetSessions();
                        break;

                    case "2":
                        CsvRepository csv = new CsvRepository("data");
                        genres = csv.GetGenres();
                        movies = csv.GetMovies();
                        sessions = csv.GetSessions();
                        break;

                    default:
                        Console.WriteLine("Неверный выбор");
                        return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
                return;
            }

            Console.WriteLine();

            string genre = FindGenre(movies, genres, "Интерстеллар");
            Console.WriteLine("1. " + (genre == null ? "null" : genre));

            genre = FindGenre(movies, genres, "Неизвестный фильм");
            Console.WriteLine("   " + (genre == null ? "null" : genre));

            Movie movie = FindMovie(movies, "Интерстеллар");
            Session session = FindSession(sessions, movie);
            Console.WriteLine("2. " + (session == null ? "null" : session.GetInfo()));

            Console.WriteLine("3. " + GetTotalDuration(movies) + " минут");

            Console.WriteLine("4. Группировка:");
            Dictionary<string, List<Movie>> grouped = GroupMoviesByGenre(movies, genres);
            foreach (KeyValuePair<string, List<Movie>> pair in grouped)
                Console.WriteLine("   " + pair.Key + " — " + pair.Value.Count);

            Console.WriteLine("5. Все фильмы:");
            PrintAllMovies(movies, genres);
        }

        // ищет фильм по названию
        static Movie FindMovie(List<Movie> movies, string title)
        {
            if (movies == null) return null;
            foreach (Movie m in movies)
                if (m.Title == title) return m;
            return null;
        }

        // ищет имя жанра по id
        static string GetGenreName(List<Genre> genres, int id)
        {
            if (genres == null) return null;
            foreach (Genre g in genres)
                if (g.Id == id) return g.Name;
            return null;
        }

        /// <summary>жанры фильма по названию. Не найдено тогда  null</summary>
        static string FindGenre(List<Movie> movies, List<Genre> genres, string title)
        {
            if (movies == null || genres == null || title == null) return null;
            Movie m = FindMovie(movies, title);
            if (m == null) return null;
            return GetGenreName(genres, m.GenreId);
        }

        /// <summary>первый сеанс фильма. Не найдено null</summary>
        static Session FindSession(List<Session> sessions, Movie movie)
        {
            if (sessions == null || movie == null) return null;
            foreach (Session s in sessions)
                if (s.MovieId == movie.Id) return s;
            return null;
        }

        /// <summary>суммарная длительность всех фильмов</summary>
        static int GetTotalDuration(List<Movie> movies)
        {
            if (movies == null) return 0;
            int total = 0;
            foreach (Movie m in movies) total = total + m.Duration;
            return total;
        }

        /// <summary>группировка фильмов по жанрам</summary>
        static Dictionary<string, List<Movie>> GroupMoviesByGenre(List<Movie> movies, List<Genre> genres)
        {
            Dictionary<string, List<Movie>> result = new Dictionary<string, List<Movie>>();
            if (movies == null) return result;
            foreach (Movie m in movies)
            {
                string name = GetGenreName(genres, m.GenreId);
                if (name == null) name = "Без жанра";
                if (!result.ContainsKey(name)) result[name] = new List<Movie>();
                result[name].Add(m);
            }
            return result;
        }

        /// <summary>вывод всех фильмов с жанром</summary>
        static void PrintAllMovies(List<Movie> movies, List<Genre> genres)
        {
            if (movies == null) return;
            foreach (Movie m in movies)
            {
                string name = GetGenreName(genres, m.GenreId);
                if (name == null) name = "—";
                Console.WriteLine("   \"" + m.GetInfo() + "\" — жанр \"" + name + "\"");
            }
        }
    }
}