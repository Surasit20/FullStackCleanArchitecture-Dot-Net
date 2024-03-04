using FullStackCleanArchitecture.Application.Common.Interfaces;
using FullStackCleanArchitecture.Domain.Common;
using FullStackCleanArchitecture.Domain.Entities;

namespace FullStackCleanArchitecture.Application.Employees.Commands.UpdateEmployee;

public record UpdateEmployeeCommand(EmployeeSaveDto args) : IRequest<Response<int>>
{
    public EmployeeSaveDto Args = args;
}

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Response<int>>
{
    private readonly IApplicationDbContext _context;

    public UpdateEmployeeCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Response<int>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var res = new Response<int>();
            var args = request.Args;
            var employee = await _context.Employees.FirstOrDefaultAsync(f => f.EmployeeId == args.EmployeeId && f.IsDelete == false);

            if (employee != null) 
            {
                employee.FirstName = args.FirstName;
                employee.LastName = args.LastName;
                employee.FullName = string.Format(BaseConst.FULL_NAME_FORMAT, args.FirstName?.Trim(), args.LastName?.Trim());

                _context.Employees.Update(employee);
                await _context.SaveChangesAsync(cancellationToken);
                return res.Success(employee.EmployeeId, BaseConst.UPDATE_SUCCESS);
            }
            else
            {
                return res.Failure(BaseConst.NOT_FOUND);
            }
        }
        catch (Exception ex)
        {
            return new Response<int>().Failure(ex.Message);
        }
    }
}
