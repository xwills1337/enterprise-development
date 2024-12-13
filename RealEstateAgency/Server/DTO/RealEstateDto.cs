using RealEstateAgency.Domain;
namespace Server.DTO;

public class RealEstateDto
{
    /// <summary>
    /// Тип объекта недвижимости (жилое/нежилое)
    /// </summary>
    public RealEstateType Type { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Площадь
    /// </summary>
    public double Square { get; set; }

    /// <summary>
    /// Количество комнат
    /// </summary>
    public int NumberOfRooms { get; set; }
}
