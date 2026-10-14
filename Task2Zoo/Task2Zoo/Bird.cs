namespace Zoo;

/// <summary>Птица умеет летать, поэтому реализует IFlyable</summary>
public class Bird : Animal, IFlyable
{
    public int WingSpan { get; set; }

    public bool IsBigBird => WingSpan > 100;

    public Bird(int id, string name, int age, int wingSpan)
        : base(id, name, age)
    {
        WingSpan = wingSpan;
    }

    public Bird(string name) : base(name)
    {
        WingSpan = 0;
    }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} says: chirp.");
    }

    // Реализация интерфейсного метода — БЕЗ override,
    // так как это не переопределение метода базового класса,
    // а реализация контракта интерфейса IFlyable
    public void Fly()
    {
        Console.WriteLine($"{Name} flaps wings and flies.");
    }

    public override string ToString()
        => $"[Bird] {Name}, {Age} years, class: {Class}, wingspan: {WingSpan}";
}