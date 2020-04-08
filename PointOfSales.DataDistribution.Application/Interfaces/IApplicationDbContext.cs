using Microsoft.EntityFrameworkCore;
using PointOfSales.DataDistribution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PointOfSales.DataDistribution.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<ProductGroup> ProductGroups { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
