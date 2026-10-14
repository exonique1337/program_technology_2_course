namespace Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Полиморфизм: список базового типа Animal,
            // внутри лежат объекты-наследники. Компилятор это разрешает.
            List<Animal> animals = new List<Animal>
            {
                new Mammal(1, "Barsik", 3, "gray"),
                new Bird(2, "Kesha", 2, 120),
                new Reptile(3, "Kaa", 5, true)
            };

            foreach (Animal animal in animals)
            {
                // Вызывается ToString() того наследника, чей объект лежит в переменной
                Console.WriteLine(animal);
                Console.WriteLine($"  age in months: {animal.AgeInMonths}");

                // MakeSound() тоже выбирается по фактическому типу
                animal.MakeSound();

                // is проверяет, реализует ли объект интерфейс.
                // Если да — flyable уже нужного типа, можно звать Fly().
                if (animal is IFlyable flyable)
                {
                    flyable.Fly();
                }
                else
                {
                    Console.WriteLine($"{animal.Name} cannot fly.");
                }

                Console.WriteLine("----------------------------------------");
            }
        }
    }
}