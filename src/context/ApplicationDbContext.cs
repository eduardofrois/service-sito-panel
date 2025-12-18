using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceSitoPanel.Helpers;
using ServiceSitoPanel.src.enums;
using ServiceSitoPanel.src.interfaces;
using ServiceSitoPanel.src.model;

namespace ServiceSitoPanel.src.context
{
    public class ApplicationDbContext : DbContext
    {
        public int CurrentTenantId { get; set; }
        public int CurrentUserId { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Users> users { get; set; }
        public DbSet<Tenant> tenant { get; set; }
        public DbSet<Profile> profiles { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<Client> client { get; set; }
        public DbSet<Expenses> expenses { get; set; }
        public DbSet<Solicitations> solicitations { get; set; }
        public DbSet<Supplier> supplier { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<Status>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            modelBuilder.Entity<Orders>(entity =>
                entity.HasQueryFilter(o => o.tenant_id == CurrentTenantId)
            );
            modelBuilder.Entity<Client>(entity =>
                entity.HasQueryFilter(o => o.tenant_id == CurrentTenantId)
            );
            modelBuilder.Entity<Expenses>(entity =>
                entity.HasQueryFilter(o => o.tenant_id == CurrentTenantId)
            );
            modelBuilder.Entity<Supplier>(entity =>
                entity.HasQueryFilter(o => o.tenant_id == CurrentTenantId)
            );

        }
    }
}