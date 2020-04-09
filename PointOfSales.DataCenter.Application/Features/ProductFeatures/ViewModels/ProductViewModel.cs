using AutoMapper;
using PointOfSales.DataCenter.Application.Mappings;
using PointOfSales.DataCenter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.DataCenter.Application.Features.ProductFeatures.ViewModels
{
    public class ProductViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public bool IsActive { get; set; } = true;
        public string Description { get; set; }

        public int ProductGroupId { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductViewModel>();
            profile.CreateMap<ProductViewModel, Product>();
        }
    }
}
