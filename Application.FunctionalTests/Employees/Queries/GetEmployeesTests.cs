using FullStackCleanArchitecture.Application.Employees.Queries.GetEmployees;
using FullStackCleanArchitecture.Domain.Entities;

namespace FullStackCleanArchitecture.Application.FunctionalTests.TodoLists.Queries;

using static Testing;

public class GetEmployeesTests : BaseTestFixture
{
 
    [Test]
    public async Task ShouldReturnAllListsAndItems()
    {

        await AddAsync(new Employee
        {
         LastName = "Test",
         FirstName = "Test",
         IsDelete = false,
        });

        var query = new GetEmployeesQuery();

        var result = await SendAsync(query);

        result.Result.Should().HaveCount(1);
    }
}
