using Microsoft.AspNetCore.Identity;
using PointOfSales.DataCenter.Domain.Entities;
using PointOfSales.DataCenter.Infrastructure.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Infrastructure.Persistence.Context
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, ApplicationDbContext context, int? retry = 0)
        {
            var defaultUser = new ApplicationUser { UserName = "iammukeshm", Email = "iammukeshm@gmail.com" };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, "123Mukesh.");
            }

            int retryForAvailability = retry.Value;
            try
            {
                // TODO: Only run this if using a real database
                // context.Database.Migrate();

                if (!context.ProductCategories.Any())
                {
                    context.ProductCategories.AddRange(
                        GetPreconfiguredProductCategories());

                    await context.SaveChangesAsync();
                }

                if (!context.ProductGroups.Any())
                {
                    context.ProductGroups.AddRange(
                        GetPreconfiguredProductGroups());

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    context.Products.AddRange(
                        GetPreconfiguredProducts());

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    await SeedAsync(userManager, context, retryForAvailability);
                }
                throw;
            }
        }
        static IEnumerable<ProductCategory> GetPreconfiguredProductCategories()
        {
            return new List<ProductCategory>()
            {
                new ProductCategory{ IsActive=true, Name = "Beverages",Tenant = 1 }
            };
        }

        static IEnumerable<ProductGroup> GetPreconfiguredProductGroups()
        {
            return new List<ProductGroup>()
            {
                new ProductGroup{ ProductCategoryId=1, Name = "Soft Drink", Tenant = 1, IsActive = true }
            };
        }

        static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product{ IsActive=true, Name = "Pepsi 350ml", ProductGroupId =1, Barcode ="MKS00115420000012", BuyingPrice = 25, DisableIfStockIsZero=true, SellingPrice = 39, Description="Sample Description about Pepsi 350ml"}
            };
        }
    }
}
