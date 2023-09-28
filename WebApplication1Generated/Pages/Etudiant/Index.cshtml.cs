using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1Generated.Pages.Etudiant
{
    public class IndexModel : PageModel
    {
        private readonly AppContextDB _context;

        public IndexModel(AppContextDB context)
        {
            _context = context;
        }

        public IList<global::Etudiant> Etudiant { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Etudiant != null)
            {
                Etudiant = await _context.Etudiant.ToListAsync();
            }
        }
    }
}
