﻿using Microsoft.EntityFrameworkCore;
using PointOfSales.DataCenter.Domain.Entities;
using PointOfSales.DataCenter.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Infrastructure.Persistence.Repositories
{
    //public class ProductRepositoryAsync : GenericRepositoryAsync<Product>, IProductRepositoryAsync
    //{
    //    public ProductRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
    //    {
    //    }

    //    public Task<Product> GetByIdWithItemsAsync(int id)
    //    {
    //        return _dbContext.Products
    //            .Include(o => o.)
    //            .Include($"{nameof(Order.OrderItems)}.{nameof(OrderItem.ItemOrdered)}")
    //            .FirstOrDefaultAsync(x => x.Id == id);
    //    }
    //}
}