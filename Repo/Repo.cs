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
        /// <summary>
        /// saves changes to the database
        /// </summary>
        /// <returns></returns>
        public async Task CommitSave()
        {
            await _equipmentContext.SaveChangesAsync();
        }
        /// <summary>
        /// returns a list of EquipmentRequests. takes in the RequstID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EquipmentRequest> GetEquipmentRequestById(Guid id)
        {
            return await EquipmentRequests.FindAsync(id);
        }
        /// <summary>
        /// returns a list of all EquipmentRequests
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EquipmentRequest>> GetEquipmentRequests()
        {
            return await EquipmentRequests.ToListAsync();
        }
        /// <summary>
        /// returns a list of EquipmentItems that have the id parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<EquipmentItem> GetEquipmentItemById(int id)
        {
            return await EquipmentItems.FindAsync(id);
        }
        /// <summary>
        /// returns a list of all EquipmentItems 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<EquipmentItem>> GetEquipmentItems()
        {
            return await EquipmentItems.ToListAsync();
        }
    }
}
