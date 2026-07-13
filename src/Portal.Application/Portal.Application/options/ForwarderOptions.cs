using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.options
{
    public sealed class ForwarderOptions
    {
        public const string SectionName = "Services:Forwarder";

        public string BaseUrl { get; init; } = string.Empty;
    }
}
