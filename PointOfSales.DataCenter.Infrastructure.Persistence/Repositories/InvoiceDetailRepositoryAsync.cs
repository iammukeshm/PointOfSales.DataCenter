using AutoMapper;
using PointOfSales.DataCenter.Application.Interfaces.Repositories;
using PointOfSales.DataCenter.Infrastructure.Persistence.Context;
using PointOfSales.Domain.Entities.Invoice;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.DataCenter.Infrastructure.Persistence.Repositories
{
    class InvoiceDetailRepositoryAsync : GenericRepositoryAsync<InvoiceDetail>, IInvoiceDetailRepositoryAsync
    {
        private readonly IMapper _mapper;
        public InvoiceDetailRepositoryAsync(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }
    }
}
