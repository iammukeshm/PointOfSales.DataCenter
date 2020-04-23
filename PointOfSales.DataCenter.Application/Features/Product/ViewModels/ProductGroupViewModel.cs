using AutoMapper;
using PointOfSales.DataCenter.Application.Mappings;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.DataCenter.Application.Features.Product.ViewModels
{
    public class ProductGroupViewModel : IMapFrom<Domain.Entities.Products.ProductGroup>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Products.ProductGroup, ProductGroupViewModel>();
            profile.CreateMap<ProductGroupViewModel, Domain.Entities.Products.ProductGroup>();
        }
    }
}
