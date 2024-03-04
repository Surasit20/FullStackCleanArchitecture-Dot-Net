using FullStackCleanArchitecture.Application.Common.Interfaces;
using FullStackCleanArchitecture.Domain.Common;
using FullStackCleanArchitecture.Domain.Entities;

namespace FullStackCleanArchitecture.Application.Employees.Commands.DeleteEmployee;

public record DeleteEmployeeCommand(int Id) : IRequest<Response<int>>;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Response<int>>
{
    private readonly IApplicationDbContext _context;

    public DeleteEmployeeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Response<int>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var res = new Response<int>();
            var employee = await _context.Employees
            .Where(f => f.EmployeeId == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
            if (employee != null) 
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync(cancellationToken);
                return res.Success(employee.EmployeeId, BaseConst.DELETE_SUCCESS);
            }
            else
            {
                return res.Success(employee.EmployeeId, BaseConst.NOT_FOUND);
            }
            
        }
        catch (Exception ex)
        {
            return new Response<int>().Failure(ex.Message);
        }
    }
}
