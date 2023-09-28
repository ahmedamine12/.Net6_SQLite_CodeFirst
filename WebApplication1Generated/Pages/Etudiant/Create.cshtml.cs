using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1Generated.Pages.Etudiant
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
        public global::Etudiant Etudiant { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Etudiant == null || Etudiant == null)
            {
                return Page();
            }

            _context.Etudiant.Add(Etudiant);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
