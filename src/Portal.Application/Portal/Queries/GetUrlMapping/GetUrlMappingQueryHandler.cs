using AutoMapper;
using MediatR;
using Portal.Application.Common;
using Portal.Application.Interfaces;
using Portal.Application.Portal.Queries.GetAvailableSlug;
using Portal.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Portal.Queries.GetUrlMapping
{
    public class GetUrlMappingQueryHandler : IRequestHandler<GetUrlMappingQuery, ApiResponse<List<GetUrlMappingResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly IUrlService _urlService;

        public GetUrlMappingQueryHandler(IMapper mapper, IUrlService urlService)
        {
            _mapper = mapper;
            _urlService = urlService;
            
        }
        public async Task<ApiResponse<List<GetUrlMappingResponse>>> Handle(GetUrlMappingQuery request, CancellationToken cancellationToken)
        {
            var entityName = typeof(GetUrlMappingResponse).Name
                .Replace("Query", string.Empty);
            try
            {
                var queryResult = await _urlService.GetAllUrlMappingAsync();

                var response = _mapper.Map<List<GetUrlMappingResponse>>(queryResult.Data);

                return ApiResponse<List<GetUrlMappingResponse>>.SuccessResponse(
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
