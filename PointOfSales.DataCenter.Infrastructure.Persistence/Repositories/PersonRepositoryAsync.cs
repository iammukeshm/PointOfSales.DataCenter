using AutoMapper;
using PointOfSales.DataCenter.Application.Interfaces.Repositories;
using PointOfSales.DataCenter.Infrastructure.Persistence.Context;
using PointOfSales.Domain.Entities.People;
using System;
using System.Collections.Generic;
using System.Text;

namespace PointOfSales.DataCenter.Infrastructure.Persistence.Repositories
{
    public class PersonRepositoryAsync : GenericRepositoryAsync<Person>, IPersonRepositoryAsync
    {
        private readonly IMapper _mapper;
        public PersonRepositoryAsync(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }
    }
}
