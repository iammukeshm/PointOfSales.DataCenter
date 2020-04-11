using AutoMapper;
using MediatR;
using PointOfSales.DataCenter.Application.DTOs;
using PointOfSales.DataCenter.Application.Interfaces.Repositories;
using PointOfSales.DataCenter.Application.Mappings;
using PointOfSales.Domain.Entities.Products;
using System.Threading;
using System.Threading.Tasks;

namespace PointOfSales.DataCenter.Application.Features.ProductFeatures.Commands
{
    public class UpdateProductCommand : IRequest<Result<int>>, IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int ProductGroupId { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, UpdateProductCommand>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            profile.CreateMap<UpdateProductCommand, Product>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<int>>
        {
            private readonly IMapper _mapper;
            private readonly IProductRepositoryAsync _productRepository;
            public UpdateProductCommandHandler(IProductRepositoryAsync productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }
            public async Task<Result<int>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(command.Id);

                if (product == null)
                {
                    return Result<int>.Failure($"Product with Id {command.Id} does not exist.");
                }
                else
                {
                    product.BuyingPrice = command.BuyingPrice;
                    product.RetailPrice = command.SellingPrice;
                    product.IsActive = command.IsActive;
                    product.Name = command.Name;
                    await _productRepository.UpdateAsync(product);
                    return Result<int>.Success(product.Id);

                }
            }
        }
    }
}
