namespace Server.DTO;

public class ClientOrderCountDto
{
    /// <summary>
    /// Клиент
    /// </summary>
    public ClientDto? Client { get; set; }

    /// <summary>
    /// Количество заявок клиента
    /// </summary>
    public int OrderCount { get; set; }
}
