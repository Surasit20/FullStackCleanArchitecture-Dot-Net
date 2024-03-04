using FullStackCleanArchitecture.Application.Common.Interfaces;
using FullStackCleanArchitecture.Domain.Common;
using FullStackCleanArchitecture.Domain.Entities;
namespace FullStackCleanArchitecture.Application.Employees.Queries.GetEmployees;

public record GetEmployeesByIdQuery(int employeeId) : IRequest<Response<EmployeeDto>>
{
}

public class GetEmployeesByIdQueryHandler : IRequestHandler<GetEmployeesByIdQuery, Response<EmployeeDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetEmployeesByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<EmployeeDto>> Handle(GetEmployeesByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var res = new Response<EmployeeDto>();
            int employeeId = request.employeeId;
            var employee = await _context.Employees
                                    .Where(f=> f.EmployeeId == employeeId && f.IsDelete == false)
                                    .AsNoTracking()
                                    .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                                    .OrderBy(o => o.FirstName)
                                    .FirstOrDefaultAsync();
            if(employee != null)
            {
                return res.Success(employee, BaseConst.GET_DATA_SUCCESS);
            }
            else
            {
                return res.Failure(BaseConst.NOT_FOUND);
            }
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDto>().Failure(ex.Message);
        }

    }
}
