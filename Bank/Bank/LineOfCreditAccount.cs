namespace Bank;

/// <summary>
/// Кредитный счёт с возможностью ухода в минус до заданного лимита.
/// </summary>
public class LineOfCreditAccount : BankAccount
{
    /// <summary>
    /// Создаёт кредитный счёт.
    /// </summary>
    /// <param name="name">Имя владельца.</param>
    /// <param name="initialBalance">Начальный баланс.</param>
    /// <param name="creditLimit">Размер кредитного лимита (положительное число).</param>
    public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit)
        : base(name, initialBalance, -creditLimit) { }

    /// <summary>
    /// Начисляет 7% на отрицательный баланс и записывает операцию в аудит.
    /// </summary>
    public override void PerformMonthAndTransactions()
    {
        if (Balance < 0)
        {
            decimal interest = -Balance * 0.07m;
            MakeWithdrawal(interest, DateTime.UtcNow, "Charge monthly interest", "Interest");
        }
    }

    /// <summary>
    /// Накладывает штраф за овердрафт при превышении кредитного лимита.
    /// </summary>
    /// <param name="isOverdrawn">Признак превышения лимита.</param>
    /// <returns>Транзакцию со штрафом −20 или null, если превышения нет.</returns>
    protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn)
        => isOverdrawn ? new Transaction(-20, DateTime.UtcNow, "Apply overdraft") : default;
}