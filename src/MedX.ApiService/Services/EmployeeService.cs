﻿using MedX.ApiService.Models.Employees;

namespace MedX.ApiService.Services
{
    public class EmployeeService(HttpClient client) : IEmployeeService
    {
        private readonly string baseUrl = $"{HttpConstant.BaseLink}Employees/";

        public async Task<Response<EmployeeResultDto>> AddAsync(EmployeeCreationDto dto)
        {
            using var multipartFormContent = ConvertHelper.ConvertToMultipartFormContent(dto);
            using var response = await client.PostAsync($"{baseUrl}create", multipartFormContent);
            if (!response.IsSuccessStatusCode)
                return default!;

            return (await response.Content.ReadFromJsonAsync<Response<EmployeeResultDto>>())!;
        }

        public async Task<Response<EmployeeResultDto>> UpdateAsync(EmployeeUpdateDto dto)
        {
            using var multipartFormContent = ConvertHelper.ConvertToMultipartFormContent(dto);
            using var response = await client.PutAsync($"{baseUrl}update", multipartFormContent);
            if (!response.IsSuccessStatusCode)
                return default!;

            return (await response.Content.ReadFromJsonAsync<Response<EmployeeResultDto>>())!;
        }

        public async Task<Response<bool>> DeleteAsync(long id)
        {
            using var response = await client.DeleteAsync($"{baseUrl}delete/{id}");
            if (!response.IsSuccessStatusCode)
                return default!;

            return (await response.Content.ReadFromJsonAsync<Response<bool>>())!;
        }

        public async Task<Response<EmployeeResultDto>> GetAsync(long id)
        {
            using var response = await client.GetAsync($"{baseUrl}get/{id}");
            if (!response.IsSuccessStatusCode)
                return default!;

            return (await response.Content.ReadFromJsonAsync<Response<EmployeeResultDto>>())!;
        }

        public async Task<Response<IEnumerable<EmployeeResultDto>>> GetAllAsync(PaginationParams @params, string search = null!)
        {
            var queryParams = new Dictionary<string, string>
            {
                { nameof(@params.PageIndex), @params.PageIndex.ToString() },
                { nameof(@params.PageSize), @params.PageSize.ToString() },
                { nameof(search), search }
            };

            var queryString = string.Join("&", queryParams.Where(p => !string.IsNullOrEmpty(p.Value)).Select(p => $"{p.Key}={p.Value}"));
            using var response = await client.GetAsync($"{baseUrl}get-all?{queryString}");

            if (!response.IsSuccessStatusCode)
                return default!;

            return (await response.Content.ReadFromJsonAsync<Response<IEnumerable<EmployeeResultDto>>>())!;
        }
    }
}
