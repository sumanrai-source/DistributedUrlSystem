using AutoMapper;
using MediatR;
using Portal.Application.Common;
using Portal.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Portal.Queries.GetAvailableSlug
{
    public class GetAvailableSlugsQueryHandler : IRequestHandler<GetAvailableSlugsQuery, ApiResponse<List<GetAvailableSlugsResponse>>>
    {
        private readonly IMapper _mapper;
        private readonly ISlugInventoryServices _slugInventoryServices;

        public GetAvailableSlugsQueryHandler(ISlugInventoryServices slugInventoryServices, IMapper mapper)
        {
            _slugInventoryServices = slugInventoryServices;
            _mapper = mapper;
            
        }
        public async Task<ApiResponse<List<GetAvailableSlugsResponse>>> Handle(GetAvailableSlugsQuery request, CancellationToken cancellationToken)
        {
            var entityName = typeof(GetAvailableSlugsQuery).Name
                .Replace("Query", string.Empty);
            try
            {
                var queryResult = await _slugInventoryServices.GetAllSlugAsync();

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
