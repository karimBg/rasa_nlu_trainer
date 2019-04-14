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
    public class ExampleRepository : IExampleRepository
    {
        private readonly AppDbContext _context;

        public ExampleRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<CommonExample> CreateAsync(CommonExample newExample)
        {
            var rasaNluDataId = _context.RasaNLUDatas.FirstOrDefault();
            newExample.RasaNLUData = rasaNluDataId;
            await _context.AddAsync(newExample);
            return newExample;
        }

        public async Task<CommonExample> DeleteAsync(int id)
        {
            var example = await GetExampleByIdAsync(id);
            if (example != null)
            {
                _context.CommonExamples.Remove(example);
            }
            return example;
        }

        public CommonExample UpdateAsync(CommonExample updatedExample)
        {
            var entity = _context.CommonExamples.Attach(updatedExample);
            entity.State = EntityState.Modified;
            return updatedExample;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IList<CommonExample>> GetAllExamplesAsync()
        {
            var examples = await _context.CommonExamples.ToListAsync();
            return examples;
        }

        public async Task<CommonExample> GetExampleByIdAsync(int? id)
        {
            return await _context.CommonExamples.FindAsync(id);
        }

        // returns true if an example with the specified id exists.
        public bool CommonExampleExists(int id)
        {
            return _context.CommonExamples.Any(e => e.Id == id);
        }
    }
}
