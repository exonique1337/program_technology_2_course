namespace Zoo;

/// <summary>
/// Базовый абстрактный класс животного.
/// Наследники обязаны переопределить MakeSound().
/// </summary>
public abstract class Animal
{
    // Приватное поле — закрыто от внешнего доступа.
    // Отдаём наружу через get-only свойство Id.
    private readonly int _id;

    public int Id => _id;

    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }

    // Свойство типа перечисления со значением по умолчанию:
    // если не задать Class явно, оно будет равно AnimalClass.Mammal
    public AnimalClass Class { get; set; } = AnimalClass.Mammal;

    // Вычисляемое свойство — get-only, без backing-поля.
    // Не хранится в памяти, считается на лету.
    public int AgeInMonths => Age * 12;

    /// <summary>Основной конструктор</summary>
    public Animal(int id, string name, int age)
    {
        _id = id;
        Name = name;
        Age = age;
    }

    /// <summary>Упрощённый конструктор — вызывает основной через this(...)</summary>
    public Animal(string name) : this(0, name, 0)
    {
    }

    /// <summary>Абстрактный метод — наследники обязаны переопределить</summary>
    public abstract void MakeSound();
}