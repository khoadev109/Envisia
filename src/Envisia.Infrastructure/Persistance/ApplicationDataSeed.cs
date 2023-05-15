using Envisia.Data.Entities;
using Envisia.Library.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Envisia.Infrastructure.Persistance
{
    public static class ApplicationDataSeed
    {
        public static async Task Run(ApplicationDbContext context)
        {
            try
            {
                await SeedClients(context);

                await context.SaveChangesAsync();

                await SeedUsers(context);

                await SeedRoles(context);

                await context.SaveChangesAsync();

                await SeedUserRoles(context);

                await context.SaveChangesAsync();

                await SeedOrganisations(context);

                await context.SaveChangesAsync();

                await SeedContacts(context);

                await SeedFormulas(context);

                await context.SaveChangesAsync();

                await SeedFormulaStats(context);

                await SeedStoreFeatures(context);

                await context.SaveChangesAsync();

                await SeedStores(context);

                await context.SaveChangesAsync();

                await SeedLogos(context);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public static async Task SeedClients(ApplicationDbContext context)
        {
            if (!context.Clients.Any())
            {
                await context.Clients.AddRangeAsync(
                    new Client
                    {
                        Name = "Client1",
                        CreatedBy = "TOAA"
                    },
                    new Client
                    {
                        Name = "Client2",
                        CreatedBy = "TOAA"
                    }
                );
            }
        }

        public static async Task SeedUsers(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                List<Client> clients = await context.Clients.ToListAsync();

                var (hashAdmin, saltAdmin) = PasswordHelper.HashPassword("admin");

                var (hashUser, saltUser) = PasswordHelper.HashPassword("pwd");

                await context.Users.AddRangeAsync(
                    new User
                    {
                        UserName = "admin1",
                        PasswordHash = hashAdmin,
                        PasswordSalt = saltAdmin,
                        ClientId = clients[0].Id,
                        Status = 1,
                        DateActive = DateTime.Now,
                        CreatedBy = "TOAA"
                    },
                    new User
                    {
                        UserName = "user1",
                        PasswordHash = hashUser,
                        PasswordSalt = saltUser,
                        ClientId = clients[0].Id,
                        Status = 1,
                        DateActive = DateTime.Now,
                        CreatedBy = "TOAA"
                    },
                    new User
                    {
                        UserName = "admin2",
                        PasswordHash = hashAdmin,
                        PasswordSalt = saltAdmin,
                        ClientId = clients[1].Id,
                        Status = 1,
                        DateActive = DateTime.Now,
                        CreatedBy = "TOAA"
                    },
                    new User
                    {
                        UserName = "user2",
                        PasswordHash = hashUser,
                        PasswordSalt = saltUser,
                        ClientId = clients[1].Id,
                        Status = 1,
                        DateActive = DateTime.Now,
                        CreatedBy = "TOAA"
                    }
                );
            }
        }

        public static async Task SeedRoles(ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                await context.Roles.AddRangeAsync(
                    new Role
                    {
                        Name = "Admin",
                        CreatedBy = "TOAA"
                    },
                    new Role
                    {
                        Name = "User",
                        CreatedBy = "TOAA"
                    }
                );
            }
        }

        public static async Task SeedUserRoles(ApplicationDbContext context)
        {
            if (!context.UserRoles.Any())
            {
                List<Client> clients = (await context.Clients.ToListAsync()).OrderBy(x => x.Id).ToList();

                List<User> users = (await context.Users.ToListAsync()).OrderBy(x => x.Id).ToList();

                List<Role> roles = (await context.Roles.ToListAsync()).OrderBy(x => x.Id).ToList();

                var adminClient1 = new UserRole
                {
                    Client = clients[0],
                    User = users[0],
                    Role = roles[0],
                    CreatedBy = "TOAA"
                };

                clients[0].UserRoles.Add(adminClient1);
                users[0].UserRoles.Add(adminClient1);
                roles[0].UserRoles.Add(adminClient1);

                var userClient1 = new UserRole
                {
                    Client = clients[0],
                    User = users[1],
                    Role = roles[1],
                    CreatedBy = "TOAA"
                };

                clients[0].UserRoles.Add(userClient1);
                users[1].UserRoles.Add(userClient1);
                roles[1].UserRoles.Add(userClient1);

                var adminClient2 = new UserRole
                {
                    Client = clients[1],
                    User = users[2],
                    Role = roles[0],
                    CreatedBy = "TOAA"
                };

                clients[1].UserRoles.Add(adminClient2);
                users[2].UserRoles.Add(adminClient2);
                roles[0].UserRoles.Add(adminClient2);

                var userClient2 = new UserRole
                {
                    Client = clients[1],
                    User = users[3],
                    Role = roles[1],
                    CreatedBy = "TOAA"
                };

                clients[1].UserRoles.Add(userClient2);
                users[3].UserRoles.Add(userClient2);
                roles[1].UserRoles.Add(userClient2);
            }
        }

        public static async Task SeedOrganisations(ApplicationDbContext context)
        {
            if (!context.Organisations.Any())
            {
                await context.Organisations.AddRangeAsync(
                    new Organisation
                    {
                        Name = "Jumbo Supermarkten",
                        CreatedBy = "TOAA"
                    },
                    new Organisation
                    {
                        Name = "Nettorama Distributie",
                        CreatedBy = "TOAA"
                    }
                );
            }
        }

        public static async Task SeedFormulas(ApplicationDbContext context)
        {
            if (!context.Formulas.Any())
            {
                await context.Formulas.AddRangeAsync(
                    new Formula
                    {
                        Name = "Albert Heijn Nederland",
                        OrganisationId = 1,
                        CreatedBy = "TOAA"
                    },
                    new Formula
                    {
                        Name = "Albert Heijn Nederland 1",
                        OrganisationId = 1,
                        CreatedBy = "TOAA"
                    }
                );
            }
        }

        public static async Task SeedFormulaStats(ApplicationDbContext context)
        {
            if (!context.FormulaStats.Any())
            {
                await context.FormulaStats.AddRangeAsync(
                    new FormulaStat
                    {
                        Annum = "2023",
                        Turnover = 138,
                        LFATotal = 11,
                        Franchise = 6,
                        Store = 88,
                        FormulaId = 1,
                        CreatedBy = "TOAA"
                    },
                    new FormulaStat
                    {
                        Annum = "2022",
                        Turnover = 138,
                        LFATotal = 11,
                        MarketShareIRI = 36.5M,
                        MarketShareNielsen = 37,
                        Franchise = 6,
                        Store = 88,
                        FormulaId = 1,
                        CreatedBy = "TOAA"
                    },
                    new FormulaStat
                    {
                        Annum = "2021",
                        Turnover = 141,
                        LFATotal = 11,
                        MarketShareIRI = 35.5M,
                        MarketShareNielsen = 35.9M,
                        Franchise = 6,
                        Store = 85,
                        FormulaId = 1,
                        CreatedBy = "TOAA"
                    }
                );
            }
        }

        public static async Task SeedContacts(ApplicationDbContext context)
        {
            if (!context.Contacts.Any())
            {
                await context.Contacts.AddRangeAsync(
                    new Contact
                    {
                        Initials = "J.",
                        SurName = "super",
                        LastName = "Bombeeck",
                        Gender = true,
                        JobTitle = "Inkoper chocolade, seizoen",
                        Phone = "0413-380200",
                        Email = "info@jumbosupermarkten.nl",
                        Address = "15 Rijksweg, 5462CE VEGHEL",
                        OrganisationId = 1,
                        CreatedBy = "TOAA"
                    },
                    new Contact
                    {
                        Initials = "H.",
                        SurName = "van",
                        LastName = "Genderen - Dam",
                        Gender = false,
                        JobTitle = "Senior Inkoper wijn en gedistilleerd",
                        Phone = "0413-380200",
                        Email = "info@jumbosupermarkten.nl",
                        Address = "15 Rijksweg, 5462CE VEGHEL",
                        OrganisationId = 1,
                        CreatedBy = "TOAA"
                    },
                    new Contact
                    {
                        Initials = "W. G.",
                        SurName = "van den",
                        LastName = "Brink",
                        Gender = true,
                        JobTitle = "Directeur",
                        Phone = "0162-455950",
                        Email = "info@nettorama.nl",
                        Address = "120 Wilhelminakanaal Zuid, 4903RA OOSTERHOUT NB",
                        OrganisationId = 2,
                        CreatedBy = "TOAA"
                    }
                );
            }
        }

        public static async Task SeedStoreFeatures(ApplicationDbContext context)
        {
            if (!context.StoreFeatures.Any())
            {
                await context.StoreFeatures.AddRangeAsync(
                    new StoreFeature
                    {
                        Feature = "Brood (bediening), BakeOff, Vleeswaren (bediening), Zelfscankassa, Kaas (bediening), Tabak, Ophaalpunt, Geldautomaat, Koffiecorner",
                        CreatedBy = "TOAA"
                    },
                    new StoreFeature
                    {
                        Feature = "Brood (bediening), BakeOff, Slagerij, Vleeswaren (bediening), Zelfscankassa, Kaas (bediening), Tabak, Wifi, Bloemen en planten, Echt vers",
                        CreatedBy = "TOAA"
                    },
                    new StoreFeature
                    {
                        Feature = "Brood (bediening), BakeOff, Slagerij, Vleeswaren (bediening), Slijterij/Borrelshop, Open op zondag, Kaas (bediening), Tabak, Ophaalpunt, Parkeren (eigen beheer), Geldautomaat",
                        CreatedBy = "TOAA"
                    }
                );
            }
        }

        public static async Task SeedStores(ApplicationDbContext context)
        {
            if (!context.Stores.Any())
            {
                var formulas = await context.Formulas.ToListAsync();
                var storeFeatures = await context.StoreFeatures.ToListAsync();
                var contacts = await context.Contacts.ToListAsync();

                await context.Stores.AddRangeAsync(
                    new Store
                    {
                        StoreName = "Albert Heijn Lindeman Elandsgracht",
                        Franchise = true,
                        Address = "Elandsgracht",
                        HouseNumber = "13",
                        CountryAlpha2 = "NL",
                        Zip = "1016TM",
                        City = "AMSTERDAM",
                        Province = "Noord-Holland",
                        Phone = "020-6236574",
                        Email = "mt8684ah@ah.nl",
                        CounterQty = 5,
                        DcName = "Zaandam",
                        Latitude = 523.697M,
                        Longitude = 488.184M,
                        StoreFeatures = new List<StoreFeature> { storeFeatures[0] },
                        ContactManagerId = contacts[0].Id,
                        ContactOwnerId = contacts[1].Id,
                        FormulaId = formulas[0].Id,
                        CreatedBy = "TOAA"
                    },
                    new Store
                    {
                        StoreName = "Albert Heijn",
                        Franchise = false,
                        Address = "Van Limburg Stirumstraat",
                        HouseNumber = "44",
                        CountryAlpha2 = "NL",
                        Zip = "1051BC",
                        City = "AMSTERDAM",
                        Province = "Noord-Holland",
                        Phone = "020-6812187",
                        Email = "sm1490ah@ah.nl",
                        CounterQty = 2,
                        DcName = "Zaandam",
                        Latitude = 523.697M,
                        Longitude = 487.603M,
                        StoreFeatures = new List<StoreFeature> { storeFeatures[1] },
                        ContactManagerId = contacts[0].Id,
                        ContactOwnerId = contacts[1].Id,
                        FormulaId = formulas[0].Id,
                        CreatedBy = "TOAA"
                    },
                    new Store
                    {
                        StoreName = "Albert Heijn Volendam Havenhof",
                        Franchise = false,
                        Address = "Zeestraat",
                        HouseNumber = "21",
                        CountryAlpha2 = "NL",
                        Zip = "1131ZD",
                        City = "VOLENDAM",
                        Province = "Noord-Holland",
                        Phone = "088-6592272",
                        Email = "sm1490ah@ah.nl",
                        CounterQty = 4,
                        DcName = "Hoorn NH Beverwijk",
                        Latitude = 524.938M,
                        Longitude = 50.723M,
                        StoreFeatures = new List<StoreFeature> { storeFeatures[2] },
                        ContactManagerId = contacts[0].Id,
                        ContactOwnerId = contacts[1].Id,
                        FormulaId = formulas[1].Id,
                        CreatedBy = "TOAA"
                    }
                );

                await context.SaveChangesAsync();

                List<Store> stores = await context.Stores.ToListAsync();

                storeFeatures[0].Stores = new List<Store> { stores[0] };
                storeFeatures[1].Stores = new List<Store> { stores[1] };
                storeFeatures[2].Stores = new List<Store> { stores[2] };
            }
        }

        public static async Task SeedLogos(ApplicationDbContext context)
        {
            if (!context.Logos.Any())
            {
                var imageByteArray = GetImageByteArray();

                List<Organisation> organisations = await context.Organisations.ToListAsync();

                List<Formula> formulas = await context.Formulas.ToListAsync();

                List<Store> stores = await context.Stores.ToListAsync();

                await context.Logos.AddRangeAsync(
                    new Logo
                    {
                        Picture = imageByteArray,
                        CreatedBy = "TOAA"
                    }
                );

                await context.SaveChangesAsync();

                Logo logo = await context.Logos.FirstOrDefaultAsync();

                foreach (var organisation in organisations)
                {
                    organisation.LogoId = logo?.Id;
                }

                foreach (var formula in formulas)
                {
                    formula.LogoId = logo?.Id;
                }

                foreach (var store in stores)
                {
                    store.LogoId = logo?.Id;
                }
            }
        }

        private static byte[] GetImageByteArray()
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);

            string strSettingsXmlFilePath = System.IO.Path.Combine(strWorkPath, "sampleLogo.jpg");

            System.Drawing.Image image = new Bitmap(strSettingsXmlFilePath);

            using (MemoryStream ms = new())
            {
                image.Save(ms, image.RawFormat);
                
                byte[] imageBytes = ms.ToArray();

                return imageBytes;
            }
        }
    }
}
