using AutoMapper;
using RealEstateAgency.Domain;
using RealEstateAgency.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Server.DTO;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(
    IRepository<Order> repository, 
    IRepository<Client> clientRepository,
    IRepository<RealEstate> realestateRepository, 
    IMapper mapper
    ) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех заявок
    /// </summary>
    /// <returns>Список всех заявок и http status</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> Get()
    {
        var orders = await repository.GetAll();

        return Ok(orders);
    }

    /// <summary>
    /// Возвращает заявку по указанному идентификатору
    /// </summary>
    /// <param name="id">Идентификатор заявки</param>
    /// <returns>Заявка и http status</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> Get(int id)
    {
        var order = await repository.Get(id);

        if (order == null)
            return NotFound();

        return Ok(order);
    }

    /// <summary>
    /// Добавляет заявку с указанным идентификатором в коллекцию
    /// </summary>
    /// <param name="value">Экземпляр, добавляемый в коллекцию</param>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] OrderDto value)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var order = mapper.Map<Order>(value);
        var client = await clientRepository.Get(value.ClientId);
        if (client == null)
            return NotFound();

        var realEstate = await realestateRepository.Get(value.RealEstateId);
        if (realEstate == null)
            return NotFound();

        order.Client = client;
        order.Item = realEstate;
        await repository.Post(order);

        return Ok();
    }

    /// <summary>
    /// Заменяет заявку с указанным идентификатором в коллекции
    /// </summary>
    /// <param name="id">Идентификатор заявки</param>
    /// <param name="value">Экземпляр, заменяющий старый экземпляр в коллекции</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] OrderDto value)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingOrder = await repository.Get(id);
        if (existingOrder == null)
            return NotFound();

        var order = mapper.Map<Order>(value);
        order.Id = id;
        var client = await clientRepository.Get(value.ClientId);
        if (client == null)
            return NotFound();

        var realEstate = await realestateRepository.Get(value.RealEstateId);
        if (realEstate == null)
            return NotFound();

        order.Client = client;
        order.Item = realEstate;
        await repository.Put(order, id);

        return Ok();
    }

    /// <summary>
    /// Удаляет заявку с указанным идентификатором из коллекции
    /// </summary>
    /// <param name="id">Идентификатор заявки</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await repository.Get(id);
        if (order == null)
            return NotFound();

        await repository.Delete(id);

        return Ok();
    }
}