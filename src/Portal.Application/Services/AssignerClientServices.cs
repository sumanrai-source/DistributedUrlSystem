using Portal.Application.Common;
using Portal.Application.DTOs;
using Portal.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace Portal.Application.Services
{
    public class AssignerClientServices : IAssignerClientServices
    {
        private readonly HttpClient _httpClient;

        public AssignerClientServices(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        public async Task<string> AssignSlugAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsync("api/slugs/assign",null,cancellationToken);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<AssignSlugResponse>>(
                    cancellationToken: cancellationToken);

            if (result == null || !result.Success || result.Data == null || string.IsNullOrWhiteSpace(result.Data.slug))
            {
                throw new InvalidOperationException(result?.Message ?? "Assigner returned an invalid response.");
            }

            return result.Data.slug;

        }
    }
}
