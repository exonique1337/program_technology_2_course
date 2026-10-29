namespace BankAccounts.Core.AccountNumbers;

// Данный интерфейс нужен для генерации номеров аккаунта - идентификатор!
// Генерация номеров - отдельная ответсвтенность, не должна быть внутри счета (SRP)
public interface IAccountNumberGenerator
{
    string Next();
}
