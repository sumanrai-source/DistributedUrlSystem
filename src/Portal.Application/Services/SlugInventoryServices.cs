using Akka.Actor;
using Portal.Application.Common;
using Portal.Application.Interfaces;
using Portal.Application.IRepository;
using Portal.Application.Portal.Queries.GetAvailableSlug;
using Shared.Contracts.SlugData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Services
{
    public class SlugInventoryServices : ISlugInventoryServices
    {

        private readonly IAkkaActorProvider _actorProvider;

        public SlugInventoryServices(IAkkaActorProvider actorProvider)
        {
            _actorProvider = actorProvider;

        }

        public async Task<ApiResponse<List<GetAvailableSlugsResponse>>> GetAllSlugAsync()
        {
            var response = await _actorProvider.SlugResolver.Ask<object>(
                new GetAllSlug(),
                TimeSpan.FromSeconds(5));

            if (response is SlugResponseNotFound)
            {
                return ApiResponse<List<GetAvailableSlugsResponse>>.FailResponse(
                    "No slugs found.");
            }

            if (response is not SlugResponseFound slugResponse)
            {
                return ApiResponse<List<GetAvailableSlugsResponse>>.FailResponse(
                    "Invalid response from slug resolver.");
            }

            var result = slugResponse.Slugs
                .Select(x => new GetAvailableSlugsResponse(
                    x.Slug,
                    x.Status))
                .ToList();

            return ApiResponse<List<GetAvailableSlugsResponse>>.SuccessResponse(
                result,
                "Slugs retrieved successfully.");
        }
    }
}
