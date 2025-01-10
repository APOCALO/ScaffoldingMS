﻿using Application.Common;
using Application.Interfaces;
using AutoMapper;
using Domain.Customers;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Customers.Commands.UpdateCustomer
{
    internal sealed class UpdateCustomerCommandHandler : ApiBaseHandler<UpdateCustomerCommand, Unit>
    {
        private readonly IBaseRepository<Customer, CustomerId> _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(IBaseRepository<Customer, CustomerId> customerRepository, IUnitOfWork unitOfWork, ILogger<UpdateCustomerCommandHandler> logger, IMapper mapper) : base(logger)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        protected async override Task<ErrorOr<ApiResponse<Unit>>> HandleRequest(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!await _customerRepository.ExistsAsync(new CustomerId(request.Id), cancellationToken))
            {
                return Error.NotFound("Customer.NotFound", "The customer with the provide Id was not found.");
            }

            if (PhoneNumber.Create(request.PhoneNumber, request.CountryCode) is not PhoneNumber phoneNumber)
            {
                return Error.Validation("CreateCustomer.PhoneNumber", "PhoneNumber has not valid format.");
            }

            if (Address.Create(request.Country, request.Department, request.City, request.StreetType, request.StreetNumber, request.CrossStreetNumber, request.PropertyNumber, request.ZipCode) is not Address address)
            {
                return Error.Validation("CreateCustomer.Address", "Address has not valid format.");
            }

            var customer = _mapper.Map<Customer>(request);

            _customerRepository.Update(customer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new ApiResponse<Unit>(Unit.Value, true);

            return response;
        }
    }
}
