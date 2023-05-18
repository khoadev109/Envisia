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

                await SeedAreaPolygons(context);

                await SeedStoreServiceArea(context);

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

        public static async Task SeedAreaPolygons(ApplicationDbContext context)
        {
            if (!context.AreaPolygons.Any())
            {
                await context.AreaPolygons.AddRangeAsync(
                    new AreaPolygon
                    {
                        Code = "BU00140000",
                        PolygonData = "{\"outerBoundaryIs\":{\"LinearRing\":{\"coordinates\":[[6.56757286260643,53.2222180347472],[6.56840494484845,53.2219953572096],[6.57009363189477,53.2214918813125],[6.57038606086764,53.2213795961509],[6.57071642278908,53.2210981906189],[6.57192476373251,53.219865925171],[6.57274073404219,53.2184595007215],[6.57242773504559,53.2184728373708],[6.56987535215599,53.2183159577927],[6.56952323733927,53.2183169117273],[6.56893097853094,53.2183509634432],[6.5684256849985,53.2185540888369],[6.56781519055054,53.2184139785159],[6.5678604453283,53.2182948229753],[6.56668391867281,53.2179151264481],[6.56659301903311,53.2180296079599],[6.56623441609392,53.2179023802209],[6.56609438060775,53.2179170560279],[6.56605955778527,53.2177820630976],[6.56584554209093,53.2176477521465],[6.563759826543,53.2170535530942],[6.56239175465595,53.216773644581],[6.56141044193544,53.2166051417549],[6.56009680427775,53.2165701826237],[6.55939800378451,53.2166166415689],[6.55941573892354,53.2172706771373],[6.55930528163376,53.2175755858315],[6.55887266895098,53.2180697485962],[6.55821054764286,53.2184375159947],[6.55776176522538,53.2187556480585],[6.55710336238949,53.2193280312529],[6.55771289258412,53.2197540618623],[6.5588151247069,53.220176836072],[6.56084122202999,53.220787850884],[6.5627496643804,53.2213432246952],[6.56479910665244,53.2218507844877],[6.56531946882144,53.2220006548866],[6.56627586720688,53.222241133747],[6.56665290533957,53.2222892268009],[6.56702893799407,53.2223004538487],[6.56757286260643,53.2222180347472]]}}}",
                        CreatedBy = "TOAA"
                    },
                    new AreaPolygon
                    {
                        Code = "BU00140001",
                        PolygonData = "{\"outerBoundaryIs\":{\"LinearRing\":{\"coordinates\":[[6.56893097853094,53.2183509634432],[6.56952323733927,53.2183169117273],[6.56987535215599,53.2183159577927],[6.57242773504559,53.2184728373708],[6.57274073404219,53.2184595007215],[6.57391431453995,53.2170622364995],[6.57405527491598,53.2169053055393],[6.57438589439617,53.2166180956978],[6.57503112279789,53.2163293181741],[6.57610448608599,53.2159800037451],[6.57660017066699,53.2157367956327],[6.57585526559662,53.2149275821876],[6.5736865851323,53.2137763206879],[6.57367062513623,53.2137682460617],[6.57333156080153,53.2135967130552],[6.57238861304543,53.2131196615646],[6.57014844948401,53.2126317908992],[6.56785947653761,53.2120540455838],[6.56581527348382,53.2119177974043],[6.56581389374537,53.2119177049189],[6.5648162812426,53.2119740452791],[6.56406884288211,53.2120162520342],[6.55985354519468,53.2129351518525],[6.55866531092531,53.2131031094695],[6.55912030011788,53.2139788385242],[6.55934549580972,53.2148568108876],[6.55939800378451,53.2166166415689],[6.56009680427775,53.2165701826237],[6.56141044193544,53.2166051417549],[6.56239175465595,53.216773644581],[6.563759826543,53.2170535530942],[6.56584554209093,53.2176477521465],[6.56605955778527,53.2177820630976],[6.56609438060775,53.2179170560279],[6.56623441609392,53.2179023802209],[6.56659301903311,53.2180296079599],[6.56668391867281,53.2179151264481],[6.5678604453283,53.2182948229753],[6.56781519055054,53.2184139785159],[6.5684256849985,53.2185540888369],[6.56893097853094,53.2183509634432]]}}}",
                        CreatedBy = "TOAA"
                    },
                    new AreaPolygon
                    {
                        Code = "BU00140002",
                        PolygonData = "{\"outerBoundaryIs\":{\"LinearRing\":{\"coordinates\":[[6.57004245810768,53.223508982475],[6.57417300334533,53.2213944967415],[6.57576335955689,53.2202774626151],[6.57648416122785,53.2204990245552],[6.57691412793595,53.2207495895965],[6.5770756563665,53.2208116476148],[6.57726186388574,53.2208689315149],[6.57789301927601,53.2209723312831],[6.57835191037271,53.2211136351194],[6.57852523267869,53.2211384513078],[6.57876972754546,53.2211314316164],[6.57877950204222,53.2207658036897],[6.57853906800753,53.2196532480006],[6.57830991482445,53.2192212998438],[6.57819545379751,53.2189632680618],[6.57837847947661,53.2186146478199],[6.57846645096518,53.2185206573016],[6.57850509971187,53.2184793638607],[6.57854723311339,53.2184343486844],[6.5788604150053,53.2180997354969],[6.580145274628,53.2170515490094],[6.58002866678616,53.2170159448884],[6.57660017066699,53.2157367956327],[6.57610448608599,53.2159800037451],[6.57503112279789,53.2163293181741],[6.57438589439617,53.2166180956978],[6.57405527491598,53.2169053055393],[6.57391431453995,53.2170622364995],[6.57274073404219,53.2184595007215],[6.57192476373251,53.219865925171],[6.57071642278908,53.2210981906189],[6.57038606086764,53.2213795961509],[6.57009363189477,53.2214918813125],[6.56840494484845,53.2219953572096],[6.56757286260643,53.2222180347472],[6.56702893799407,53.2223004538487],[6.56665290533957,53.2222892268009],[6.56627586720688,53.222241133747],[6.56627841548614,53.2226188655203],[6.56971380415877,53.2236624208605],[6.57004245810768,53.223508982475]]}}}",
                        CreatedBy = "TOAA"
                    }
                );

                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedStoreServiceArea(ApplicationDbContext context)
        {
            if (!context.StoreServiceAreas.Any())
            {
                var stores = context.Stores.ToList();
                var areaPolygons = context.AreaPolygons.ToList();

                var firstArea = new StoreServiceArea
                {
                    Store = stores[0],
                    Polygon = areaPolygons[0],
                    CreatedBy = "TOAA"
                };

                stores[0].StoreServiceAreas.Add(firstArea);
                areaPolygons[0].StoreServiceAreas.Add(firstArea);

                var secondArea = new StoreServiceArea
                {
                    Store = stores[1],
                    Polygon = areaPolygons[1],
                    CreatedBy = "TOAA"
                };

                stores[1].StoreServiceAreas.Add(secondArea);
                areaPolygons[1].StoreServiceAreas.Add(secondArea);

                var thirdArea = new StoreServiceArea
                {
                    Store = stores[2],
                    Polygon = areaPolygons[2],
                    CreatedBy = "TOAA"
                };

                stores[2].StoreServiceAreas.Add(thirdArea);
                areaPolygons[2].StoreServiceAreas.Add(thirdArea);

                await context.SaveChangesAsync();
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
