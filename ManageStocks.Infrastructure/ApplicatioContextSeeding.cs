


namespace ManageStocks.Infrastructure
{
    public static class ApplicatioContextSeeding
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                try
                {
                    if (!context.Stocks.Any())
                    {
                        context.Stocks.AddRange(new List<Stock>()
                            {
                               new Stock()
                            {
                               Name = "Vianet",
                               Price= 25
                            },
                               new Stock()
                            {
                                Name = "Agritek",
                               Price= 55
                            },
                               new Stock()
                            {
                                 Name = "Akamai",
                                 Price= 75
                            },
                               new Stock()
                            {
                                Name = "Baidu",
                                Price=78
                            },
                               new Stock()
                            {
                                Name = "Blinkx",
                                Price=22
                            },
                                new Stock()
                            {
                                Name = "Blucora",
                                Price=47
                            },
                                      new Stock()
                            {
                                Name = "Boingo",
                                Price=47
                            },
                                            new Stock()
                            {
                                Name = "Brainybrawn",
                                Price=47
                            },
                                                  new Stock()
                            {
                                Name = "Carbonite",
                                Price=47
                            },
                                                        new Stock()
                            {
                                Name = "China Finance",
                                Price=47
                            },
                                                              new Stock()
                            {
                                Name = "ChinaCache",
                                Price=47
                            },
                                                                    new Stock()
                            {
                                Name = "ADR",
                                Price=47
                            },
                                                                          new Stock()
                            {
                                Name = "ChitrChatr",
                                Price=47
                            },
                                                                                new Stock()
                            {
                                Name = "Cnova",
                                Price=47
                            },
                                                                                      new Stock()
                            {
                                Name = "Cogent",
                                Price=47
                            },
                                                                                            new Stock()
                            {
                                Name = "Crexendo",
                                Price=47
                            },
                            new Stock()
                            {
                                Name = "CrowdGather",
                                Price=47
                            },
                                                        new Stock()
                            {
                                Name = "EarthLink",
                                Price=47
                            },
                                                                                    new Stock()
                            {
                                Name = "Eastern",
                                Price=47
                            },
                                                                                                                new Stock()
                            {
                                Name = "ENDEXX",
                                Price=47
                            },
new Stock()
                            {
                                Name = "Envestnet",
                                Price=47
                            },
new Stock()
                            {
                                Name = "Epazz",
                                Price=47
                            },
new Stock()
                            {
                                Name = "FlashZero",
                                Price=47
                            },
new Stock()
                            {
                                Name = "Genesis",
                                Price=47
                            },
new Stock()
                            {
                                Name = "InterNAP",
                                Price=47
                            },
new Stock()
                            {
                                Name = "MeetMe",
                                Price=47
                            },
new Stock()
                            {
                                Name = "Netease",
                                Price=47
                            },
new Stock()
                            {
                                Name = "Qihoo",
                                Price=47
                            },

                        });



                        context.SaveChanges();
                    }
                }

                catch (Exception ex)
                {
                    //var logger = loggerFactory.CreateLogger<ApplicatioContextSeeding>();
                    //logger.LogError(ex.Message);
                }


            }
        }
   }
}
