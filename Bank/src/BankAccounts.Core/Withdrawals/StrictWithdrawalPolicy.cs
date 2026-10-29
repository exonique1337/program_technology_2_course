namespace BankAccounts.Core.Withdrawals;

// Политика по умолчанию: если баланс ниже минимума — отклоняем
public sealed class StrictWithdrawalPolicy : IWithdrawalPolicy
{
    public WithdrawalDecision Evaulate(decimal currentBalance, decimal amount, decimal minimalBalance)
    {
        return currentBalance - amount < minimalBalance
            ? WithdrawalDecision.Reject("Not sufficient rubls for this withdrawal")
            : WithdrawalDecision.Allow();
    }
}