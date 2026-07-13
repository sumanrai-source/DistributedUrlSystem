using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Infrastructure.Data
{
    public class PortalDbContextFactory : IDesignTimeDbContextFactory<PortalDbContext>
    {
        public PortalDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PortalDbContext>();
            optionsBuilder.UseSqlite("Data Source=portal.db");
            return new PortalDbContext(optionsBuilder.Options);
        }
    }
}
