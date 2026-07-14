using Assigner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assigner.Infrastructure.Data
{
    public class AssignerDbContext : DbContext
    {
        public DbSet<Slugs> Slugs { get; set; }
        public DbSet<UrlMapping> UrlMappings { get; set; }

        public AssignerDbContext(DbContextOptions<AssignerDbContext> options)
             : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AssignerDbContext).Assembly);


            #region Slugs
            modelBuilder.Entity<Slugs>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Value)
                      .HasMaxLength(10)
                      .IsRequired();

                entity.HasIndex(x => x.Value)
                      .IsUnique();

                entity.Property(x => x.Status)
                      .IsRequired();
            });

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
   }
