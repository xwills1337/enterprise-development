using Microsoft.EntityFrameworkCore;

namespace RealEstateAgency.Domain.Repositories;

public class RealEstateRepository(RealEstateAgencyContext context) : IRepository<RealEstate>
{
    public async Task<List<RealEstate>> GetAll() => await context.RealEstates.ToListAsync();

    public async Task<RealEstate?> Get(int id) => await context.RealEstates.FirstOrDefaultAsync(r => r.Id == id);

    public async Task Post(RealEstate obj)
    {
        await context.RealEstates.AddAsync(obj);
        await context.SaveChangesAsync();
    }

    public async Task Put(RealEstate obj, int id)
    {
        var oldRealEstate = await Get(id);
        if (oldRealEstate == null)
            return;

        oldRealEstate.Type = obj.Type;
        oldRealEstate.Address = obj.Address;
        oldRealEstate.Square = obj.Square;
        oldRealEstate.NumberOfRooms = obj.NumberOfRooms;
        context.RealEstates.Update(oldRealEstate);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var deletedRealEstate = await Get(id);
        if (deletedRealEstate == null)
            return;

        context.RealEstates.Remove(deletedRealEstate);
        await context.SaveChangesAsync();
    }
}
