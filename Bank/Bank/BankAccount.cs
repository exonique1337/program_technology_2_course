using System.Text;

namespace Bank;

/// <summary>
/// Представляет банковский счёт клиента с возможностью пополнения, снятия и ведения истории.
/// </summary>
public class BankAccount
{
    private readonly decimal _minimumBalance;
    private static int s_accountNumberSeed = 1000000000;

    /// <summary>
    /// Уникальный номер счёта.
    /// </summary>
    public string Number { get; }

    /// <summary>
    /// Имя владельца счёта.
    /// </summary>
    public string Owner { get; private set; }

    /// <summary>
    /// Статус клиента. Влияет на комиссии, проценты и кэшбэк.
    /// </summary>
    public ClientStatus Status { get; set; } = ClientStatus.Regular;

    /// <summary>
    /// Текущий баланс, рассчитанный по всем транзакциям.
    /// </summary>
    public decimal Balance
    {
        get
        {
            decimal balance = 0;
            foreach (var item in _allTransactions)
                balance += item.Amount;
            return balance;
        }
    }

    private List<Transaction> _allTransactions = new List<Transaction>();

    /// <summary>
    /// Создаёт счёт с нулевым минимальным балансом.
    /// </summary>
    /// <param name="name">Имя владельца.</param>
    /// <param name="initialBalance">Начальный баланс.</param>
    public BankAccount(string name, decimal initialBalance)
        : this(name, initialBalance, 0) { }

    /// <summary>
    /// Создаёт счёт с заданным минимальным балансом.
    /// </summary>
    /// <param name="name">Имя владельца.</param>
    /// <param name="initialBalance">Начальный баланс.</param>
    /// <param name="minimumBalance">Минимально допустимый баланс.</param>
    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {
        Owner = name;
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;
        _minimumBalance = minimumBalance;

        if (initialBalance > 0)
            MakeDeposit(initialBalance, DateTime.UtcNow, "Initial balance");
    }

    /// <summary>
    /// Пополняет счёт на указанную сумму.
    /// </summary>
    /// <param name="amount">Сумма пополнения. Должна быть положительной.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Бросается, если <paramref name="amount"/> меньше или равен нулю.
    /// </exception>
    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");

        var deposit = new Transaction(amount, date, note);
        _allTransactions.Add(deposit);
    }

    /// <summary>
    /// Снимает деньги со счёта. Проверяет минимальный баланс и начисляет VIP-кэшбэк.
    /// </summary>
    /// <param name="amount">Сумма снятия. Должна быть положительной.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий.</param>
    /// <exception cref="ArgumentOutOfRangeException">Если сумма ≤ 0.</exception>
    /// <exception cref="InvalidOperationException">Если недостаточно средств.</exception>
    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        Transaction? overdraftTransaction =
            CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        Transaction? withdrawal = new(-amount, date, note);

        _allTransactions.Add(withdrawal);

        if (overdraftTransaction is not null)
            _allTransactions.Add(overdraftTransaction);

        if (Status == ClientStatus.VIP)
            MakeDeposit(amount * 0.05m, date, "VIP cashback");
    }

    /// <summary>
    /// Проверяет, превышает ли операция минимально допустимый баланс.
    /// </summary>
    /// <param name="isOverdrawn">Признак превышения лимита.</param>
    /// <returns>Транзакцию овердрафта или null, если превышения нет.</returns>
    /// <exception cref="InvalidOperationException">Если баланс уходит ниже минимума.</exception>
    protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
    {
        if (isOverdrawn)
            throw new InvalidOperationException("Not sufficient funds for this withdrawal");
        return default;
    }

    /// <summary>
    /// Возвращает текстовую историю всех транзакций по счёту.
    /// </summary>
    /// <returns>Строка с таблицей транзакций (дата, сумма, баланс, примечание).</returns>
    public string GetAccountHistory()
    {
        var report = new StringBuilder();
        decimal balance = 0;
        report.AppendLine("Date\t\tAmount\tBalance\tNote");
        foreach (var item in _allTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Note}");
        }
        return report.ToString();
    }

    /// <summary>
    /// Выполняет ежемесячные операции по счёту в зависимости от статуса клиента.
    /// </summary>
    public virtual void PerformMonthAndTransactions()
    {
        switch (Status)
        {
            case ClientStatus.Regular:
                if (Balance >= 50m)
                    MakeWithdrawal(50m, DateTime.UtcNow, "Service fee");
                break;

            case ClientStatus.Premium:
                if (Balance > 10000m)
                {
                    decimal interest = 0.01m * (Balance - 10000m);
                    MakeDeposit(interest, DateTime.UtcNow, "Premium interest");
                }
                break;

            case ClientStatus.VIP:
                if (Balance > 10000m)
                {
                    decimal interest = 0.03m * (Balance - 10000m);
                    MakeDeposit(interest, DateTime.UtcNow, "VIP interest");
                }
                break;
        }
    }

    /// <summary>
    /// Возвращает строковое описание счёта.
    /// </summary>
    /// <returns>Информация о владельце, номере и типе счёта.</returns>
    public override string ToString()
        => $"Owner: {Owner}\taccount number: {Number} (тип счета {GetType()})";
}