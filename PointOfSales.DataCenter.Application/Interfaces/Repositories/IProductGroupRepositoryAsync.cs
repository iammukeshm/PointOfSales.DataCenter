using PointOfSales.DataCenter.Application.Features.Product.ViewModels;
using PointOfSales.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Application.Interfaces.Repositories
{
    public interface IProductGroupRepositoryAsync : IRepositoryAsync<ProductGroup>
    {
        Task<IEnumerable<ProductGroupViewModel>> ViewModelListAllAsync();
    }
}
