using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PointOfSales.DataCenter.Application.DTOs;
using PointOfSales.DataCenter.Application.Features.ProductFeatures.ViewModels;
using PointOfSales.DataCenter.Application.Interfaces;
using PointOfSales.DataCenter.Application.Interfaces.Repositories;
using PointOfSales.DataCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Application.Features.ProductFeatures.Queries
{
    public class GetAllProductsQuery : IRequest<Result<IEnumerable<ProductViewModel>>>
    {
        
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<IEnumerable<ProductViewModel>>>
        {
            private readonly IMapper _mapper;
            private readonly IProductRepositoryAsync _productRepository;
            private readonly IApplicationDbContext _context;
            public GetAllProductsQueryHandler(IMapper mapper, IApplicationDbContext context, IProductRepositoryAsync productRepository)
            {
                _mapper = mapper;
                _context = context;
                _productRepository = productRepository;
            }
            public async Task<Result<IEnumerable<ProductViewModel>>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
            {
                var productList = await _productRepository.ViewModelListAllAsync();
                if (productList == null)
                {
                    return Result<IEnumerable<ProductViewModel>>.Failure($"Product List Empty.");
                }
                return Result<IEnumerable<ProductViewModel>>.Success(productList);
            }
        }
    }
}
