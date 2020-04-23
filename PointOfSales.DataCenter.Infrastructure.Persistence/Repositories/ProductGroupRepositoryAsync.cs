using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PointOfSales.DataCenter.Application.Features.Product.ViewModels;
using PointOfSales.DataCenter.Application.Interfaces.Repositories;
using PointOfSales.DataCenter.Infrastructure.Persistence.Context;
using PointOfSales.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Infrastructure.Persistence.Repositories
{
    public class ProductGroupRepositoryAsync : GenericRepositoryAsync<ProductGroup>, IProductGroupRepositoryAsync
    {
        private readonly IMapper _mapper;
        public ProductGroupRepositoryAsync(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductGroupViewModel>> ViewModelListAllAsync()
        {
            var mappedProductList = await _dbContext.ProductGroups
                        .ProjectTo<ProductGroupViewModel>(_mapper.ConfigurationProvider)
                        .ToListAsync();
            return mappedProductList.AsReadOnly();
        }
    }
}
