using Microsoft.EntityFrameworkCore;
using PointOfSales.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<ProductGroup> ProductGroups { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
