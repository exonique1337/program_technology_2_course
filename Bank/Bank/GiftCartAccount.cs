namespace Bank;

/// <summary>
/// Подарочный счёт с ежемесячным автоматическим пополнением.
/// </summary>
public class GiftCardAccount : BankAccount
{
    private readonly decimal _monthlyDeposit = 0m;

    /// <summary>
    /// Создаёт подарочный счёт.
    /// </summary>
    /// <param name="name">Имя владельца.</param>
    /// <param name="initialBalance">Начальный баланс.</param>
    /// <param name="monthlyDeposit">Сумма ежемесячного пополнения. По умолчанию 0.</param>
    public GiftCardAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0)
        : base(name, initialBalance)
        => _monthlyDeposit = monthlyDeposit;

    /// <summary>
    /// Пополняет счёт на сумму ежемесячного взноса и записывает операцию в аудит.
    /// </summary>
    public override void PerformMonthAndTransactions()
    {
        if (_monthlyDeposit != 0)
            MakeDeposit(_monthlyDeposit, DateTime.UtcNow, "Add monthly deposit", "Deposit");
    }

    /// <summary>
    /// Возвращает строковое описание подарочного счёта.
    /// </summary>
    /// <returns>Информация о счёте и сумме ежемесячного пополнения.</returns>
    public override string ToString()
        => base.ToString() + $" monthly deposit: {_monthlyDeposit}";
}