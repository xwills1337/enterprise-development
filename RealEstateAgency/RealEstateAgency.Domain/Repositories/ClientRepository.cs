namespace RealEstateAgency.Domain.Repositories;

public class ClientRepository : IRepository<Client>
{
    private readonly List<Client> _clients = [];
    private int _id = 1;

    public List<Client> GetAll() => _clients;

    public Client? Get(int id) => _clients.Find(s => s.Id == id);

    public void Post(Client obj)
    {
        obj.Id = _id++;
        _clients.Add(obj);
    }

    public bool Put(Client obj, int id)
    {
        var oldClient = Get(id);
        if (oldClient == null)
            return false;
        oldClient.Id = obj.Id;
        oldClient.FullName = obj.FullName;
        oldClient.Passport = obj.Passport;
        oldClient.Phone = obj.Phone;
        oldClient.Address = obj.Address;
        return true;
    }

    public bool Delete(int id)
    {
        var deletedClient = Get(id);
        if (deletedClient == null)
            return false;
        _clients.Remove(deletedClient);
        return true;
    }
}