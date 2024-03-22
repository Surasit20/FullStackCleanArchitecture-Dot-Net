using FullStackCleanArchitecture.Application.Employees.Commands.CreateEmployee;
using FullStackCleanArchitecture.Domain.Entities;
using FullStackCleanArchitecture.Shared.DTOs;
using System.ComponentModel.DataAnnotations;


namespace FullStackCleanArchitecture.Application.FunctionalTests.Employees.Commands;

using static Testing;

public class CreateEmployeeTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateEmployee()
    {
        var employee = new EmployeeSaveDto()
        {
            FirstName = "FirstNameTest",
            LastName = "LastNameTest",
        };
        var command = new CreateEmployeeCommand(employee);
        var id = await SendAsync(command);
        var createdEmployee = await FindAsync<Employee>(id);

        createdEmployee.Should().NotBeNull();
        createdEmployee.FirstName.Should().Be(employee.FirstName);
        createdEmployee.LastName.Should().Be(employee.LastName);
    }
}
