using PointOfSales.DataDistribution.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.DataDistribution.Domain.Entities
{
    public class Product :AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public bool DisableIfStockIsZero { get; set; } = true;
        public ProductGroup ProductGroup { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
    }
}
