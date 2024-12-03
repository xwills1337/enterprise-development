namespace RealEstateAgency.Domain;

/// <summary>
/// заявка клиента
/// </summary>
public class Order
{
    /// <summary>
    /// идентификатор заявки
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// время заявки
    /// </summary>
    public DateTime Time { get; set; }

    /// <summary>
    /// клиент, сделавший заявку
    /// </summary>
    public required Client Client { get; set; }

    public enum TransactionType
    {
        Purchase,
        Sale
    }

    /// <summary>
    /// тип заявки (покупка или продажа)
    /// </summary>
    public TransactionType Type { get; set; }

    /// <summary>
    /// цена недвижимости
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// объект недвижимости
    /// </summary>
    public required RealEstate Item { get; set; }
}