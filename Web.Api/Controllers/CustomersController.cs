﻿using Application.Common;
using Application.Customers.Commands.CreateCustomer;
using Application.Customers.Commands.DeleteCustomer;
using Application.Customers.Commands.UpdateCustomer;
using Application.Customers.Queries.GetAllCustomersPaged;
using Application.Customers.Queries.GetCustomerById;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class CustomersController : ApiBaseController
    {
        private readonly ISender _mediator;

        public CustomersController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Get all customers paginated
        /// </summary>
        /// <param name="pagination">Pagination parameters</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCustomersPagedAsync([FromQuery] PaginationParameters pagination)
        {
            var result = await _mediator.Send(new GetAllCustomersPagedAsyncQuery(pagination));

            return result.Match(
                customers => Ok(customers),
                errors => Problem(errors)
            );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery(id));

            return result.Match(
                customer => Ok(customer),
                errors => Problem(errors)
            );
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match(
                customer => Created(),
                errors => Problem(errors)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerCommand command)
        {
            if (command.Id != id)
            {
                List<Error> errors = new()
                {
                    Error.Validation("Customer.UpdateInvalid", "The request Id does not match with the url Id.")
                };

                return Problem(errors);
            }

            var result = await _mediator.Send(command);

            return result.Match(
                Id => NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteResult = await _mediator.Send(new DeleteCustomerCommand(id));

            return deleteResult.Match(
                Id => NoContent(),
                errors => Problem(errors)
            );
        }
    }
}
