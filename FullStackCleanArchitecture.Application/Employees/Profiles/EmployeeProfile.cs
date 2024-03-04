using AutoMapper;
using FullStackCleanArchitecture.Domain.Common;
using FullStackCleanArchitecture.Domain.Entities;

namespace FullStackCleanArchitecture.Application
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            
            CreateMap<Employee, EmployeeDto>().ReverseMap();

            CreateMap<EmployeeSaveDto, Employee>()
            .ForMember(f => f.FullName, m => m.MapFrom(s => s.FirstName != null && s.LastName != null ? string.Format(BaseConst.FULL_NAME_FORMAT, s.FirstName.Trim().ToUpper(), s.LastName.Trim()) : null));
        }
    }
}
