using rasa_nlu_storage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rasa_nlu_db_storage.Repository
{
    public interface IExampleRepository
    {
        Task<IList<CommonExample>> GetAllExamplesAsync();
        Task<CommonExample> CreateAsync(CommonExample newExample);
        Task<CommonExample> GetExampleByIdAsync(int? id);
        CommonExample UpdateAsync(CommonExample updatedExample);
        Task<CommonExample> DeleteAsync(int id);
        Task<int> SaveChangesAsync();
        bool CommonExampleExists(int id);
    }
}
