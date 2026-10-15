# Отчёт по практике «Развиваем банковское приложение» бла бла бла....

Ветка: `bank-before-solid` /// пу-пу-пу я устал босс....

---

## Задача 0 а именно Документация всего приложения

Все публичные и `protected` члены классов `BankAccount`, `GiftCardAccount`,
`InterestEarningAccount`, `LineOfCreditAccount`, `Transaction` покрыты
XML-документацией. Проект собирается без предупреждений `CS1591`.

**Методы, у которых `<summary>` пришлось писать через «и»:**

1. `BankAccount.MakeWithdrawal` — «снимает деньги **и** проверяет
   минимальный баланс **и** начисляет VIP-кэшбэк **и** записывает
   операцию в аудит».
2. `BankAccount.PerformMonthAndTransactions` — «начисляет проценты
   **и** списывает комиссию **и** зависит от статуса клиента».
3. `GiftCardAccount.PerformMonthAndTransactions` — «пополняет счёт
   **и** записывает операцию в аудит».
4. `InterestEarningAccount.PerformMonthAndTransactions` — «начисляет
   проценты **и** записывает операцию в аудит».
5. `LineOfCreditAccount.PerformMonthAndTransactions` — «начисляет
   проценты **и** записывает операцию в аудит».

Всего таких методов: **5**.

---

## Задача 1. Скидка на обслуживание

**Пришлось ли править метод в базовом классе?**
Да. `PerformMonthAndTransactions` в `BankAccount` теперь `switch`
по `Status`. Также добавлено свойство `Status` и VIP-кэшбэк
в `MakeWithdrawal`.

**Пришлось ли дублировать логику из `InterestEarningAccount`?**
Да. `InterestEarningAccount` начисляет 2% на остаток > 500.
В базовом классе Premium получает 1% на остаток > 10 000, VIP — 3%.
Схожая логика, но живёт в разных местах.

**Что будет, если завтра появится ещё один статус — `Student`?**
Придётся:
1. добавить значение в `ClientStatus`;
2. добавить `case` в `switch` базового `BankAccount`;
3. проверить, что наследники (`GiftCardAccount`, `InterestEarningAccount`,
   `LineOfCreditAccount`) не переопределяют `PerformMonthAndTransactions`
   так, что новый статус потеряется.

**Сколько мест в коде пришлось изменить, чтобы добавить один
новый статус?**
**3 места:** `ClientStatus.cs`, `BankAccount.cs`, `Program.cs`.

---

## Задача 2. Аудит операций

**Куда добавил вызов `AuditLogs.Add`:**
- `BankAccount.MakeDeposit` — тип `"Deposit"` (или переданный извне);
- `BankAccount.MakeWithdrawal` — тип `"Withdrawal"` (или переданный
  извне);
- неявно через `MakeDeposit`/`MakeWithdrawal` — во всех наследниках:
  - `GiftCardAccount.PerformMonthAndTransactions` (тип `"Deposit"`),
  - `InterestEarningAccount.PerformMonthAndTransactions` (тип `"Interest"`),
  - `LineOfCreditAccount.PerformMonthAndTransactions` (тип `"Interest"`),
  - VIP-кэшбэк внутри `MakeWithdrawal` (тип `"Cashback"`),
  - комиссия Regular (тип `"Fee"`),
  - проценты Premium/VIP (тип `"Interest"`).

**Сколько мест изменил?**
2 метода в базе (`MakeDeposit`, `MakeWithdrawal`) + добавлен параметр
`type`. Наследников правил только в вызовах — 3 файла.

**Риск забыть при следующей правке?**
Есть. Любой новый вид транзакции (например, `Transfer` в Задаче 3)
обязан пройти через `MakeDeposit`/`MakeWithdrawal`, иначе в аудит
не попадёт. Компилятор этого не проверит.

**Легко ли потом убрать аудит?**
Нет. Придётся искать все вызовы `AuditLogs.Add` по проекту, удалять
параметры `type` из сигнатур и править всех вызывающих.

---

## Задача 3. Перевод между счетами

**Куда поместил метод и почему?**
В отдельный статический класс `TransferBetweenAccounts`.
Если бы поместил в `BankAccount`, счёт начал бы «знать» о других
счетах — это размывает ответственность класса.

**Что пришлось изменить в других классах?**
- в `BankAccount.MakeDeposit` и `MakeWithdrawal` добавлен
  необязательный параметр `type` — чтобы различать `Transfer`
  в аудите;
- `Program.cs` — добавлена демонстрация перевода.

**Проблемы, которые заметил:**
- Метод **не атомарный**: если `to.MakeDeposit` упадёт с исключением,
  списание с `from` уже прошло, и откатить его нельзя.
- В `Transaction` нет поля, связывающего парные операции
  («списал 50, зачислил 50»). Две записи в аудите никак не связаны.
- Статический метод тестировать сложнее, чем сервис с зависимостями.
---