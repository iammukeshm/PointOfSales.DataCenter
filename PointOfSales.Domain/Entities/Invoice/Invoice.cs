using PointOfSales.Domain.Entities.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.Domain.Entities.Invoice
{
	public class Invoice
	{ 
		public int Id { get; set; }
		public int InvoiceNumber { get; set; } 
		public Person Customer { get; set; }
		public int CustomerId { get; set; }
		public decimal SubTotal { get; set; }
		public decimal GivenDiscount { get; set; }
		public decimal Tax { get; set; }
		public decimal Total { get; set; }

		public decimal AmountReceived { get; set; }
		public decimal Due { get; set; }
		public decimal Difference { get; set; }

		public string Remarks { get; set; }
	}
}
