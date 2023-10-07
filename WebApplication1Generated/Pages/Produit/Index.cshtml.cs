using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1Generated.Model;

namespace WebApplication1Generated.Pages.Produit
{
    public class IndexModel : PageModel
    {
        private readonly AppContextDB _context;

        public IndexModel(AppContextDB context)
        {
            _context = context;
        }

        public IList<Model.Produit> Produit { get;set; } = default!;
        [BindProperty(SupportsGet = true)] 
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<Model.Produit> productQuery = _context.Produits;

            if (!string.IsNullOrEmpty(SearchString))
            {
                productQuery = productQuery.Where(p => p.Nom.Contains(SearchString));
            }w

            Produit = await productQuery.ToListAsync();
        }

        }
    }
