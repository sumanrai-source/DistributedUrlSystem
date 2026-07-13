using Microsoft.EntityFrameworkCore;
using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Infrastructure.Data
{
    public class PortalDbContext : DbContext
    {

        public DbSet<UrlMapping> UrlMappings { get; set; }

        public PortalDbContext(DbContextOptions<PortalDbContext> options)
               : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region UrlMapping


            modelBuilder.Entity<UrlMapping>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Slug)
                      .HasMaxLength(10)
                      .IsRequired();

                entity.Property(x => x.DestinationUrl)
                      .IsRequired();
            });

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
