using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1Generated.Pages.Etudiant
{
    public class EditModel : PageModel
    {
        private readonly AppContextDB _context;

        public EditModel(AppContextDB context)
        {
            _context = context;
        }

        [BindProperty]
        public global::Etudiant Etudiant { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Etudiant == null)
            {
                return NotFound();
            }

            var etudiant =  await _context.Etudiant.FirstOrDefaultAsync(m => m.Id == id);
            if (etudiant == null)
            {
                return NotFound();
            }
            Etudiant = etudiant;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Etudiant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EtudiantExists(Etudiant.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EtudiantExists(int id)
        {
          return (_context.Etudiant?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
