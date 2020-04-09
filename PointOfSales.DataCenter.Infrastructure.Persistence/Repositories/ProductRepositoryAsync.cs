using Microsoft.EntityFrameworkCore;
using PointOfSales.DataCenter.Application.Interfaces.Repositories;
using PointOfSales.DataCenter.Domain.Entities;
using PointOfSales.DataCenter.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Infrastructure.Persistence.Repositories
{
    public class ProductRepositoryAsync : GenericRepositoryAsync<Product>, IProductRepositoryAsync
    {
        public ProductRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> DoesBarCodeExist(string barCode)
        {
            return _dbContext.Products.AnyAsync(a => a.Barcode == barCode);
        }
    }
}
