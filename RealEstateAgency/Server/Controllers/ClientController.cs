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
    public ActionResult<IEnumerable<Client>> Get()
    {
        return Ok(repository.GetAll());
    }

    /// <summary>
    /// Возвращает клиента по указанному идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Клиент и http status</returns>
    [HttpGet("{id}")]
    public ActionResult<Client> Get(int id)
    {
        var client = repository.Get(id);

        if (client == null)
            return NotFound();

        return Ok(client);
    }

    /// <summary>
    /// Добавляет клиента с указанным идентификатором в коллекцию
    /// </summary>
    /// <param name="value">Экземпляр, добавляемый в коллекцию</param>
    [HttpPost]
    public IActionResult Post([FromBody] ClientDto value)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var client = mapper.Map<Client>(value);
        repository.Post(client);

        return Ok();
    }

    /// <summary>
    /// Заменяет клиента с указанным идентификатором в коллекции
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <param name="value">Экземпляр, заменяющий старый экземпляр в коллекции</param>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] ClientDto value)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var client = mapper.Map<Client>(value);
        client.Id = id;

        if (!repository.Put(client, id))
            return NotFound();

        return Ok();
    }

    /// <summary>
    /// Удаляет клиента с указанным идентификатором из коллекции
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!repository.Delete(id))
            return NotFound();

        return Ok();
    }
}
