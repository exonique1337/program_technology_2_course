
using System.Text;

namespace Bank_11._09_;

internal class BankAccount
{
    private List<Transaction> _alltransactions = new List<Transaction>();

    public string Owner { get; private set; }
    public string Number { get; }
    public decimal Balance
    {
        get
        {
            decimal balance = 0;
            foreach (var transaction in _alltransactions)
            {
                balance += transaction.Amount;
            }
            return balance;
        }
    }
    private static int s_accountNumberSeed = 100000000;


    public BankAccount(string name, decimal initialBalance)
    {
        MakeDeposite(initialBalance, DateTime.UtcNow, "initial balance");     //this.Balance = Balance;
        Owner = name;
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;
    }
    public void MakeDeposite(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException
                (nameof(amount), "Amount must be positive");
        }

        var deposite = new Transaction(amount, date, note);
        _alltransactions.Add(deposite);
    }
    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException
                (nameof(amount), "Amount of withdrawl be positive");
        }
        if (Balance < amount)
        {
            throw new InvalidOperationException
                ("Not sufficient money for this withdrawal");
        }
        var withdrowal = new Transaction(amount, date, note);
        _alltransactions.Add(withdrowal);
    }

    public string GetAccountHistory()
    {
        var report = new StringBuilder();

        decimal balance = 0;
        report.AppendLine("Date\t\tAmount\tBalance\tNote");
        foreach (var item in _alltransactions)
        {
            balance += item.Amount;
            report.AppendLine($"" =
                $"{item.Date.ToShortDateString()}\t" +
                $"{item.Amount}\t{balance}\t{item.Note}");
        }
        return report.ToString();
    }
}