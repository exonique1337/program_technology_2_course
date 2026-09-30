using System;

namespace Cinema
{
    /// <summary>Сеанс фильма</summary>
    public class Session
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public TimeSpan StartTime { get; set; }
        public int Hall { get; set; }

        // ИСПРАВИЛ что теперь Price теперь с проверкой 
        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Price","Цена не может быть отрицательной");
                _price = value;
            }
        }

        /// <summary>Пустой кнструкатр</summary>
        public Session() { }

        /// <summary>Конструктор с параметрами</summary>
        public Session(int id, int movieId, TimeSpan startTime, int hall, decimal price)
        {
            Id = id;
            MovieId = movieId;
            StartTime = startTime;
            Hall = hall;
            Price = price;   
        }

        /// <summary>Вечерний ли сеанс (>= 18:00)</summary>
        public bool IsEvening
        {
            get { return StartTime >= new TimeSpan(18, 0, 0); }
        }

        /// <summary>Краткая инфа о сеансе</summary>
        public string GetInfo()
        {
            return StartTime.ToString(@"hh\:mm") + ", зал " + Hall + ", " + Price + " руб.";
        }
    }
}