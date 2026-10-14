namespace Zoo;

/// <summary>Рептилия </summary>
public class Reptile : Animal
{
    public bool IsVenomous { get; set; }

    public bool IsDangerous => IsVenomous && Age > 1;

    public Reptile(int id, string name, int age, bool isVenomous)
        : base(id, name, age)
    {
        IsVenomous = isVenomous;
    }

    public Reptile(string name) : base(name)
    {
        IsVenomous = false;
    }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} says: hiss.");
    }

    public override string ToString()
        => $"[Reptile] {Name}, {Age} years, class: {Class}";
}