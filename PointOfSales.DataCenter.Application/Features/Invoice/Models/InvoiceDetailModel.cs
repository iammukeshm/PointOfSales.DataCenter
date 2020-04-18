using PointOfSales.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.DataCenter.Application.Features.Invoice.ViewModels
{
    public class InvoiceDetailModel
    {
        public int ProductId { get; set; }
        public int QuantityInCart { get; set; }
    }
}
