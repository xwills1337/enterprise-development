namespace Server.DTO;

public class ClientDto
{
    /// <summary>
    /// ФИО клиента
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Паспорт
    /// </summary>
    public required string Passport { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Адрес прописки
    /// </summary>
    public required string Address { get; set; }
}
