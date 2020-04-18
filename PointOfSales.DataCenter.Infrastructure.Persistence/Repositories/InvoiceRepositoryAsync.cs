using AutoMapper;
using PointOfSales.DataCenter.Application.Interfaces.Repositories;
using PointOfSales.DataCenter.Infrastructure.Persistence.Context;
using PointOfSales.Domain.Entities.Invoice;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.DataCenter.Infrastructure.Persistence.Repositories
{
    class InvoiceRepositoryAsync : GenericRepositoryAsync<Invoice>, IInvoiceRepositoryAsync
    {
        private readonly IMapper _mapper;
        public InvoiceRepositoryAsync(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }
    }
}
