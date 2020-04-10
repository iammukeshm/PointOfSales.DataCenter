using PointOfSales.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.Domain.Entities
{
    public class ProductGroup : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public int ProductCategoryId { get; set; }
        public int Tenant { get; set; }
    }
}
