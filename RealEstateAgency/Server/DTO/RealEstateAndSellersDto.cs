namespace Server.DTO;

public class RealEstateAndSellersDto
{
    /// <summary>
    /// Объект недвижимости
    /// </summary>
    public RealEstateDto? RealEstate { get; set; }

    /// <summary>
    /// Список продавцов, продающих этот объект недвижимости
    /// </summary>
    public List<ClientDto>? Sellers { get; set; }
}
