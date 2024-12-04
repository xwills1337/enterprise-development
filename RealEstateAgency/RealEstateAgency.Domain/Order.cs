namespace RealEstateAgency.Domain;

public enum TransactionType
{
    Purchase,
    Sale
}

/// <summary>
/// Заявка клиента
/// </summary>
public class Order
{
    /// <summary>
    /// Идентификатор заявки
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Время заявки
    /// </summary>
    public DateTime Time { get; set; }

    /// <summary>
    /// Клиент, сделавший заявку
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Тип заявки (покупка или продажа)
    /// </summary>
    public TransactionType Type { get; set; }

    /// <summary>
    /// Цена недвижимости
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Объект недвижимости
    /// </summary>
    public required RealEstate Item { get; set; }
}