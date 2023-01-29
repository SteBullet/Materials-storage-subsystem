using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Materials_storage_subsystem.Models;
using Materials_storage_subsystem.Models.Roles;
using Microsoft.EntityFrameworkCore;

namespace Materials_storage_subsystem.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Admin> Admins { get; set; } = null!;
        public DbSet<Manager> Managers { get; set; } = null!;
        public DbSet<WarehouseManager> WarehouseManagers { get; set; } = null!;
        public DbSet<Checkman> Checkmen { get; set; } = null!;

        public DbSet<Warehouse> Warehouses { get; set; } = null!;
        public DbSet<ExpenseSheet> ExpenseSheets { get; set; } = null!;
        public DbSet<MaterialMovement> MaterialMovements { get; set; } = null!;
        public DbSet<MaterialRemaining> MaterialRemainings { get; set; } = null!;
        public DbSet<Material> Materials { get; set; } = null!;

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

    }
}
