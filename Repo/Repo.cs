using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;

namespace Repository
{
    public class Repo
    {
        private readonly EquipmentContext _equipmentContext;
        private readonly ILogger _logger;
        public DbSet<EquipmentRequest> equipmentRequests;
        public DbSet<EquipmentItem> equipmentItems;

        public Repo(EquipmentContext equipmentContext, ILogger<Repo> logger)
        {
            _equipmentContext = equipmentContext;
            _logger = logger;
            this.equipmentItems = _equipmentContext.equipmentItems;
            this.equipmentRequests = _equipmentContext.equipmentRequests;
        }

        public async Task CommitSave()
        {
            await _equipmentContext.SaveChangesAsync();
        }

        public async Task<EquipmentRequest> GetEquipmentRequestById(int id)
        {
            return await equipmentRequests.FindAsync(id);
        }
        public async Task<IEnumerable<EquipmentRequest>> GetEquipmentRequests()
        {
            return await equipmentRequests.ToListAsync();
        }
        public async Task<EquipmentItem> GetEquipmentItemById(int id)
        {
            return await equipmentItems.FindAsync(id);
        }
        public async Task<IEnumerable<EquipmentItem>> GetEquipmentItems()
        {
            return await equipmentItems.ToListAsync();
        }
    }
}
