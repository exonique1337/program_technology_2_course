namespace Bank;

/// <summary>
/// Неизменяемая запись о банковской операции.
/// </summary>
/// <param name="Amount">Сумма операции: положительная — приход, отрицательная — расход.</param>
/// <param name="Date">Дата и время проведения операции.</param>
/// <param name="Note">Текстовое примечание к операции.</param>
public record Transaction(decimal Amount, DateTime Date, string Note);