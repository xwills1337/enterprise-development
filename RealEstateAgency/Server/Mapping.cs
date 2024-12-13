using AutoMapper;
using RealEstateAgency.Domain;
using Server.DTO;

namespace Server;

public class Mapping: Profile
{
    public Mapping()
    {
        CreateMap<Client, ClientDto>().ReverseMap();
        CreateMap<RealEstate, RealEstateDto>().ReverseMap();
        CreateMap<Order, OrderDto>().ReverseMap();
    }
}
