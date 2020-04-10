using PointOfSales.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.Domain.Entities
{
    public class Product :AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public bool IsActive { get; set; } = true;
        public string Description { get; set; }
        public bool DisableIfStockIsZero { get; set; } = true;
        public ProductGroup ProductGroup { get; set; }

        public int ProductGroupId { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
    }
}
