using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using rasa_nlu_db_storage.Data;
using rasa_nlu_db_storage.Repository;
using rasa_nlu_storage.Entities;

namespace rasa_nlu_db_storage.Pages.Rasa
{
    public class CreateModel : PageModel
    {
        private readonly IExampleRepository _repository;

        public CreateModel(IExampleRepository repository)
        {
            this._repository = repository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CommonExample CommonExample { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateAsync(CommonExample);
            await _repository.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}