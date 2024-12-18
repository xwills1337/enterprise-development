﻿using RealEstateAgency.Domain;
using RealEstateAgency.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Server.DTO;
using AutoMapper;

namespace Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QueryController(IRepository<Client> clientRepository, IRepository<Order> orderRepository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Вывести сведения о всех клиентах, ищущих недвижимость заданного типа, упорядочить по ФИО.
    /// </summary>
    [HttpGet("clients_by_realestate_type")]
    public async Task<ActionResult<IEnumerable<Client>>> GetClientsByRealEstateType(RealEstateType realEstateType)
    {
        var orders = await orderRepository.GetAll();

        var clients = (await clientRepository.GetAll())
            .Where(c => orders.Any(o => o.Client == c && o.Type == TransactionType.Purchase && o.Item.Type == realEstateType))
            .OrderBy(c => c.FullName)
            .ToList();

        return Ok(clients);
    }

    /// <summary>
    /// вывести всех продавцов, оставивших заявки за заданный период.
    /// </summary>
    [HttpGet("sellers-by-period")]
    public async Task<ActionResult<List<ClientDto>>> GetSellersByPeriod(DateTime start, DateTime end)
    {
        var orders = await orderRepository.GetAll();

        var sellers = (await clientRepository.GetAll())
            .Where(c => orders.Any(o => o.Client == c && o.Type == TransactionType.Sale && o.Time >= start && o.Time <= end))
            .ToList();
        return Ok(sellers);
    }

    /// <summary>
    /// Вывести сведения о продавцах и объектах недвижимости, соответствующих определенной заявке покупателя.
    /// </summary>
    [HttpGet("sellers_and_realestates_for_buyer_request")]
    public async Task<ActionResult<RealEstateAndSellersDto>> GetSellersAndRealEstatesForBuyerRequest(int buyerId)
    {
        var buyerOrder = await orderRepository.Get(buyerId);
        if (buyerOrder == null)
            return NotFound();
        if (buyerOrder.Type == TransactionType.Sale)
            return BadRequest("Неверный тип заявки.");

        var orders = await orderRepository.GetAll();
        var sellers = (await clientRepository.GetAll())
            .Where(s => orders.Any(o =>
                o.Type == TransactionType.Sale &&
                o.Client.Id == s.Id &&
                o.Item.Type == buyerOrder.Item.Type &&
                o.Item.Square == buyerOrder.Item.Square &&
                o.Item.NumberOfRooms == buyerOrder.Item.NumberOfRooms &&
                o.Price <= buyerOrder.Price))
            .ToList();
        
        var result = new RealEstateAndSellersDto
        {
            RealEstate = mapper.Map<RealEstateDto>(buyerOrder.Item),
            Sellers = mapper.Map<List<ClientDto>>(sellers)
        };

        return Ok(result);
    }

    /// <summary>
    /// Вывести информацию о количестве заявок по каждому типу недвижимости.
    /// </summary>
    [HttpGet("order_count_by_realestate_type")]
    public async Task<ActionResult<List<RealEstateTypeOrderCountDto>>> GetOrderCountByRealEstateType()
    {
        var result = (await orderRepository.GetAll())
            .GroupBy(o => o.Item.Type)
                .Select(group => new RealEstateTypeOrderCountDto
                {
                    RealEstateType = group.Key,
                    OrderCount = group.Count()
                })
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// Вывести топ 5 клиентов по количеству заявок на покупку.
    /// </summary>
    [HttpGet("top_5_purchasers_by_order_count")]
    public async Task<ActionResult<List<ClientOrderCountDto>>> GetTop5Buyers()
    {
        var orders = await orderRepository.GetAll();
        var top5Clients = (await clientRepository.GetAll())
            .OrderByDescending(c => orders
                .Count(o => o.Client.Id == c.Id && o.Type == TransactionType.Purchase))
            .Take(5)
            .ToList();

        var result = top5Clients.Select(client => new ClientOrderCountDto
        {
            Client = mapper.Map<ClientDto>(client),
            OrderCount = orders.Count(o => o.Client.Id == client.Id && o.Type == TransactionType.Purchase)
        }).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Вывести топ 5 клиентов по количеству заявок на продажу.
    /// </summary>
    [HttpGet("top_5_sellers_by_order_count")]
    public async Task<ActionResult<List<ClientOrderCountDto>>> GetTop5Sellers()
    {
        var orders = await orderRepository.GetAll();
        var top5Clients = (await clientRepository.GetAll())
            .OrderByDescending(c => orders
                .Count(o => o.Client.Id == c.Id && o.Type == TransactionType.Sale))
            .Take(5)
            .ToList();

        var result = top5Clients.Select(client => new ClientOrderCountDto
        {
            Client = mapper.Map<ClientDto>(client),
            OrderCount = orders.Count(o => o.Client.Id == client.Id && o.Type == TransactionType.Sale)
        }).ToList();

        return Ok(result);
    }

    /// <summary>
    /// Вывести информацию о клиентах, открывших заявки с минимальной стоимостью.
    /// </summary>
    [HttpGet("min_order_price")]
    public async Task<ActionResult<List<ClientOrderMinPriceDto>>> GetClientsWithMinOrderPrice()
    {
        var orders = await orderRepository.GetAll();
        var minPrice = orders.Min(o => o.Price);

        var clientsWithMinPrice = (await clientRepository.GetAll())
        .Where(c => orders.Any(o => o.Client.Id == c.Id && o.Price == minPrice))
        .ToList();

        var result = clientsWithMinPrice.Select(client => new ClientOrderMinPriceDto
        {
            Client = mapper.Map<ClientDto>(client),
            OrderMinPrice = minPrice
        }).ToList();

        return Ok(result);
    }
}
