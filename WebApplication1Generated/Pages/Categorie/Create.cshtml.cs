using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1Generated.Model;

namespace WebApplication1Generated.Pages.Categorie
{
    public class CreateModel : PageModel
    {
        private readonly AppContextDB _context;

        public CreateModel(AppContextDB context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Model.Categorie Categorie { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("OnPostAsync started");  // log at the start

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model is not valid");  // log if the model isn't valid
                return Page();
            }

            _context.Categorie.Add(Categorie);
    
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}