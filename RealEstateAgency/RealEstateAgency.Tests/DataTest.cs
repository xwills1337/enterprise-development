using RealEstateAgency.Domain;

namespace RealEstateAgency.Tests;

public static class TestData
{
    public static List<Client> Clients { get; private set; }
    public static List<RealEstate> RealEstates { get; private set; }
    public static List<Order> Orders { get; private set; }

    static TestData()
    {
        Clients =
        [
            new Client
            {
                Id = 1,
                FullName = "Smirnov Andrey",
                Passport = "1111 222233",
                Phone = "+7 800 555 3535",
                Address = "st. Pervomayskaya, 1, Moscow",
            },
            new Client
            {
                Id = 2,
                FullName = "Korolev Yuri",
                Passport = "2222 334455",
                Phone = "+7 800 555 4646",
                Address = "st. Tverskaya, 25, Moscow",
            },
            new Client
            {
                Id = 3,
                FullName = "Vasilieva Olga",
                Passport = "3333 445566",
                Phone = "+7 800 555 5757",
                Address = "st. Arbat, 12, Moscow",
            },
            new Client
            {
                Id = 4,
                FullName = "Karpov Pavel",
                Passport = "4444 556677",
                Phone = "+7 800 555 6868",
                Address = "st. Sretenka, 56, Moscow",
            },
            new Client
            {
                Id = 5,
                FullName = "Nikolaev Ivan",
                Passport = "5555 667788",
                Phone = "+7 800 555 7979",
                Address = "st. Pokrovka, 9, Moscow",
            }
        ];

        RealEstates =
        [
            new RealEstate
            {
                Type = RealEstateType.Residential,
                Address = "100 Leningradsky Prospekt, Moscow",
                Square = 70.5,
                NumberOfRooms = 2
            },
            new RealEstate
            {
                Type = RealEstateType.Commercial,
                Address = "300 Tverskaya St, Moscow",
                Square = 150.0,
                NumberOfRooms = 4
            },
            new RealEstate
            {
                Type = RealEstateType.Residential,
                Address = "10 Pushkinskaya Sq, Moscow",
                Square = 80.0,
                NumberOfRooms = 3
            },
            new RealEstate
            {
                Type = RealEstateType.Residential,
                Address = "15 Garden Ring Rd, Moscow",
                Square = 60.0,
                NumberOfRooms = 2
            },
            new RealEstate
            {
                Type = RealEstateType.Commercial,
                Address = "22 Nevskiy Ave, St. Petersburg",
                Square = 200.0,
                NumberOfRooms = 5
            },
            new RealEstate
            {
                Type = RealEstateType.Residential,
                Address = "44 Admiralteysky Prospect, St. Petersburg",
                Square = 110.0,
                NumberOfRooms = 3
            },
            new RealEstate
            {
                Type = RealEstateType.Residential,
                Address = "18 Marata St, St. Petersburg",
                Square = 90.0,
                NumberOfRooms = 4
            },
            new RealEstate
            {
                Type = RealEstateType.Commercial,
                Address = "12 Nevsky Prospect, St. Petersburg",
                Square = 300.0,
                NumberOfRooms = 6
            },
            new RealEstate
            {
                Type = RealEstateType.Residential,
                Address = "28 Moscow St, Yekaterinburg",
                Square = 65.0,
                NumberOfRooms = 2
            },
            new RealEstate
            {
                Type = RealEstateType.Residential,
                Address = "35 Kirov St, Yekaterinburg",
                Square = 120.0,
                NumberOfRooms = 5
            }
        ];

        Orders =
        [
            new Order
            {
                Id = 1,
                Time = DateTime.Now.AddMonths(-1),
                Client = Clients[0],
                Type = TransactionType.Purchase,
                Price = 750000,
                Item = RealEstates[0]
            },
            new Order
            {
                Id = 2,
                Time = DateTime.Now.AddMonths(-2),
                Client = Clients[0],
                Type = TransactionType.Sale,
                Price = 950000,
                Item = RealEstates[1]
            },
            new Order
            {
                Id = 3,
                Time = DateTime.Now.AddMonths(-3),
                Client = Clients[1],
                Type = TransactionType.Purchase,
                Price = 800000,
                Item = RealEstates[2]
            },
            new Order
            {
                Id = 4,
                Time = DateTime.Now.AddMonths(-4),
                Client = Clients[1],
                Type = TransactionType.Sale,
                Price = 1100000,
                Item = RealEstates[3]
            },
            new Order
            {
                Id = 5,
                Time = DateTime.Now.AddMonths(-5),
                Client = Clients[2],
                Type = TransactionType.Purchase,
                Price = 600000,
                Item = RealEstates[4]
            },
            new Order
            {
                Id = 6,
                Time = DateTime.Now.AddMonths(-2),
                Client = Clients[2],
                Type = TransactionType.Sale,
                Price = 1200000,
                Item = RealEstates[5]
            },
            new Order
            {
                Id = 7,
                Time = DateTime.Now.AddMonths(-3),
                Client = Clients[3],
                Type = TransactionType.Purchase,
                Price = 1000000,
                Item = RealEstates[6]
            },
            new Order
            {
                Id = 8,
                Time = DateTime.Now.AddMonths(-1),
                Client = Clients[3],
                Type = TransactionType.Sale,
                Price = 850000,
                Item = RealEstates[7]
            },
            new Order
            {
                Id = 9,
                Time = DateTime.Now.AddMonths(-6),
                Client = Clients[4],
                Type = TransactionType.Purchase,
                Price = 1500000,
                Item = RealEstates[8]
            },
            new Order
            {
                Id = 10,
                Time = DateTime.Now.AddMonths(-4),
                Client = Clients[4],
                Type = TransactionType.Sale,
                Price = 950000,
                Item = RealEstates[9]
            }
        ];
    }
}