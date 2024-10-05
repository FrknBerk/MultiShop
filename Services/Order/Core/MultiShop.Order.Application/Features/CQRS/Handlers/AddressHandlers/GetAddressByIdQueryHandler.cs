using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Features.CQRS.Queries.AddressQueries;
using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public class GetAddressByIdQueryHandler
    {
        private readonly IRepository<Address> _addressRepository;

        public GetAddressByIdQueryHandler(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<GetAddressByIdQueryResult> Handle(GetAddressByIdQuery getAddressByIdQuery)
        {
            var values = await _addressRepository.GetByIdAsync(getAddressByIdQuery.Id);
            return new GetAddressByIdQueryResult
            {
                AddressId = values.AddressId,
                City = values.City,
                Detail1 = values.Detail1,
                District =  values.District,
                UserId = values.UserId,
                Country = values.Country,
                Description = values.Description,
                Detail2 = values.Detail2,
                Email = values.Email,
                PhoneNumber = values.PhoneNumber,
                ZipCode = values.ZipCode,
                Name = values.Name,
                Surname = values.Surname
            };
        }
    }
}
