using FullStackCleanArchitecture.Application.Employees.Commands.CreateEmployee;
using FullStackCleanArchitecture.Application.Employees.Commands.DeleteEmployee;
using FullStackCleanArchitecture.Application.Employees.Commands.UpdateEmployee;
using FullStackCleanArchitecture.Application.Employees.Queries.GetEmployees;
using FullStackCleanArchitecture.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Controllers.v1;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FullStackCleanArchitecture.API.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : BaseController
    {
        public EmployeesController(ISender sender) : base(sender)
        {
        }
        [HttpGet("GetEmployees", Name = "GetEmployees")]
        public async Task<Response<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            return await _sender.Send(new GetEmployeesQuery());
        }

        [HttpGet("GetEmployee/{id}", Name = "GetEmployeeById")]
        public async Task<Response<EmployeeDto>> GetEmployeeById(int id)
        {
            return await _sender.Send(new GetEmployeesByIdQuery(id));
        }

        [HttpPost("CreateEmployee", Name = "CreateEmployee")]
        public async Task<Response<int>> CreateEmployee([FromBody] EmployeeSaveDto args)
        {
            return await _sender.Send(new CreateEmployeeCommand(args));
        }

        [HttpPatch("UpdateEmployee/{id}", Name = "UpdateEmployee")]
        public async Task<Response<int>> UpdateEmployees(int Id, [FromBody] EmployeeSaveDto args)
        {
           if (Id != args.EmployeeId) return new Response<int>().Failure(BaseConst.NOT_FOUND);

           return await _sender.Send(new UpdateEmployeeCommand(args));
        }

        [HttpDelete("DeleteEmployee/{id}", Name = "DeleteEmployee")]
        public async Task<Response<int>> DeleteEmployee(int Id)
        {
            return await _sender.Send(new DeleteEmployeeCommand(Id));
        }
    }
}   
