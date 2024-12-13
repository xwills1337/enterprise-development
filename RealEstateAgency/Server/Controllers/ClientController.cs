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
    /// ���������� ������ ���� ��������
    /// </summary>
    /// <returns>������ ���� �������� � http status</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Client>> Get()
    {
        return Ok(repository.GetAll());
    }

    /// <summary>
    /// ���������� ������� �� ���������� ��������������
    /// </summary>
    /// <param name="id">������������� �������</param>
    /// <returns>������ � http status</returns>
    [HttpGet("{id}")]
    public ActionResult<Client> Get(int id)
    {
        var client = repository.Get(id);

        if (client == null)
            return NotFound();

        return Ok(client);
    }

    /// <summary>
    /// ��������� ������� � ��������� ��������������� � ���������
    /// </summary>
    /// <param name="value">���������, ����������� � ���������</param>
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
    /// �������� ������� � ��������� ��������������� � ���������
    /// </summary>
    /// <param name="id">������������� �������</param>
    /// <param name="value">���������, ���������� ������ ��������� � ���������</param>
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
    /// ������� ������� � ��������� ��������������� �� ���������
    /// </summary>
    /// <param name="id">������������� �������</param>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!repository.Delete(id))
            return NotFound();

        return Ok();
    }
}
