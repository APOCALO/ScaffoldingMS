﻿using Application.Common;
using Application.Interfaces;
using AutoMapper;
using Domain.Customers;
using Domain.Primitives;
using Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Customers.Commands.CreateCustomer
{
    internal sealed class CreateCustomerCommandHandler : ApiBaseHandler<CreateCustomerCommand, ErrorOr<ApiResponse<Unit>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICustomerRepository customerRepository, ILogger<CreateCustomerCommandHandler> logger)
            : base(logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        protected override async Task<ErrorOr<ApiResponse<Unit>>> HandleRequest(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (PhoneNumber.Create(request.PhoneNumber, request.CountryCode) is not PhoneNumber phoneNumber)
            {
                _logger.LogWarning("Invalid phone number format for {PhoneNumber} in {CountryCode}.", request.PhoneNumber, request.CountryCode);
                return Error.Validation("CreateCustomer.PhoneNumber", "PhoneNumber has not valid format.");
            }

            if (Address.Create(request.Country, request.Department, request.City, request.StreetType, request.StreetNumber, request.CrossStreetNumber, request.PropertyNumber, request.ZipCode) is not Address address)
            {
                _logger.LogWarning("Invalid address format for {Country} {Department} {City} {StreetType} {StreetNumber} {CrossStreetNumber} {PropertyNumber} {ZipCode}.", 
                    request.Country, 
                    request.Department, 
                    request.City, 
                    request.StreetType, 
                    request.StreetNumber, 
                    request.CrossStreetNumber, 
                    request.PropertyNumber, 
                    request.ZipCode);

                return Error.Validation("CreateCustomer.Address", "Address has not valid format.");
            }

            var customer = _mapper.Map<Customer>(request);

            await _customerRepository.AddAsync(customer, cancellationToken).ConfigureAwait(false);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new ApiResponse<Unit>(Unit.Value, true);

            return response;
        }
    }
}
