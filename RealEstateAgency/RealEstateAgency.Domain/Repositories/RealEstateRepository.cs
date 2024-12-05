namespace RealEstateAgency.Domain.Repositories;

public class RealEstateRepository : IRepository<Realestate>
{
    private readonly List<RealEstate> _realestates = [];
    private int _id = 1;

    public List<Realestate> GetAll() => _realestates;

    public Realestate? Get(int id) => _realestates.Find(s => s.Id == id);

    public void Post(Realestate obj)
    {
        obj.Id = _id++;
        _realestates.Add(obj);
    }

    public bool Put(Realestate obj, int id)
    {
        var oldRealestate = Get(id);
        if (oldRealestate == null)
            return false;
        oldRealestate.Id = obj.Id;
        oldRealestate.Type = obj.Type;
        oldRealestate.Address = obj.Address;
        oldRealestate.Square = obj.Square;
        oldRealestate.NumberOfRooms = obj.NumberOfRooms;
        return true;
    }

    public bool Delete(int id)
    {
        var deletedRealestate = Get(id);
        if (deletedRealestate == null)
            return false;
        _realestates.Remove(deletedRealestate);
        return true;
    }
}
