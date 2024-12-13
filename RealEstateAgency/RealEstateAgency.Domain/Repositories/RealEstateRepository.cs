namespace RealEstateAgency.Domain.Repositories;

public class RealEstateRepository : IRepository<RealEstate>
{
    private readonly List<RealEstate> _realestates = [];
    private int _id = 1;

    public List<RealEstate> GetAll() => _realestates;

    public RealEstate? Get(int id) => _realestates.Find(s => s.Id == id);

    public void Post(RealEstate obj)
    {
        obj.Id = _id++;
        _realestates.Add(obj);
    }

    public bool Put(RealEstate obj, int id)
    {
        var oldRealEstate = Get(id);
        if (oldRealEstate == null)
            return false;
        oldRealEstate.Id = obj.Id;
        oldRealEstate.Type = obj.Type;
        oldRealEstate.Address = obj.Address;
        oldRealEstate.Square = obj.Square;
        oldRealEstate.NumberOfRooms = obj.NumberOfRooms;
        return true;
    }

    public bool Delete(int id)
    {
        var deletedRealEstate = Get(id);
        if (deletedRealEstate == null)
            return false;
        _realestates.Remove(deletedRealEstate);
        return true;
    }
}
