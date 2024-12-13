namespace Server.DTO;

public class ClientOrderMinPriceDto
{
    /// <summary>
    /// Клиент
    /// </summary>
    public ClientDto? Client { get; set; }

    /// <summary>
    /// Минимальная стоимость заявки
    /// </summary>
    public decimal OrderMinPrice { get; set; }
}
