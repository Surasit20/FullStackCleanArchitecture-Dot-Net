using FullStackCleanArchitecture.Application.Common.Interfaces;
using FullStackCleanArchitecture.Application.Employees.Queries.GetEmployees;
using FullStackCleanArchitecture.Domain.Common;
using FullStackCleanArchitecture.Domain.Entities;

namespace FullStackCleanArchitecture.Application.Employees.Commands.CreateEmployee;

public record CreateEmployeeCommand(EmployeeSaveDto args) : IRequest<Response<int>>
{
    public EmployeeSaveDto Args = args;
}


public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Response<int>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public CreateEmployeeCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<int>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var res = new Response<int>();
            var args = request.Args;
            var employee  = _mapper.Map<Employee>(args);

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(cancellationToken);
            return res.Success(employee.EmployeeId, BaseConst.SAVE_SUCCESS);
        }
        catch (Exception ex)
        {
            return new Response<int>().Failure(ex.Message);
        }
    }
}
