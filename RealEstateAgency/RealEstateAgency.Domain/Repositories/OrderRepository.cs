using Microsoft.EntityFrameworkCore;

namespace RealEstateAgency.Domain.Repositories;

public class OrderRepository(RealEstateAgencyContext context) : IRepository<Order>
{
    public async Task<List<Order>> GetAll() => await context.Orders.Include(o => o.Client).Include(o => o.Item).ToListAsync();

    public async Task<Order?> Get(int id) => await context.Orders.Include(o => o.Client).Include(o => o.Item).FirstOrDefaultAsync(o => o.Id == id);

    public async Task Post(Order obj)
    {
        await context.Orders.AddAsync(obj);
        await context.SaveChangesAsync();
    }

    public async Task Put(Order obj, int id)
    {
        var oldOrder = await Get(id);
        if (oldOrder == null)
            return;

        oldOrder.Time = obj.Time;
        oldOrder.Client = obj.Client;
        oldOrder.Type = obj.Type;
        oldOrder.Price = obj.Price;
        oldOrder.Item = obj.Item;
        context.Orders.Update(oldOrder);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var deletedOrder = await Get(id);
        if (deletedOrder == null)
            return;

        context.Orders.Remove(deletedOrder);
        await context.SaveChangesAsync();
    }
}
