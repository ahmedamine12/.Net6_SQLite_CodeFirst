using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1Generated.Model;
using System.Text.Json; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1Generated.Pages.Produit
{
    public class IndexModel : PageModel
    {
        private readonly AppContextDB _context;
        // Constante pour le nom du cookie pour une gestion uniforme.
        private const string CartCookieName = "CartCookie";

        public IndexModel(AppContextDB context)
        {
            _context = context;
        }

        public IList<Model.Produit> Produit { get; set; } = new List<Model.Produit>();

        [BindProperty]
        public string SearchString { get; set; }

        [BindProperty]
        public int? CategoryID { get; set; }

        public SelectList Categories { get; set; }

        public async Task OnGetAsync()
        {
            PopulateProducts();
        }

        public IActionResult OnPost(int productId, int quantity = 1)
        {
            // Récupère le panier depuis les cookies.
            var cartJson = Request.Cookies[CartCookieName];
            // Désérialise le panier ou crée un nouveau s'il est vide.
            List<SessionCartItem> cartItems = string.IsNullOrEmpty(cartJson) ? new List<SessionCartItem>() : JsonSerializer.Deserialize<List<SessionCartItem>>(cartJson);

            // Vérifie si le produit est déjà dans le panier.
            var existingItem = cartItems.FirstOrDefault(x => x.ProductId == productId);
            if (existingItem != null)
            {
                // Si le produit est déjà dans le panier, augmente la quantité.
                existingItem.Quantity += quantity;
            }
            else
            {
                // Sinon, ajoute le produit au panier.
                cartItems.Add(new SessionCartItem { ProductId = productId, Quantity = quantity });
            }

            // Sauvegarde le panier mis à jour dans les cookies.
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddYears(1),
                HttpOnly = true,
                IsEssential = true
            };
            Response.Cookies.Append(CartCookieName, JsonSerializer.Serialize(cartItems), cookieOptions);
            
            return RedirectToPage("./Index");
        }

        // Récupère la liste des produits actuellement dans le panier.
        public List<Model.Produit> GetCartItems()
        {
            var cartJson = Request.Cookies[CartCookieName];
            List<SessionCartItem> cartItemIds = string.IsNullOrEmpty(cartJson) ? new List<SessionCartItem>() : JsonSerializer.Deserialize<List<SessionCartItem>>(cartJson);

            var productIds = cartItemIds.Select(c => c.ProductId).ToList();
            return _context.Produits.Where(p => productIds.Contains(p.Id)).ToList();
        }

        // Récupère un article spécifique du panier par son ID produit.
        public SessionCartItem GetCartItemById(int productId)
        {
            var cartJson = Request.Cookies[CartCookieName];
            List<SessionCartItem> cartItems = string.IsNullOrEmpty(cartJson) ? new List<SessionCartItem>() : JsonSerializer.Deserialize<List<SessionCartItem>>(cartJson);
            return cartItems.FirstOrDefault(c => c.ProductId == productId);
        }

        // Peuple la liste des produits à afficher.
        private void PopulateProducts()
        {
            IQueryable<Model.Produit> productQuery = _context.Produits;

            if (!string.IsNullOrEmpty(SearchString))
            {
                productQuery = productQuery.Where(p => p.Nom.Contains(SearchString));
            }

            if (CategoryID.HasValue)
            {
                productQuery = productQuery.Where(p => p.CategorieId == CategoryID.Value);
            }

            Categories = new SelectList(_context.Categorie.ToList(), "Id", "Nom");
            Produit = productQuery.ToList();
        }

        // Classe pour représenter un élément du panier.
        public class SessionCartItem
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
