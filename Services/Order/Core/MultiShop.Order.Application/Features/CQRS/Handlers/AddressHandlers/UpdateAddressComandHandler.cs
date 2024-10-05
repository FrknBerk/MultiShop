using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public class UpdateAddressComandHandler
    {
        private readonly IRepository<Address> _repository;

        public UpdateAddressComandHandler(IRepository<Address> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateAddressCommand command)
        {
            var values = await _repository.GetByIdAsync(command.AddressId);
            values.Detail1 = command.Detail1;
            values.District = command.District;
            values.City = command.City;
            values.UserId = command.UserId;
            values.Country = command.Country;
            values.Description = command.Description;
            values.Detail2 = command.Detail2;
            values.Email = command.Email;
            values.PhoneNumber = command.PhoneNumber;
            values.ZipCode = command.ZipCode;
            values.Name = command.Name;
            values.Surname = command.Surname;
            await _repository.UpdateAsync(values);
        }
    }
}
