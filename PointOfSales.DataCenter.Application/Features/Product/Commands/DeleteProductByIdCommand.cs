using AutoMapper;
using MediatR;
using PointOfSales.DataCenter.Application.DTOs;
using PointOfSales.DataCenter.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Application.Features.ProductFeatures.Commands
{
    public class DeleteProductByIdCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, Result<int>>
        {
            private readonly IProductRepositoryAsync _productRepository;
            public DeleteProductByIdCommandHandler(IProductRepositoryAsync productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<Result<int>> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(command.Id);
                
                if (product==null)
                {
                    return Result<int>.Failure($"Product with Id {command.Id} does not exist.");
                }
                else
                {
                    //TODO - Only Admins should be able to delete
                    await _productRepository.DeleteAsync(product);
                    return Result<int>.Success($"Product with Id {command.Id} deleted.",command.Id);
                }                
            }
        }
    }
}
