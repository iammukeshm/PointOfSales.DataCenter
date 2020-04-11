using PointOfSales.Domain.Common;
using PointOfSales.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.Domain.Entities.Sales
{
    public class SaleDetail : AuditableEntity
    {
        public int Id { get; set; }
        
        public Product Product { get; set; }

    }
}
