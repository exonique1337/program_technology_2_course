namespace BankAccounts.Core.AccountNumbers;



// Это последовательная нумерация. Можно заменить на Gui или другую стратегию!
// Это тоже принцип (SRP)
// Реализация ниже: 
public sealed class SequentialAccountNumberGenerator:IAccountNumberGenerator
{
    private int _seed;
    public SequentialAccountNumberGenerator(int seed =1234567890)
        => _seed = seed;

    public string Next()
    {
        return (_seed++).ToString();
    }
}
