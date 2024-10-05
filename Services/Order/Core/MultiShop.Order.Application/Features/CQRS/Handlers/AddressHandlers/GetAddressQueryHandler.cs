using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
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
    public class GetAddressQueryHandler
    {
        private readonly IRepository<Address> _addressRepository;

        public GetAddressQueryHandler(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<List<GetAddressQueryResult>> Handle()
        {
            var values = await _addressRepository.GetAllAsync();
            return values.Select(x => new GetAddressQueryResult
            {
                AddressId = x.AddressId,
                City = x.City,
                Detail1 = x.Detail1,
                District = x.District,
                UserId = x.UserId,
                Country = x.Country,
                Description = x.Description,
                Detail2 = x.Detail2,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                ZipCode = x.ZipCode,
                Name = x.Name,
                Surname = x.Surname
            }).ToList();
        }
    }
}
