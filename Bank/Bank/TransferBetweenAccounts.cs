namespace Bank;

/// <summary>
/// Сервис перевода денег между двумя банковскими счетами.
/// </summary>
public static class TransferBetweenAccounts
{
    /// <summary>
    /// Переводит деньги со счёта <paramref name="from"/> на счёт <paramref name="to"/>.
    /// </summary>
    /// <param name="from">Счёт-отправитель.</param>
    /// <param name="to">Счёт-получатель.</param>
    /// <param name="amount">Сумма перевода. Должна быть положительной.</param>
    /// <exception cref="ArgumentNullException">Если один из счетов равен null.</exception>
    /// <exception cref="ArgumentException">Если счета совпадают.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Если сумма ≤ 0.</exception>
    /// <exception cref="InvalidOperationException">
    /// Если на счёте-отправителе недостаточно средств.
    /// </exception>
    public static void Transfer(BankAccount from, BankAccount to, decimal amount)
    {
        ArgumentNullException.ThrowIfNull(from);
        ArgumentNullException.ThrowIfNull(to);
        if (ReferenceEquals(from, to))
            throw new ArgumentException("You cannot transfer funds to yourself.");
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be positive");

        from.MakeWithdrawal(amount, DateTime.UtcNow, "Transfer out", "Transfer");
        to.MakeDeposit(amount, DateTime.UtcNow, "Transfer in", "Transfer");
    }
}