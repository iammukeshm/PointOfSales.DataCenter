using System.Collections.Generic;

namespace PointOfSales.DataCenter.Application.Features.Invoice.ViewModels
{
    public class InvoiceModel
    {
        public List<InvoiceDetailModel> InvoiceDetails { get; set; }
        public int CustomerId { get; set; }
    }
}
