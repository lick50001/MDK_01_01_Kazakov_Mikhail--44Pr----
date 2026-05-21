using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Postavki.Classes.Common;
using Postavki.Models;

namespace Postavki.Context
{
    public class WarehouseContext : DbContext
    {
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseMySql(
            "server=localhost;user=root;password=;database=WarehouseDB",
            ServerVersion.AutoDetect("server=localhost;user=root;password=;database=WarehouseDB")
        );
    }
}
