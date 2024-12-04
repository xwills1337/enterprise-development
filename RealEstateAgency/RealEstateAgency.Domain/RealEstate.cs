namespace RealEstateAgency.Domain;

public enum RealEstateType
{
    Residential,
    Commercial
}

/// <summary>
/// Объект недвижимости
/// </summary>
public class RealEstate
{
    /// <summary>
    /// Идентификатор объекта
    /// </summary>
    public int Id { get; set; }

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
