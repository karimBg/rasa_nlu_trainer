using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using rasa_nlu_storage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rasa_nlu_db_storage.Data
{
    public class RasaNluSeeder
    {
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _hosting;

        public RasaNluSeeder(AppDbContext context, IHostingEnvironment hosting)
        {
            this._context = context;
            this._hosting = hosting;
        }

        //public void Seed()
        //{
        //    _context.Database.EnsureCreated();
        //    if(!_context.NluModel.Any())
        //    {
        //        // Create Sample Data.
        //        var filePath = Path.Combine(_hosting.ContentRootPath, "Data/test.json");
        //        var json = File.ReadAllText(filePath);
        //        var nluModel = JsonConvert.DeserializeObject<NluModel>(json);
        //        _context.NluModel.Add(nluModel);

        //        _context.SaveChanges();
        //    } 
        //}
    }
}
