namespace RealEstateAgency.Domain.Repositories;

public class OrderRepository : IRepository<Order>
{
    private readonly List<Order> _orders = [];
    private int _id = 1;

    public List<Order> GetAll() => _orders;

    public Order? Get(int id) => _orders.Find(s => s.Id == id);

    public void Post(Order obj)
    {
        obj.Id = _id++;
        _orders.Add(obj);
    }

    public bool Put(Order obj, int id)
    {
        var oldOrder = Get(id);
        if (oldOrder == null)
            return false;
        oldOrder.Id = obj.Id;
        oldOrder.Time = obj.Time;
        oldOrder.Client = obj.Client;
        oldOrder.Type = obj.Type;
        oldOrder.Price = obj.Price;
        oldOrder.Item = obj.Item;
        return true;
    }

    public bool Delete(int id)
    {
        var deletedOrder = Get(id);
        if (deletedOrder == null)
            return false;
        _orders.Remove(deletedOrder);
        return true;
    }
}
