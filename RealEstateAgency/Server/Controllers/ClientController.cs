using AutoMapper;
using RealEstateAgency.Domain;
using RealEstateAgency.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Server.DTO;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientController(IRepository<Client> repository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех клиентов
    /// </summary>
    /// <returns>Список всех клиентов и http status</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> Get()
    {
        var clients = await repository.GetAll();
        return Ok(clients);
    }

    /// <summary>
    /// Возвращает клиента по указанному идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Клиент и http status</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> Get(int id)
    {
        var client = await repository.Get(id);
        if (client == null)
            return NotFound();

        return Ok(client);
    }

    /// <summary>
    /// Добавляет клиента с указанным идентификатором в коллекцию
    /// </summary>
    /// <param name="value">Экземпляр, добавляемый в коллекцию</param>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ClientDto value)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var client = mapper.Map<Client>(value);
        await repository.Post(client);

        return Ok();
    }

    /// <summary>
    /// Заменяет клиента с указанным идентификатором в коллекции
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <param name="value">Экземпляр, заменяющий старый экземпляр в коллекции</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] ClientDto value)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingClient = await repository.Get(id);
        if (existingClient == null)
            return NotFound();

        var client = mapper.Map<Client>(value);
        client.Id = id;
        await repository.Put(client, id);

        return Ok();
    }

    /// <summary>
    /// Удаляет клиента с указанным идентификатором из коллекции
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var client = await repository.Get(id);
        if (client == null)
            return NotFound();

        await repository.Delete(id);
        return Ok();
    }
}
