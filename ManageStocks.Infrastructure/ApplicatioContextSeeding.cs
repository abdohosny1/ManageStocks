using ManageStocks.ApplicationCore.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
                               Name = "Stock1",
                               Price= 25
                            },
                               new Stock()
                            {
                                Name = "Stock2",
                               Price= 55
                            },
                               new Stock()
                            {
                                 Name = "Stock3",
                                 Price= 75
                            },
                               new Stock()
                            {
                                Name = "Stock4",
                                Price=78
                            },
                               new Stock()
                            {
                                Name = "Stock5",
                                Price=22
                            },
                                new Stock()
                            {
                                Name = "Stock6",
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
