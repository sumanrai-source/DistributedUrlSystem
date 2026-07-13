using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Contracts.Events
{
    public record UrlCreatedAck
    (string Slug,
    bool Success);
}
