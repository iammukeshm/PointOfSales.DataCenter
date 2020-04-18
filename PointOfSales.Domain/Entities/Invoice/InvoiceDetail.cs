using PointOfSales.Domain.Common;
using PointOfSales.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.Domain.Entities.Invoice
{
    public class InvoiceDetail : AuditableEntity
    {
        public int Id { get; set; }
        public Invoice Invoice { get; set; }
        public int InvoiceId { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public decimal Difference { get; set; }
    }
}
