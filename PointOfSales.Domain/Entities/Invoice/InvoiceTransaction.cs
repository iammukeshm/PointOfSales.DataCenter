using PointOfSales.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.Domain.Entities.Invoice
{
    public class InvoiceTransaction : AuditableEntity
    {
        public int Id { get; set; }
        public Invoice Invoice { get; set; }
        public int InvoiceId { get; set; }

        public decimal Amount { get; set; }
        public string Remarks { get; set; }
    }
}
