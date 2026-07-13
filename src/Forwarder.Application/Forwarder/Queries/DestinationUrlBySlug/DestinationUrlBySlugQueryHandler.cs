using Akka.Util;
using AutoMapper;
using Forwarder.Application.Common;
using Forwarder.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forwarder.Application.Forwarder.Queries.DestinationUrlBySlug
{
    public class DestinationUrlBySlugQueryHandler : IRequestHandler<DestinationUrlBySlugQuery, ApiResponse<DestinationUrlBySlugResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IRedirectService _redirectService;

        public DestinationUrlBySlugQueryHandler(IMapper mapper, IRedirectService redirectService)
        {
            _mapper = mapper;
            _redirectService = redirectService;

        }
        public async Task<ApiResponse<DestinationUrlBySlugResponse>> Handle(DestinationUrlBySlugQuery query, CancellationToken cancellationToken)
        {
            var entityName = typeof(DestinationUrlBySlugQuery).Name
                .Replace("Query", string.Empty);
            try
            {
                var queryResult = await _redirectService.GetDestinationUrlAsync(query.request);
                return ApiResponse<DestinationUrlBySlugResponse>.SuccessResponse(queryResult.Data, $"{entityName} retrieved successfully.");

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
