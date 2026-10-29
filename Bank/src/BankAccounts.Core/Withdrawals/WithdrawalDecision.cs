using System.Data.SqlTypes;

namespace BankAccounts.Core.Withdrawals;

// Единый контракт для всех политик снятия:
// Возвращаем решение, а не выбрасываем исключения
// Это устраняет LSP между базовым и переопределенным методом.

public class WithdrawalDecision(bool IsAllowed, decimal Fee, string? RejectionReason)
{

    public static WithdrawalDecision Allow() => new(true, 0m, null);
    public static WithdrawalDecision AllowWithFree(decimal fee) => new(true, fee, null);

    public static WithdrawalDecision Reject(string reason) => new(false, 0m, reason);
    void f()
    {
        IsAllowed = true;
    }
}
