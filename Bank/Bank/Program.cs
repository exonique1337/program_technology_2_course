using Bank;

namespace Bank
{
    internal class Program
    {
        static void Main()
        {
            BankAccount account1 = new BankAccount("Yana", 100000);
            BankAccount account2 = new BankAccount("Lena", 10);

            Console.WriteLine($"account {account1.Balance} №{account1.Number} {account1.Owner}");
            Console.WriteLine($"account {account2.Balance} №{account2.Number} {account2.Owner}");

            account1.MakeDeposit(2000000, DateTime.UtcNow, ":)");
            Console.WriteLine(account1.Balance);

            account1.MakeWithdrawal(200, DateTime.UtcNow, ":(");
            Console.WriteLine(account1.Balance);

            Console.WriteLine(account1.GetAccountHistory());

            try
            {
                account2.MakeWithdrawal(10000, DateTime.UtcNow, ":(");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }

            InterestEarningAccount interest = new InterestEarningAccount("Yana", 1000);
            interest.PerformMonthAndTransactions();
            Console.WriteLine(interest.GetAccountHistory());

            LineOfCreditAccount lineOfCredit = new LineOfCreditAccount("Yana", 0, 1000m);
            lineOfCredit.MakeWithdrawal(500m, DateTime.UtcNow, "credit");

            GiftCardAccount giftCard = new GiftCardAccount("Yana", 1000m, 5000m);

            List<BankAccount> accounts = new List<BankAccount>();
            accounts.Add(account1);
            accounts.Add(interest);
            accounts.Add(lineOfCredit);
            accounts.Add(giftCard);

            foreach (BankAccount account in accounts)
            {
                Console.WriteLine(account);
                account.PerformMonthAndTransactions();
                Console.WriteLine(account.GetAccountHistory());
            }

            lineOfCredit.MakeWithdrawal(600m, DateTime.UtcNow, "credit");
            Console.WriteLine(lineOfCredit.GetAccountHistory());

            // ===== Демонстрация статусов =====
            Console.WriteLine("=== ClientStatus ===");

            BankAccount regular = new BankAccount("Reg", 2000m);
            regular.Status = ClientStatus.Regular;
            regular.PerformMonthAndTransactions();
            Console.WriteLine($"Regular после месяца: {regular.Balance} (ожидаем 1950)");

            BankAccount premium = new BankAccount("Prem", 20000m);
            premium.Status = ClientStatus.Premium;
            premium.PerformMonthAndTransactions();
            Console.WriteLine($"Premium после месяца: {premium.Balance} (ожидаем 20100)");

            BankAccount vip = new BankAccount("Vip", 20000m);
            vip.Status = ClientStatus.VIP;
            vip.PerformMonthAndTransactions();
            vip.MakeWithdrawal(1000m, DateTime.UtcNow, "Покупка");
            Console.WriteLine($"VIP после месяца и покупки 1000: {vip.Balance} (ожидаем 20300)");
        }
    }
}