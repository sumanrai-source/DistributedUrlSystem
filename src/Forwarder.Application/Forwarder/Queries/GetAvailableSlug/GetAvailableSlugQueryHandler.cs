using AutoMapper;
using Forwarder.Application.Common;
using Forwarder.Application.Forwarder.Queries.DestinationUrlBySlug;
using Forwarder.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forwarder.Application.Forwarder.Queries.GetAvailableSlug
{
    public class GetAvailableSlugsQueryHandler : IRequestHandler<GetAvailableSlugsQuery, ApiResponse<List<GetAvailableSlugsResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IRedirectService _redirectService;

        public GetAvailableSlugsQueryHandler(IMapper mapper, IRedirectService redirectService)
        {
            _mapper = mapper;
            _redirectService = redirectService;

        }
        public async Task<ApiResponse<List<GetAvailableSlugsResponse>>> Handle(GetAvailableSlugsQuery request, CancellationToken cancellationToken)
        {
            var entityName = typeof(DestinationUrlBySlugQuery).Name
                .Replace("Query", string.Empty);
            try
            {
                var queryResult = await _redirectService.GetAllSlugs();

                var response = _mapper.Map<List<GetAvailableSlugsResponse>>(queryResult.Data);

                return ApiResponse<List<GetAvailableSlugsResponse>>.SuccessResponse(
                    response,
                    $"{entityName} retrieved successfully.");

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
