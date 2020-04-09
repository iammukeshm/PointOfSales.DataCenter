using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using PointOfSales.DataCenter.Application.DTOs;
using PointOfSales.DataCenter.Application.Interfaces;
using PointOfSales.DataCenter.Application.Interfaces.Repositories;
using PointOfSales.DataCenter.Application.Mappings;
using PointOfSales.DataCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Application.Features.ProductFeatures.Commands
{
    public class CreateProductCommand : IRequest<Result<int>>, IMapFrom<Product>
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int ProductGroupId { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, CreateProductCommand>();
            profile.CreateMap<CreateProductCommand, Product>();
        }
        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<int>>
        {
            private readonly IMapper _mapper;
            private readonly IProductRepositoryAsync _productRepository;
            public CreateProductCommandHandler(IMapper mapper, IProductRepositoryAsync productRepository)
            {
                _mapper = mapper;
                _productRepository = productRepository;
            }
            public async Task<Result<int>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
                var product = _mapper.Map<Product>(command);
                if(await _productRepository.DoesBarCodeExist(product.Barcode))
                {
                    return Result<int>.Failure($"Product with Barcode {product.Barcode} already exists.");
                }
                var result = await _productRepository.AddAsync(product);
                return Result<int>.Success(result.Id);
            }
        }
    }
}
