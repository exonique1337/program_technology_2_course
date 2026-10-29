
namespace BankAccounts.Core.Transactions;


// Простой DTO (data transfer object): сумма, дата, примечание.
public sealed class Transaction(decimal amount, DateTime date, string note);