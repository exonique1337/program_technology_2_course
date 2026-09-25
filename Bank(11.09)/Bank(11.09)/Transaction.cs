namespace Bank_11._09_;
///<summary>
/// Тип данных который запрещает менять состояние объекта
///</summary>
///<param name="Amount"> Сумма транзакций </param>
///<param name="Date"> Дата транзакции </param>
///<param name="Note"> Заметка транзакций </param>

internal record Transaction(decimal Amount, DateTime Date, string Note);       // record - тип данных записи неизменяемый 
