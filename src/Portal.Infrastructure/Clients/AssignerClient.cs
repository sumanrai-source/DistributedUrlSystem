using Microsoft.Extensions.Logging;
using Portal.Application.DTOs;
using Portal.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace Portal.Infrastructure.Clients
{
    public class AssignerClient : IAssignerClientServices
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AssignerClient> _logger;

        public AssignerClient(
            HttpClient httpClient,
            ILogger<AssignerClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }


        public async Task<string> AssignSlugAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _httpClient.PostAsync(
                    "api/slugs/assign",
                    null,
                    cancellationToken);

                response.EnsureSuccessStatusCode();

                var result = await response.Content
                    .ReadFromJsonAsync<AssignSlugResponse>(
                        cancellationToken: cancellationToken);

                if (result == null ||
                    string.IsNullOrWhiteSpace(result.slug))
                {
                    throw new InvalidOperationException(
                        "Assigner returned invalid slug.");
                }

                return result.slug;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to assign slug from Assigner service");

                throw;
            }
        }
    }
}
