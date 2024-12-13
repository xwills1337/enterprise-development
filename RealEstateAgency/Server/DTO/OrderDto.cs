using RealEstateAgency.Domain;

namespace Server.DTO;

public class OrderDto
{
    /// <summary>
    /// Время заявки
    /// </summary>
    public DateTime Time { get; set; }

    /// <summary>
    /// Клиент, сделавший заявку
    /// </summary>
    public required int ClientId { get; set; }

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
    public required int RealEstateId { get; set; }
}
