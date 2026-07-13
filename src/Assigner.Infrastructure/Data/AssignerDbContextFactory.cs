using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assigner.Infrastructure.Data
{
    public class AssignerDbContextFactory : IDesignTimeDbContextFactory<AssignerDbContext>
    {
        public AssignerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AssignerDbContext>();
            optionsBuilder.UseSqlite("Data Source=assigner.db");
            return new AssignerDbContext(optionsBuilder.Options);
        }
    }
}
