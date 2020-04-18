using AutoMapper;
using MediatR;
using PointOfSales.DataCenter.Application.DTOs;
using PointOfSales.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Application.Features.Invoice.Commands
{
    public class CreateInvoiceCommand : IRequest<Result<int>>
    {
        public IReadOnlyList<Product> Products;
        public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Result<int>>
        {
            private readonly IMapper _mapper;
            private readonly IProductRepositoryAsync _productRepository;
            public CreateProductCommandHandler(IMapper mapper, IProductRepositoryAsync productRepository)
            {
                _mapper = mapper;
                _productRepository = productRepository;
            }
            public async Task<Result<int>> Handle(CreateInvoiceCommand command, CancellationToken cancellationToken)
            {
                var product = _mapper.Map<Product>(command);
                if (await _productRepository.DoesBarCodeExist(product.Barcode))
                {
                    return Result<int>.Failure($"Product with Barcode {product.Barcode} already exists.");
                }
                var result = await _productRepository.AddAsync(product);
                return Result<int>.Success(result.Id);
            }
        }
    }
}
