namespace BankAccounts.Core.Time;

// Реализация для production берет реальное время!
public sealed class SystemClock : IClock
{
  public DateTime Now => DateTime.UtcNow;
}
