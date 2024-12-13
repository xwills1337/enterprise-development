using RealEstateAgency.Domain;
namespace Server.DTO;

public class RealEstateTypeOrderCountDto
{
    /// <summary>
    /// Тип объекта недвижимости (жилое/нежилое)
    /// </summary>
    public RealEstateType RealEstateType { get; set; }

    /// <summary>
    /// Количество заявок с данным типом объекта
    /// </summary>
    public int OrderCount { get; set; }
}
