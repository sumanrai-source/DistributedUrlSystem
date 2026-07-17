using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.options
{
    public sealed class AssignerOptions
    {
        public const string SectionName = "Services:Assigner";

        public string BaseUrl { get; init; } = string.Empty;
    }
}
