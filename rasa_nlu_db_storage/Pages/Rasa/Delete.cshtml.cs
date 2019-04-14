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
    public class DeleteModel : PageModel
    {
        private readonly IExampleRepository _repository;

        public DeleteModel(IExampleRepository repository)
        {
            this._repository = repository;
        }

        [BindProperty]
        public CommonExample CommonExample { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CommonExample = await _repository.GetExampleByIdAsync(id);

            if (CommonExample == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CommonExample = await _repository.GetExampleByIdAsync(id);

            if (CommonExample != null)
            {
                await _repository.DeleteAsync(CommonExample.Id);
                await _repository.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
