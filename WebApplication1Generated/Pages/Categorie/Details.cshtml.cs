using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1Generated.Model;

namespace WebApplication1Generated.Pages.Categorie
{
    public class DetailsModel : PageModel
    {
        private readonly AppContextDB _context;

        public DetailsModel(AppContextDB context)
        {
            _context = context;
        }

      public Model.Categorie Categorie { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Categorie == null)
            {
                return NotFound();
            }

            var categorie = await _context.Categorie.FirstOrDefaultAsync(m => m.Id == id);
            if (categorie == null)
            {
                return NotFound();
            }
            else 
            {
                Categorie = categorie;
            }
            return Page();
        }
    }
}
