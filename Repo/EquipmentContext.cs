using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Repository
{
    public class EquipmentContext : DbContext
    {
        public DbSet<EquipmentRequest> equipmentRequests;
        public DbSet<EquipmentItem> equipmentItems;

        public EquipmentContext() { }
        public EquipmentContext(DbContextOptions<EquipmentContext> options) : base(options) { }

    }
}
