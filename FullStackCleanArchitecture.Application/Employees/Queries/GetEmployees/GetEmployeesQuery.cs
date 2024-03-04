using FullStackCleanArchitecture.Application.Common.Interfaces;
using FullStackCleanArchitecture.Domain.Common;
namespace FullStackCleanArchitecture.Application.Employees.Queries.GetEmployees;

public record GetEmployeesQuery : IRequest<Response<IEnumerable<EmployeeDto>>>;

public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, Response<IEnumerable<EmployeeDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<EmployeeDto>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var employees = await _context.Employees
                                    .Where(f=>f.IsDelete == false)
                                    .AsNoTracking()
                                    .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                                    .OrderBy(t => t.EmployeeId)
                                    .ToListAsync(cancellationToken);
            var res = new Response<IEnumerable<EmployeeDto>>();
            return res.Success(employees, BaseConst.GET_DATA_SUCCESS);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<EmployeeDto>>().Failure(ex.Message);
        }

    }
}
