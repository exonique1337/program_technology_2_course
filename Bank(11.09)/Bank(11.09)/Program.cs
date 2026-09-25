using System.Security.Principal;

namespace Bank_11._09_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount account1 = new BankAccount("Detskiy_Dom", 1000000000);
            BankAccount account2 = new BankAccount("NGASU", 10);

            Console.WriteLine ($"{account1.Owner} {account1.Balance} { account1.Number}");
            Console.WriteLine ($"{account2.Owner} { account2.Balance} { account2.Number}");

            account1.MakeDeposite(9000000, DateTime.UtcNow, ";)");
            Console.WriteLine($"Balance: {account1.Balance}");

            account1.MakeWithdrawal(123, DateTime.UtcNow, ";)");
            Console.WriteLine($"Balance: {account1.Balance}");

            try
            {
                account2.MakeWithdrawal(10000, DateTime.UtcNow, "asdas");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
