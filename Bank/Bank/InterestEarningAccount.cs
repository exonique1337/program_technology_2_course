namespace Bank;

/// <summary>
/// Сберегательный счёт с начислением процентов на остаток.
/// </summary>
public class InterestEarningAccount : BankAccount
{
    /// <summary>
    /// Создаёт сберегательный счёт.
    /// </summary>
    /// <param name="name">Имя владельца.</param>
    /// <param name="initialBalance">Начальный баланс.</param>
    public InterestEarningAccount(string name, decimal initialBalance)
        : base(name, initialBalance) { }

    /// <summary>
    /// Начисляет 2% на остаток при балансе свыше 500 и записывает операцию в аудит.
    /// </summary>
    public override void PerformMonthAndTransactions()
    {
        if (Balance > 500m)
        {
            decimal interest = Balance * 0.02m;
            MakeDeposit(interest, DateTime.UtcNow, "Apply month interest", "Interest");
        }
    }
}