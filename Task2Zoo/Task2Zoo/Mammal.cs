namespace Zoo;

/// <summary>Млекопитающее </summary>
public class Mammal : Animal
{
    public string FurColor { get; set; } = string.Empty;

    public bool IsAdult => Age >= 3;

    public Mammal(int id, string name, int age, string furColor)
        : base(id, name, age)
    {
        FurColor = furColor;
    }

    public Mammal(string name) : base(name)
    {
        FurColor = "unknown";
    }

    // предоставляем свою реализацию абстрактного метода
    public override void MakeSound()
    {
        Console.WriteLine($"{Name} says: meow.");
    }

    public override string ToString()
        => $"[Mammal] {Name}, {Age} years, class: {Class}";
}