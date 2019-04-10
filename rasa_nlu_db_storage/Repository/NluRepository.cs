using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rasa_nlu_db_storage.Data;
using rasa_nlu_storage.Models;

namespace rasa_nlu_db_storage.Repository
{
    public class NluRepository : INluRepository
    {
        private readonly AppDbContext _context;

        public NluRepository(AppDbContext context)
        {
            this._context = context;
        }

        ///////////////// CRUD Operations

        // General 
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }

        // NluModel
        public async Task<NluModel> GetRasaModelAsync(int id)
        {
            var query = _context.NluModel.Where(m => m.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        // RasaNluData
        public async Task<RasaNLUData> GetRasaNluDataAsync(int nluModelId, int rasaNluId)
        {
            IQueryable<RasaNLUData> query = _context.RasaNLUDatas;

            // Add Query
            query = query
              .Where(t => t.Id == rasaNluId && t.NluModelId == nluModelId);

            return await query.FirstOrDefaultAsync();
        }

        // CommonExamples
        public async Task<CommonExample[]> GetExamplesByRasaNluIdAsync(int modelId, int rasaNluId)
        {
            IQueryable<CommonExample> query = _context.CommonExamples
                .Where(c => c.RasaNLUData.NluModelId == modelId && c.RasaNLUData.Id == rasaNluId)
                .Distinct();

            return await query.ToArrayAsync();
                
        }

        public async Task<CommonExample> GetExampleByRasaNluIdAsync(int modelId, int rasaNluId, int exampleId)
        {
            var query = _context.CommonExamples
                .Where(
                    c => c.Id == exampleId && 
                    c.RasaNLUData.NluModelId == modelId && 
                    c.RasaNLUData.Id == rasaNluId
                );

            return await query.FirstOrDefaultAsync();
        }

        // Entity
        public async Task<Entity[]> GetEntitiesByExampleIdAsync(int modelId, int rasaNluId, int exampleId)
        {
            IQueryable<Entity> query = _context.Entities
                .Where(
                    e => e.CommonExample.RasaNLUData.NluModelId == modelId &&
                         e.CommonExample.RasaNLUData.Id == rasaNluId &&
                         e.CommonExample.Id == exampleId
                ).Distinct();

            return await query.ToArrayAsync();
        }

        public async Task<Entity> GetEntityByExampleIdAsync(int modelId, int rasaNluId, int exampleId, int entityId)
        {
            var query = _context.Entities
                .Where(
                    e => e.CommonExample.RasaNLUData.NluModelId == modelId &&
                         e.CommonExample.RasaNLUData.Id == rasaNluId &&
                         e.CommonExample.Id == exampleId &&
                         e.Id == entityId
                );

            return await query.FirstOrDefaultAsync();
        }
    }
}
