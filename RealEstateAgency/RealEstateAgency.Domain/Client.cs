namespace RealEstateAgency.Domain;

/// <summary>
/// Клиент риэлторского агентства
/// </summary>
public class Client
{
    /// <summary>
    /// идентификатор клиента
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// фио клиента
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// паспорт
    /// </summary>
    public required string Passport { get; set; }

    /// <summary>
    /// номер телефона
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// адрес прописки
    /// </summary>
    public required string Address { get; set; }
}
