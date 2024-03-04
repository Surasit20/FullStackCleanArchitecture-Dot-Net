using FullStackCleanArchitecture.Shared.Common.Models;
namespace FullStackCleanArchitecture.Web.Managers.Employee
{
    public interface IEmployeeManger
    {
        Task<Response<IEnumerable<EmployeeDto>>> GetAllAsync();
        Task<Response<EmployeeDto>> GetAsync(int Id);
        Task<Response<int>> SaveAsync(EmployeeSaveDto request);
        Task<Response<int>> PatchAsync(int Id, EmployeeSaveDto request);
        Task<Response<int>> DeleteAsync(int Id);
    }
}
