using rasa_nlu_storage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rasa_nlu_db_storage.Repository
{
    public interface IEntityRepository
    {
        Task<IList<Entity>> GetEntitiesAsync();
        Task<Entity> CreateAsync(Entity newExample, CommonExample example);
        Task<Entity> GetEntityByIdAsync(int? id);
        Entity Update(Entity updatedExample);
        Task<Entity> DeleteAsync(int id);
        Task<int> SaveChangesAsync();
        bool EntityExists(int id);
    }
}
