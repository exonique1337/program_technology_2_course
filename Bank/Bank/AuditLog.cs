using System.Text;

namespace Bank;

/// <summary>
/// Запись в журнале аудита банковских операций.
/// </summary>
/// <param name="Number">Номер счёта.</param>
/// <param name="Type">Тип операции: Deposit, Withdrawal, Interest, Fee, Cashback, Transfer.</param>
/// <param name="Amount">Сумма операции.</param>
/// <param name="Date">Дата операции.</param>
public record AuditLog(string Number, string Type, decimal Amount, DateTime Date);

/// <summary>
/// Глобальный журнал аудита всех операций всех счетов.
/// </summary>
public static class AuditLogs
{
    private static readonly List<AuditLog> _auditLog = new List<AuditLog>();

    /// <summary>
    /// Добавляет запись об операции в журнал аудита.
    /// </summary>
    /// <param name="number">Номер счёта.</param>
    /// <param name="type">Тип операции.</param>
    /// <param name="amount">Сумма операции.</param>
    /// <param name="date">Дата операции.</param>
    public static void Add(string number, string type, decimal amount, DateTime date)
    {
        _auditLog.Add(new AuditLog(number, type, amount, date));
    }

    /// <summary>
    /// Возвращает журнал аудита в виде текстовой таблицы.
    /// </summary>
    /// <returns>Строка со всеми записями аудита.</returns>
    public static string GetAuditLogs()
    {
        var log = new StringBuilder();
        log.AppendLine("Number\t\tType\t\tAmount\t\tDate");
        foreach (var item in _auditLog)
        {
            log.AppendLine($"{item.Number}\t{item.Type}\t\t{item.Amount}\t\t{item.Date.ToShortDateString()}");
        }
        return log.ToString();
    }
}