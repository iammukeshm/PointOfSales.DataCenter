using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PointOfSales.DataCenter.Application.Features.ProductFeatures.ViewModels;
using PointOfSales.DataCenter.Application.Interfaces.Repositories;
using PointOfSales.Domain.Entities;
using PointOfSales.DataCenter.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Infrastructure.Persistence.Repositories
{
    public class ProductRepositoryAsync : GenericRepositoryAsync<Product>, IProductRepositoryAsync
    {
        private readonly IMapper _mapper;
        public ProductRepositoryAsync(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        public Task<bool> DoesBarCodeExist(string barCode)
        {
            return _dbContext.Products.AnyAsync(a => a.Barcode == barCode);
        }
        public async Task<IEnumerable<ProductViewModel>> ViewModelListAllAsync()
        {
            var mappedProductList = await _dbContext.Products
                        .ProjectTo<ProductViewModel>(_mapper.ConfigurationProvider)
                        .ToListAsync();
            return mappedProductList.AsReadOnly();
        }
    }
}
