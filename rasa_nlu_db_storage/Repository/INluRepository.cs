using rasa_nlu_storage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rasa_nlu_db_storage.Repository
{
    public interface INluRepository
    {
        // General 
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        // NluModel
        Task<NluModel> GetRasaModelAsync(int id);

        // RasaNluData
        Task<RasaNLUData> GetRasaNluDataByNluModelAsync(int id, int nluDataId);

        // CommonExamples
        Task<CommonExample[]> GetExamplesByRasaNluIdAsync(int rasaNluId, int rasaNluDataId);
        Task<CommonExample> GetExampleByRasaNluIdAsync(int rasaNluId, int rasaNluDataId, int exampleId);

        // Entity
        Task<Entity[]> GetEntitiesByExampleIdAsync(int modelId, int rasaNluId, int exampleId);
        Task<Entity> GetEntityByExampleIdAsync(int modelId, int rasaNluId, int exampleId, int entityId);

    }
}
