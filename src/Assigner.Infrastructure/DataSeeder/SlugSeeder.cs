using Assigner.Domain.Entities;
using Assigner.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static Assigner.Domain.Enums.HelperEnum;

namespace Assigner.Infrastructure.DataSeeder
{
    public static class SlugSeeder
    {
        public static async Task SeedAsync(AssignerDbContext context)
        {
            if (await context.Slugs.AnyAsync())
                return;

            var slugs = Enumerable.Range(1, 1000)
                .Select(_ => new Slugs(
                    id: Guid.NewGuid().ToString(),
                    value: Guid.NewGuid().ToString("N")[..6],
                    status: SlugStatus.Available,
                    createdAt: DateTime.UtcNow,
                    assignedAt: null
                ));

            await context.Slugs.AddRangeAsync(slugs);
            await context.SaveChangesAsync();
        }
    }
}
