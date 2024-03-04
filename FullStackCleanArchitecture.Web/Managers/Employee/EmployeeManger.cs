using FullStackCleanArchitecture.Shared.Common.Models;
using FullStackCleanArchitecture.Web.Extensions;
using System.Net.Http.Json;

namespace FullStackCleanArchitecture.Web.Managers.Employee
{
    public class EmployeeManger : IEmployeeManger
    {
        private readonly HttpClient _httpClient;

        public EmployeeManger(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Response<IEnumerable<EmployeeDto>>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(Routes.EmployeeEndpoints.GetAll);
            return await response.ToResult<Response<IEnumerable<EmployeeDto>>>();
        }

        public async Task<Response<EmployeeDto>> GetAsync(int Id)
        {
            var response = await _httpClient.GetAsync(Routes.EmployeeEndpoints.Get + Id);
            return await response.ToResult<Response<EmployeeDto>>();
        }

        public async Task<Response<int>> SaveAsync(EmployeeSaveDto request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.EmployeeEndpoints.Save, request);
            return await response.ToResult<Response<int>>();
        }

        public async Task<Response<int>> PatchAsync(int Id, EmployeeSaveDto request)
        {
            var response = await _httpClient.PatchAsJsonAsync(Routes.EmployeeEndpoints.Patch + Id, request);
            return await response.ToResult<Response<int>>();
        }
        public async Task<Response<int>> DeleteAsync(int Id)
        {
            var response = await _httpClient.DeleteAsync(Routes.EmployeeEndpoints.Delete + Id);
            return await response.ToResult<Response<int>>();
        }
    }
}
