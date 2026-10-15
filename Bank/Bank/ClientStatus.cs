namespace Bank;

/// <summary>
/// Статус клиента банка. Влияет на комиссии, проценты и кэшбэк.
/// </summary>
public enum ClientStatus
{
    /// <summary>Обычный клиент. Комиссия за обслуживание 50 руб/мес.</summary>
    Regular,

    /// <summary>Премиум-клиент. Комиссии нет, 1% на остаток свыше 10 000.</summary>
    Premium,

    /// <summary>VIP-клиент. Комиссии нет, 3% на остаток свыше 10 000, кэшбэк 5% с покупок.</summary>
    VIP
}