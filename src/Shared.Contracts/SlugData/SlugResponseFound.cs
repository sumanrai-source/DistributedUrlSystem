using System;
using System.Collections.Generic;
using System.Text;
using static Assigner.Domain.Enums.HelperEnum;

namespace Shared.Contracts.SlugData
{

    public sealed record SlugDto(
       string Slug,
       SlugStatus Status
   );
    public sealed record SlugResponseFound
    (
          List<SlugDto> Slugs
        );
}
