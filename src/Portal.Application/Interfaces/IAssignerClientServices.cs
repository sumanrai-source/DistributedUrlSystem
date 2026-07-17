using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Interfaces
{
    public interface IAssignerClientServices
    {
        Task<string> AssignSlugAsync(CancellationToken cancellationToken = default);
    }
}
