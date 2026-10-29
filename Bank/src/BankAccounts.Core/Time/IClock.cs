
namespace BankAccounts.Core.Time;

// Выносим зависимости от времени в интерфейс,
// Чтобы можно подменить его в тестах (DIP)
public interface IClock
{
    DateTime Now { get; }
}
