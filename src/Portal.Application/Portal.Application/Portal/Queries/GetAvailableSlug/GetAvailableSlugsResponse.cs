using System;
using System.Collections.Generic;
using System.Text;
using static Assigner.Domain.Enums.HelperEnum;

namespace Portal.Application.Portal.Queries.GetAvailableSlug
{
    public record GetAvailableSlugsResponse
    (
        string slug,
        SlugStatus status
        );
}
