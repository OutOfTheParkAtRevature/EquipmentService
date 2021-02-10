using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class Repo
    {
        private readonly EquipmentContext _equipmentContext;
        private readonly ILogger _logger;
        public DbSet<EquipmentRequest> EquipmentRequests;
        public DbSet<EquipmentItem> EquipmentItems;

        public Repo(EquipmentContext equipmentContext, ILogger<Repo> logger)
        {
            _equipmentContext = equipmentContext;
            _logger = logger;
            EquipmentRequests = equipmentContext.EquipmentRequests;
            EquipmentItems = equipmentContext.EquipmentItems;
        }

        public async Task CommitSave()
        {
            await _equipmentContext.SaveChangesAsync();
        }
        public async Task<EquipmentRequest> GetEquipmentRequestById(int id)
        {
            return await EquipmentRequests.FindAsync(id);
        }
        public async Task<IEnumerable<EquipmentRequest>> GetEquipmentRequests()
        {
            return await EquipmentRequests.ToListAsync();
        }
        public async Task<EquipmentItem> GetEquipmentItemById(int id)
        {
            return await EquipmentItems.FindAsync(id);
        }
        public async Task<IEnumerable<EquipmentItem>> GetEquipmentItems()
        {
            return await EquipmentItems.ToListAsync();
        }
    }
}
