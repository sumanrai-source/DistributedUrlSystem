

using Portal.Application.Common;
using Portal.Application.Portal.Command.CreateShortUrl;
using Portal.Application.Portal.Queries.GetAvailableSlug;
using Portal.Application.Portal.Queries.GetUrlMapping;

namespace Portal.Application.Interfaces
{
    public interface IUrlService
    {
        Task<ApiResponse<CreateShortUrlResponse>> CreateShortUrlAsync(CreateShortUrlCommand request, CancellationToken cancellationToken = default);

        Task<ApiResponse<List<GetUrlMappingResponse>>> GetAllUrlMappingAsync();
    }
}
