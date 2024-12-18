using Microsoft.EntityFrameworkCore;

namespace RealEstateAgency.Domain.Repositories;

public class ClientRepository(RealEstateAgencyContext context) : IRepository<Client>
{
    public async Task<List<Client>> GetAll() => await context.Clients.ToListAsync();

    public async Task<Client?> Get(int id) => await context.Clients.FirstOrDefaultAsync(c => c.Id == id);

    public async Task Post(Client obj)
    {
        await context.Clients.AddAsync(obj);
        await context.SaveChangesAsync();
    }

    public async Task Put(Client obj, int id)
    {
        var oldClient = await Get(id);
        if (oldClient == null)
            return;

        oldClient.FullName = obj.FullName;
        oldClient.Passport = obj.Passport;
        oldClient.Phone = obj.Phone;
        oldClient.Address = obj.Address;
        context.Clients.Update(oldClient);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var deletedClient = await Get(id);
        if (deletedClient == null)
            return;

        context.Clients.Remove(deletedClient);
        await context.SaveChangesAsync();
    }
}