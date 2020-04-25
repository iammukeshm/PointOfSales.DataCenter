using AutoMapper;
using MediatR;
using PointOfSales.DataCenter.Application.DTOs;
using PointOfSales.DataCenter.Application.Features.Invoice.ViewModels;
using PointOfSales.DataCenter.Application.Interfaces.Jobs;
using PointOfSales.DataCenter.Application.Interfaces.Repositories;
using PointOfSales.Domain.Entities.Invoice;
using PointOfSales.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entity = PointOfSales.Domain.Entities.Invoice;

namespace PointOfSales.DataCenter.Application.Features.Invoice.Commands
{
    public class CreateInvoiceCommand : IRequest<Result<int>>
    {
        public InvoiceModel invoice { get; set; }
        public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Result<int>>
        {
            private readonly IInvoiceRepositoryAsync _invoiceRepository;
            private readonly IInvoiceDetailRepositoryAsync _invoiceDetailRepository;
            private readonly IProductRepositoryAsync _productRepository;
            private readonly IPersonRepositoryAsync _personRepository;
            private readonly IEmailScheduler _emailScheduler;
            public CreateInvoiceCommandHandler(IInvoiceRepositoryAsync invoiceRepository, IProductRepositoryAsync productRepository, IPersonRepositoryAsync personRepositoryAsync, IInvoiceDetailRepositoryAsync invoiceDetailRepository, IEmailScheduler emailScheduler)
            {
                _invoiceRepository = invoiceRepository;
                _personRepository = personRepositoryAsync;
                _productRepository = productRepository;
                _invoiceDetailRepository = invoiceDetailRepository;
                _emailScheduler = emailScheduler;
            }
            public async Task<Result<int>> Handle(CreateInvoiceCommand command, CancellationToken cancellationToken)
            {
                decimal taxRate = 0.14m;
                //Check if customer is valid
                var customer = await _personRepository.GetByIdAsync(command.invoice.CustomerId);
                if(customer == null)
                {
                    return Result<int>.Failure($"Customer with ID {command.invoice.CustomerId} is either not active or not registered yet.");
                }
                //Create Invoice Detail List Model
                var invoiceDetails = new List<Entity.InvoiceDetail>();
                foreach (var item in command.invoice.InvoiceDetails)
                {
                    var detail = new InvoiceDetail
                    {
                        ProductId = item.ProductId,
                        Quantity = item.QuantityInCart
                    };

                    //Get Information about this product
                    var thisProduct = await _productRepository.GetByIdAsync(detail.ProductId);
                    //Check if Products are valid
                    if (thisProduct == null)
                    {
                        return Result<int>.Failure($"Product with ID {detail.ProductId} is either not active or not registered yet.");
                    }
                    detail.Rate = thisProduct.Rate;
                    detail.SubTotal = (detail.Rate * detail.Quantity);
                    //TODO: Get Tax Rate
                    if (thisProduct.IsTaxable)
                    {
                        
                        detail.Tax = taxRate * detail.SubTotal;
                        detail.Total = (detail.SubTotal + detail.Tax);
                    }
                    else
                    {
                        detail.Total = detail.SubTotal;
                    }

                    invoiceDetails.Add(detail);

                }
                //Create Invoice Model
                var invoice = new Entity.Invoice
                {
                    SubTotal = invoiceDetails.Sum(z => z.SubTotal),
                    Tax = invoiceDetails.Sum(z => z.Tax)
                };
                invoice.Total = invoice.SubTotal + invoice.Tax;
                invoice.CustomerId = command.invoice.CustomerId;
                invoice.Due = invoice.Total;

                //Save Invoice and Get ID
                var newInvoice = await _invoiceRepository.AddAsync(invoice);
                //Save Invoice Details
                foreach(var item in invoiceDetails)
                {
                    item.InvoiceId = newInvoice.Id;
                    await _invoiceDetailRepository.AddAsync(item);
                }
                //Email
                //_emailScheduler.Schedule(customer.Email, "Invoice Created!", $"Your Invoice has been generated!. Your Due Amount is '{ invoice.Total}'.");
                return Result<int>.Success(newInvoice.Id);
                
            }
        }
    }
}
