using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using rasa_nlu_db_storage.Data;
using rasa_nlu_db_storage.Repository;
using rasa_nlu_storage.Entities;

namespace rasa_nlu_db_storage.Pages.Rasa
{
    public class IndexModel : PageModel
    {
        private readonly IExampleRepository _repository;

        public IndexModel(IExampleRepository repository)
        {
            this._repository = repository;
        }

        public IList<CommonExample> CommonExample { get;set; }

        public async Task OnGetAsync()
        {
            CommonExample = await _repository.GetAllExamplesAsync();
        }
    }
}
