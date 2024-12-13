using AutoMapper;
using RealEstateAgency.Domain;
using RealEstateAgency.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Server.DTO;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IRepository<Order> repository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех заявок
    /// </summary>
    /// <returns>Список всех заявок и http status</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Order>> Get()
    {
        return Ok(repository.GetAll());
    }

    /// <summary>
    /// Возвращает заявку по указанному идентификатору
    /// </summary>
    /// <param name="id">Идентификатор заявки</param>
    /// <returns>Заявка и http status</returns>
    [HttpGet("{id}")]
    public ActionResult<Order> Get(int id)
    {
        var order = repository.Get(id);

        if (order == null)
            return NotFound();

        return Ok(order);
    }

    /// <summary>
    /// Добавляет заявку с указанным идентификатором в коллекцию
    /// </summary>
    /// <param name="value">Экземпляр, добавляемый в коллекцию</param>
    [HttpPost]
    public IActionResult Post([FromBody] OrderDto value)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var order = mapper.Map<Order>(value);
        repository.Post(order);

        return Ok();
    }

    /// <summary>
    /// Заменяет заявку с указанным идентификатором в коллекции
    /// </summary>
    /// <param name="id">Идентификатор заявки</param>
    /// <param name="value">Экземпляр, заменяющий старый экземпляр в коллекции</param>
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] OrderDto value)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var order = mapper.Map<Order>(value);
        order.Id = id;

        if (!repository.Put(order, id))
            return NotFound();

        return Ok();
    }

    /// <summary>
    /// Удаляет заявку с указанным идентификатором из коллекции
    /// </summary>
    /// <param name="id">Идентификатор заявки</param>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!repository.Delete(id))
            return NotFound();

        return Ok();
    }
}