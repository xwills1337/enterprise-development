using AutoMapper;
using RealEstateAgency.Domain;
using RealEstateAgency.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Server.DTO;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RealEstateController(IRepository<RealEstate> repository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех объектов недвижимости
    /// </summary>
    /// <returns>Список всех объектов недвижимости и http status</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RealEstate>>> Get()
    {
        var realestates = await repository.GetAll();

        return Ok(realestates);
    }

    /// <summary>
    /// Возвращает объект недвижимости по указанному идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта недвижимости</param>
    /// <returns>Объект недвижимости и http status</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RealEstate>> Get(int id)
    {
        var realEstate = await repository.Get(id);

        if (realEstate == null)
            return NotFound();

        return Ok(realEstate);
    }

    /// <summary>
    /// Добавляет объект недвижимости с указанным идентификатором в коллекцию
    /// </summary>
    /// <param name="value">Экземпляр, добавляемый в коллекцию</param>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RealEstateDto value)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var realEstate = mapper.Map<RealEstate>(value);
        await repository.Post(realEstate);

        return Ok();
    }

    /// <summary>
    /// Заменяет объект недвижимости с указанным идентификатором в коллекции
    /// </summary>
    /// <param name="id">Идентификатор объекта недвижимости</param>
    /// <param name="value">Экземпляр, заменяющий старый экземпляр в коллекции</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] RealEstateDto value)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingRealEstate = await repository.Get(id);
        if (existingRealEstate == null)
            return NotFound();

        var realEstate = mapper.Map<RealEstate>(value);
        realEstate.Id = id;
        await repository.Put(realEstate, id);

        return Ok();
    }

    /// <summary>
    /// Удаляет объект недвижимости с указанным идентификатором из коллекции
    /// </summary>
    /// <param name="id">Идентификатор объекта недвижимости</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var realEstate = await repository.Get(id);
        if (realEstate == null)
            return NotFound();

        await repository.Delete(id);
        return Ok();
    }
}
