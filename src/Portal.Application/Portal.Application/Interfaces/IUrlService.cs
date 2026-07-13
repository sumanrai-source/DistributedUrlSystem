

using Portal.Application.Common;
using Portal.Application.Portal.Command.CreateShortUrl;

namespace Portal.Application.Interfaces
{
    public interface IUrlService
    {
        Task<ApiResponse<CreateShortUrlResponse>> CreateShortUrlAsync(CreateShortUrlCommand request, CancellationToken cancellationToken = default);
    }
}
