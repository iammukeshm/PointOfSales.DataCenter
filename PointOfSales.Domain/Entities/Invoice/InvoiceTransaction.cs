using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.Domain.Entities.Invoice
{
    public class InvoiceTransaction
    {
        public int Id { get; set; }
        public Invoice Invoice { get; set; }
        public int InvoiceId { get; set; }

        public decimal Amount { get; set; }
        public string Remarks { get; set; }
    }
}
