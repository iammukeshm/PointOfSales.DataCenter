using PointOfSales.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.Domain.Entities.Sales
{
    public class Sale : AuditableEntity
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public decimal Tax { get; set; }
        public decimal SubTotal { get; set; }
        public IEnumerable<SaleDetail> SaleDetails { get; set; } 
    }
}
