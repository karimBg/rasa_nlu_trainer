using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rasa_nlu_db_storage.Data;
using rasa_nlu_storage.Entities;

namespace rasa_nlu_db_storage.Repository
{
    public class EntityRepository : IEntityRepository
    {
        private readonly AppDbContext _context;

        public EntityRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<Entity> CreateAsync(Entity newExample, CommonExample entityId)
        {
            var commonExample = await _context.CommonExamples.FindAsync(entityId);
            newExample.CommonExample = commonExample;
            await _context.Entities.AddAsync(newExample);
            return newExample;
        }

        public async Task<Entity> DeleteAsync(int id)
        {
            var entity = await GetEntityByIdAsync(id);
            if (entity != null)
            {
                _context.Entities.Remove(entity);
            }
            return entity;
        }

        public Entity Update(Entity updatedEntity)
        {
            var entity = _context.Entities.Attach(updatedEntity);
            entity.State = EntityState.Modified;
            return updatedEntity;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IList<Entity>> GetEntitiesAsync()
        {
            var entities = await _context.Entities.ToListAsync();
            return entities;
        }

        public async Task<Entity> GetEntityByIdAsync(int? id)
        {
            return await _context.Entities.FindAsync(id);
        }

        // returns true if an entity with the specified id exists.
        public bool EntityExists(int id)
        {
            return _context.Entities.Any(e => e.Id == id);
        }
    }
}
