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
    public class IndexModel : PageModel
    {
        private readonly AppContextDB _context;

        public IndexModel(AppContextDB context)
        {
            _context = context;
        }

        public IList<Model.Categorie> Categorie { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Categorie != null)
            {
                Categorie = await _context.Categorie.ToListAsync();
            }
        }
    }
}
