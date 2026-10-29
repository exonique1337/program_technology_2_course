namespace BankAccounts.Core.Withdrawals;

// 

public interface IWithdrawalPolicy
{
    WithdrawalDecision Evaulate(decimal currentBalance, decimal amount, decimal minimalBalance);
}
