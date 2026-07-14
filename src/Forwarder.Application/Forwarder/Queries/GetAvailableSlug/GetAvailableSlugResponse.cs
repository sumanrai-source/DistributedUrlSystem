using System;
using System.Collections.Generic;
using System.Text;
using static Assigner.Domain.Enums.HelperEnum;

namespace Forwarder.Application.Forwarder.Queries.GetAvailableSlug
{
    public record GetAvailableSlugsResponse
    (
        string slug,
        SlugStatus status
        );
}
