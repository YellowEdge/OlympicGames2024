using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using OgWeb.Constants;

namespace OgWeb.Data;

public static class DbSeeder
{
    public static async Task SeedDefaultData(IServiceProvider service, ApplicationDbContext context)
    {
        var userMgr = service.GetService<UserManager<ApplicationUser>>();
        var roleMgr = service.GetService<RoleManager<IdentityRole>>();
        //adding some roles to db
        var adminRoleExits = await roleMgr.FindByNameAsync(Roles.admin.ToString());
        var clientRoleExits = await roleMgr.FindByNameAsync(Roles.client.ToString());
        var securityRoleExits = await roleMgr.FindByNameAsync(Roles.ticketcontrol.ToString());

        if (adminRoleExits is null)
        {
            await roleMgr.CreateAsync(new IdentityRole(Roles.admin.ToString()));
            //adminRoleExits = await roleMgr.FindByNameAsync(Roles.admin.ToString());
            //if (!adminRoleExits.NormalizedName.Equals(Roles.admin.ToString()))
            //{
            //    adminRoleExits.NormalizedName = "admin";
            //}
        }

        if (clientRoleExits is null)
        {
            await roleMgr.CreateAsync(new IdentityRole(Roles.client.ToString()));
        }

        if (securityRoleExits is null)
        {
            await roleMgr.CreateAsync(new IdentityRole(Roles.ticketcontrol.ToString()));
        }

        // create admin user
        var adminUserExists = await userMgr.FindByEmailAsync("admin@paris.jo");
        if(adminUserExists is null)
        {
            var admin = new ApplicationUser
            {
                UserName = "admin@paris.jo",
                Email = "admin@paris.jo",
                EmailConfirmed = true,
                CreatedAt = DateTime.Now,
                UserKey = Guid.NewGuid().ToString()
            };

            var adminInDb = await userMgr.FindByEmailAsync(admin.Email);
            if (adminInDb is null)
            {
                await userMgr.CreateAsync(admin, "Azerty01$");
                await userMgr.AddToRoleAsync(admin, Roles.admin.ToString());
            }
        }
        else
        {
            return;   // DB has been seeded
        }

        //create client user
        var testUserExists = await userMgr.FindByEmailAsync("ana@paris.jo");
        if(testUserExists is null)
        {
            var ana = new ApplicationUser
            {
                UserName = "ana@paris.jo",
                Email = "ana@paris.jo",
                EmailConfirmed = true,
                CreatedAt = DateTime.Now,
                UserKey = Guid.NewGuid().ToString()
            };

            var clientInDb = await userMgr.FindByEmailAsync(ana.Email);
            if (clientInDb is null)
            {
                await userMgr.CreateAsync(ana, "Azerty01$");
                await userMgr.AddToRoleAsync(ana, Roles.client.ToString());
            }
        }

        //create security user
        var secUserExists = await userMgr.FindByEmailAsync("bull@paris.jo");
        if (secUserExists is null)
        {
            var bull = new ApplicationUser
            {
                UserName = "bull@paris.jo",
                Email = "bull@paris.jo",
                EmailConfirmed = true,
                CreatedAt = DateTime.Now,
                UserKey = Guid.NewGuid().ToString()
            };

            var secInDb = await userMgr.FindByEmailAsync(bull.Email);
            if (secInDb is null)
            {
                await userMgr.CreateAsync(bull, "Azerty01$");
                await userMgr.AddToRoleAsync(bull, Roles.ticketcontrol.ToString());
            }
        }
                
        //Data
        var ATHLÉTISME = new Category
        {
            Name = "ATHLÉTISME",
            Desc = "L'athlétisme est un ensemble d’épreuves sportives codifiées comprenant les courses, sauts, lancers, épreuves combinées et marche."
        };

        var AVIRON = new Category
        {
            Name = "AVIRON",
            Desc = "Sport nautique pratiqué sur un plan d''eau aménagé, où une embarcation est propulsée à l'aide de rames."
        };

        var BADMINTON = new Category
        {
            Name = "BADMINTON",
            Desc = "Sport pratiqué sur un court, et qui consiste à se renvoyer, dans les limites de ce court, un volant par-dessus un filet à l'aide de raquettes."
        };

        var SURF = new Category
        {
            Name = "SURF",
            Desc = "Sport consistant à se maintenir en équilibre sur une planche portée par une vague déferlante."
        };

        var GOLF = new Category
        {
            Name = "GOLF",
            Desc = "Sport consistant à envoyer une balle à l'aide de clubs dans les 18 trous successifs d'un vaste terrain avec le minimum de coups."
        };

        var categories = new Category[]
        {
            ATHLÉTISME,
            AVIRON,
            BADMINTON,
            SURF,
            GOLF
        };

        context.AddRange(categories);


        var Bordeaux = new Site
        {
            Addresse = "Rue de la Liberté",
            City = "Bordeaux"
        };

        var Lyon = new Site
        {
            Addresse = "Rue de la Fraternité",
            City = "Lyon"
        };

        var Paris = new Site
        {
            Addresse = "Avenue de France",
            City = "Paris"
        };

        var SaintDenis = new Site
        {
            Addresse = "Avenue du Stade de France",
            City = "Saint-Denis"
        };

        var Marseille = new Site
        {
            Addresse = "Route du port",
            City = "Marseille"
        };

        var sites = new Site[]
        {
            Bordeaux,
            Lyon,
            Paris,
            SaintDenis,
            Marseille
        };

        context.AddRange(sites);

        var SOLO = new Offer
        {
            Desc = "Offre de ticket valable pour une personne.",
            Name = "SOLO",
            Quantity = 1
        };

        var DUO = new Offer
        {
            Desc = "Offre de ticket valable pour deux personnes",
            Name = "DUO",
            Quantity = 2
        };

        var FAMILY = new Offer
        {
            Desc = "Offre de ticket valable pour quatre personnes",
            Name = "FAMILY",
            Quantity = 4
        };

        var ENTERPRISE = new Offer
        {
            Desc = "Offre de ticket valable pour dix personnes",
            Name = "ENTERPRISE",
            Quantity = 10
        };

        var offers = new Offer[]
        {
            SOLO,
            DUO,
            FAMILY,
            ENTERPRISE
        };

        context.AddRange(offers);

        var Pending = new OrderStatus
        {
            StatusName = "Pending",
            StatusId = 1
        };

        var Shipped = new OrderStatus
        {
            StatusName = "Shipped",
            StatusId = 2
        };

        var Delivered = new OrderStatus
        {
            StatusName = "Delivered",
            StatusId = 3
        };

        var Cancelled = new OrderStatus
        {
            StatusName = "Cancelled",
            StatusId = 4
        };

        var Returned = new OrderStatus
        {
            StatusName = "Returned",
            StatusId = 5
        };

        var Refund = new OrderStatus
        {
            StatusName = "Refund",
            StatusId = 6
        };

        var orderStatuses = new OrderStatus[]
        {
            Pending,
            Shipped,
            Delivered,
            Cancelled,
            Returned,
            Refund
        };

        context.AddRange(orderStatuses);

        var F100mH = new Event
        {
            EventTitle = "Finale 100 mètres [H]",
            EventDesc = "Finale 100 mètres. Epreuve concernant les hommes.",
            Price = 100,
            StartDate = DateTime.Parse("2024-08-15 12:00:00"),
            Site = SaintDenis,
            Category = ATHLÉTISME
        };

        var F100mF = new Event
        {
            EventTitle = "Finale 100 mètres [F]",
            EventDesc = "Finale 100 mètres. Epreuve concernant les femmes.",
            Price = 100,
            StartDate = DateTime.Parse("2024-08-15 16:00:00"),
            Site = SaintDenis,
            Category = ATHLÉTISME
        };

        var surf1ertH = new Event
        {
            EventTitle = "Surf 1er tour [H]",
            EventDesc = "Premier tour éliminatoire concernant les hommes.",
            Price = 50,
            StartDate = DateTime.Parse("2024-08-30 10:00:00"),
            Site = Marseille,
            Category = SURF
        };

        var surf1ertF = new Event
        {
            EventTitle = "Surf 1er tour [F]",
            EventDesc = "Premier tour éliminatoire concernant les femmes.",
            Price = 50,
            StartDate = DateTime.Parse("2024-08-30 14:00:00"),
            Site = Marseille,
            Category = SURF
        };

        var finalGolfH = new Event
        {
            EventTitle = "Finale Golf [H]",
            EventDesc = "Finale Golf concernant les hommes.",
            Price = 100,
            StartDate = DateTime.Parse("2024-08-20 08:00:00"),
            Site = Bordeaux,
            Category = GOLF
        };

        var finalGolfF = new Event
        {
            EventTitle = "Finale Golf [F]",
            EventDesc = "Finale Golf concernant les femmes.",
            Price = 100,
            StartDate = DateTime.Parse("2024-08-22 08:00:00"),
            Site = Bordeaux,
            Category = GOLF
        };

        var events = new Event[]
        {
            F100mH,
            F100mF,
            surf1ertH,
            surf1ertF,
            finalGolfH,
            finalGolfF
        };

        context.AddRange(events);
        context.SaveChanges();
    }
}