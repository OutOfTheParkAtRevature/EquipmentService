using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class EquipmentContext : DbContext
    {
        public DbSet<EquipmentItem> EquipmentItems { get; set; }
        public DbSet<EquipmentRequest> EquipmentRequests;

        public EquipmentContext() { }
        public EquipmentContext(DbContextOptions<EquipmentContext> options) : base(options) { }
    }
}
