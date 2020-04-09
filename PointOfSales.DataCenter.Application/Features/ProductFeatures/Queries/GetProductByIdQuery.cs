using AutoMapper;
using MediatR;
using PointOfSales.DataCenter.Application.DTOs;
using PointOfSales.DataCenter.Application.Features.ProductFeatures.ViewModels;
using PointOfSales.DataCenter.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Application.Features.ProductFeatures.Queries
{
    public class GetProductByIdQuery : IRequest<Result<ProductViewModel>>
    {
        public int id;
        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductViewModel>>
        {
            private readonly IMapper _mapper;
            private readonly IProductRepositoryAsync _productRepository;
            public GetProductByIdQueryHandler(IMapper mapper, IProductRepositoryAsync productRepository)
            {
                _mapper = mapper;
                _productRepository = productRepository;
            }
            public async Task<Result<ProductViewModel>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(query.id);
                var result = _mapper.Map<ProductViewModel>(product);
                if (result == null)
                {
                    return Result<ProductViewModel>.Failure($"Product Not Found.");
                }
                return Result<ProductViewModel>.Success(result);
            }
        }
    }
}
