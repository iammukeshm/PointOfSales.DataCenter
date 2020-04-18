using AutoMapper;
using MediatR;
using PointOfSales.DataCenter.Application.DTOs;
using PointOfSales.DataCenter.Application.Interfaces;
using PointOfSales.DataCenter.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Entity = PointOfSales.Domain.Entities.People;

namespace PointOfSales.DataCenter.Application.Features.Person.Queries
{

    public class GetAllPeopleQuery : IRequest<Result<IEnumerable<Entity.Person>>>
    {

        public class GetAllPeopleQueryHandler : IRequestHandler<GetAllPeopleQuery, Result<IEnumerable<Entity.Person>>>
        {
            private readonly IMapper _mapper;
            private readonly IPersonRepositoryAsync _personRepository;
            private readonly IApplicationDbContext _context;
            public GetAllPeopleQueryHandler(IMapper mapper, IApplicationDbContext context, IPersonRepositoryAsync personRepository)
            {
                _mapper = mapper;
                _context = context;
                _personRepository = personRepository;
            }
            public async Task<Result<IEnumerable<Entity.Person>>> Handle(GetAllPeopleQuery query, CancellationToken cancellationToken)
            {
                var peopleList = await _personRepository.ListAllAsync();
                if (peopleList == null || peopleList.Count == 0)
                {
                    return Result<IEnumerable<Entity.Person>>.Failure($"No one is registered.");
                }
                return Result<IEnumerable<Entity.Person>>.Success(peopleList);
            }
        }
    }
}
