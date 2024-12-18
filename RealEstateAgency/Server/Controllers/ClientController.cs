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
    public async Task<ActionResult<IEnumerable<Client>>> Get()
    {
        var clients = await repository.GetAll();
        return Ok(clients);
    }

    /// <summary>
    /// ���������� ������� �� ���������� ��������������
    /// </summary>
    /// <param name="id">������������� �������</param>
    /// <returns>������ � http status</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> Get(int id)
    {
        var client = await repository.Get(id);
        if (client == null)
            return NotFound();

        return Ok(client);
    }

    /// <summary>
    /// ��������� ������� � ��������� ��������������� � ���������
    /// </summary>
    /// <param name="value">���������, ����������� � ���������</param>
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
    /// �������� ������� � ��������� ��������������� � ���������
    /// </summary>
    /// <param name="id">������������� �������</param>
    /// <param name="value">���������, ���������� ������ ��������� � ���������</param>
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
    /// ������� ������� � ��������� ��������������� �� ���������
    /// </summary>
    /// <param name="id">������������� �������</param>
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
