using RealEstateAgency.Domain;

namespace RealEstateAgency.Tests;

public class RealEstateAgencyTests
{
    /// <summary>
    /// Проверка: Вывести сведения о всех клиентах, ищущих недвижимость заданного типа, упорядочить по ФИО
    /// </summary>
    [Fact]
    public void GetClientsByRealEstateType_SortedByFullName()
    {
        var realEstateType = RealEstate.RealEstateType.Commercial;

        var clients = TestData.Clients
            .Where(c => TestData.Orders.Any(o => o.Client == c && o.Type == Order.TransactionType.Purchase && o.Item.Type == realEstateType))
            .OrderBy(c => c.FullName)
            .ToList();

        Assert.NotEmpty(clients);
        Assert.Equal("Vasilieva Olga", clients[0].FullName);
    }

    /// <summary>
    /// Проверка: Вывести всех продавцов, оставивших заявки за заданный период
    /// </summary>
    [Fact]
    public void GetSellersByPeriod()
    {
        var start = new DateTime(2024, 1, 1);
        var end = new DateTime(2024, 12, 31);

        var sellers = TestData.Clients
            .Where(c => TestData.Orders.Any(o => o.Client == c && o.Type == Order.TransactionType.Sale && o.Time >= start && o.Time <= end))
            .ToList();

        Assert.NotEmpty(sellers);
        Assert.Contains(sellers, s => s.FullName == "Karpov Pavel");
        Assert.Contains(sellers, s => s.FullName == "Smirnov Andrey");
        Assert.Contains(sellers, s => s.FullName == "Vasilieva Olga");
        Assert.Contains(sellers, s => s.FullName == "Korolev Yuri");
        Assert.Contains(sellers, s => s.FullName == "Nikolaev Ivan");
    }

    /// <summary>
    /// Проверка: Вывести сведения о продавцах и объектах недвижимости, соответствующих определенной заявке покупателя
    /// </summary>
    [Fact]
    public void GetSellersAndRealEstatesForBuyerRequest()
    {
        var realEstateType = RealEstate.RealEstateType.Commercial;
        var maxPrice = 900000;

        var sellers = TestData.Clients
            .Join(
                TestData.Orders.Where(o => o.Type == Order.TransactionType.Sale
                                        && o.Item.Type == realEstateType
                                        && o.Price <= maxPrice),
                seller => seller.Id,
                saleOrder => saleOrder.Client.Id,
                (seller, saleOrder) => new
                {
                    Seller = seller,
                    Property = saleOrder.Item
                })
            .ToList();

        Assert.NotEmpty(sellers);
        Assert.Equal("Karpov Pavel", sellers[0].Seller.FullName);
    }

    /// <summary>
    /// Проверка: Вывести информацию о количестве заявок по каждому типу недвижимости
    /// </summary>
    [Fact]
    public void GetOrderCountByRealEstateType()
    {
        var orderCountByType = TestData.Orders
            .GroupBy(o => o.Item.Type)
            .Select(g => new
            {
                RealEstateType = g.Key,
                OrderCount = g.Count()
            })
            .ToList();

        var residentialCount = orderCountByType.FirstOrDefault(t => t.RealEstateType == RealEstate.RealEstateType.Residential);
        var commercialCount = orderCountByType.FirstOrDefault(t => t.RealEstateType == RealEstate.RealEstateType.Commercial);

        Assert.NotNull(residentialCount);
        Assert.NotNull(commercialCount);

        Assert.Equal(7, residentialCount.OrderCount);
        Assert.Equal(3, commercialCount.OrderCount);
    }

    /// <summary>
    /// Проверка: Вывести топ 5 клиентов по количеству заявок (отдельно на покупку и продажу)
    /// </summary>
    [Fact]
    public void GetTop5ClientsByOrderCount()
    {
        var purchaseOrders = TestData.Clients
            .OrderByDescending(c => TestData.Orders.Count(o => o.Client == c && o.Type == Order.TransactionType.Purchase))
            .Take(5)
            .ToList();

        var saleOrders = TestData.Clients
            .OrderByDescending(c => TestData.Orders.Count(o => o.Client == c && o.Type == Order.TransactionType.Sale))
            .Take(5)
            .ToList();

        Assert.NotEmpty(purchaseOrders);
        Assert.Contains(purchaseOrders, p => p.FullName == "Karpov Pavel");

        Assert.NotEmpty(saleOrders);
        Assert.Contains(saleOrders, s => s.FullName == "Karpov Pavel");
    }

    /// <summary>
    /// Проверка: Вывести информацию о клиентах, открывших заявки с минимальной стоимостью
    /// </summary>
    [Fact]
    public void GetClientsWithMinOrderPrice()
    {
        var minPrice = TestData.Orders.Min(o => o.Price);

        var clientsWithMinPrice = TestData.Clients
            .Where(c => TestData.Orders.Any(o => o.Client == c && o.Price == minPrice))
            .ToList();

        Assert.NotEmpty(clientsWithMinPrice);
        Assert.Equal(600000, minPrice);
        Assert.Contains(clientsWithMinPrice, c => c.FullName == "Vasilieva Olga");
    }
}