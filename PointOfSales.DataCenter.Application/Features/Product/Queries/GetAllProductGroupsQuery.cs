using AutoMapper;
using MediatR;
using PointOfSales.DataCenter.Application.DTOs;
using PointOfSales.DataCenter.Application.Features.Product.ViewModels;
using PointOfSales.DataCenter.Application.Interfaces;
using PointOfSales.DataCenter.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Application.Features.Product.Queries
{
    public class GetAllProductGroupsQuery : IRequest<Result<IEnumerable<ProductGroupViewModel>>>
    {

        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductGroupsQuery, Result<IEnumerable<ProductGroupViewModel>>>
        {
            private readonly IMapper _mapper;
            private readonly IProductGroupRepositoryAsync _productGroupRepository;
            private readonly IApplicationDbContext _context;
            public GetAllProductsQueryHandler(IMapper mapper, IApplicationDbContext context, IProductGroupRepositoryAsync productRepository)
            {
                _mapper = mapper;
                _context = context;
                _productGroupRepository = productRepository;
            }
            public async Task<Result<IEnumerable<ProductGroupViewModel>>> Handle(GetAllProductGroupsQuery query, CancellationToken cancellationToken)
            {
                var productList = await _productGroupRepository.ViewModelListAllAsync();
                if (productList == null)
                {
                    return Result<IEnumerable<ProductGroupViewModel>>.Failure($"Product List Empty.");
                }
                return Result<IEnumerable<ProductGroupViewModel>>.Success(productList);
            }
        }
    }
}
