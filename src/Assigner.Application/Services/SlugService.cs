using Assigner.Application.Common;
using Assigner.Application.DTOs;
using Assigner.Application.Interfaces;
using Assigner.Application.IRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Assigner.Application.Services
{
    public class SlugService : ISlugService
    {
        private readonly ISlugRepository _slugRepository;
        private readonly ILogger<SlugService> _logger;

        public SlugService(
        ISlugRepository slugRepository,
        ILogger<SlugService> logger)
        {
            _slugRepository = slugRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<AssignSlugResponse>> AssignSlugAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var slug = await _slugRepository.AssignAvailableSlugAsync(cancellationToken);

                if (slug == null)
                {
                    throw new InvalidOperationException(
                        "No available slug found.");
                }

                _logger.LogInformation(
                    "Slug {Slug} assigned",
                    slug.Value);

                var response = new AssignSlugResponse(
                    slug: slug.Value
                    );



                return ApiResponse<AssignSlugResponse>.SuccessResponse(
                    response,
                    "Slug assigned successfully."
                    );

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
