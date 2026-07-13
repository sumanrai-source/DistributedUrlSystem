using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forwarder.Infrastructure.Data
{


    public class ForwarderDbContextFactory : IDesignTimeDbContextFactory<ForwarderDbContext>
    {
        public ForwarderDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ForwarderDbContext>();
            optionsBuilder.UseSqlite("Data Source=forwarder.db");
            return new ForwarderDbContext(optionsBuilder.Options);
        }
    }
}
