using Forwarder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forwarder.Infrastructure.Data
{
    public class ForwarderDbContext : DbContext
    {
        public DbSet<UrlMapping> UrlMappings { get; set; }

        public ForwarderDbContext(DbContextOptions<ForwarderDbContext> options)
             : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ForwarderDbContext).Assembly);


            #region UrlMappings


            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
