using AutoMapper;
using MediatR;
using PointOfSales.DataCenter.Application.DTOs;
using PointOfSales.DataCenter.Application.Features.Product.ViewModels;
using PointOfSales.DataCenter.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Application.Features.Product.Queries
{
    public class GetProductGroupByIdQuery : IRequest<Result<ProductGroupViewModel>>
    {
        public int Id;
        public class GetProductGroupByIdQueryHandler : IRequestHandler<GetProductGroupByIdQuery, Result<ProductGroupViewModel>>
        {
            private readonly IMapper _mapper;
            private readonly IProductGroupRepositoryAsync _productGroupRepository;
            public GetProductGroupByIdQueryHandler(IMapper mapper, IProductGroupRepositoryAsync productGroupRepository)
            {
                _mapper = mapper;
                _productGroupRepository = productGroupRepository;
            }
            public async Task<Result<ProductGroupViewModel>> Handle(GetProductGroupByIdQuery query, CancellationToken cancellationToken)
            {
                var productGroup = await _productGroupRepository.GetByIdAsync(query.Id);
                var result = _mapper.Map<ProductGroupViewModel>(productGroup);
                if (result == null)
                {
                    return Result<ProductGroupViewModel>.Failure($"Product Group Not Found.");
                }
                return Result<ProductGroupViewModel>.Success(result);
            }
        }
    }
}
