using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.DataCenter.Application.Features.Invoice.ViewModels
{
    public class InvoiceModel
    {
        public List<InvoiceDetailModel> InvoiceDetails { get; set; }
        public int CustomerId { get; set; }
    }
}
