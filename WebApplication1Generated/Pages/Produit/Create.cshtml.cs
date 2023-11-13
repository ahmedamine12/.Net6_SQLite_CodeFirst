using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1Generated.Controllers; // Import your controller namespace
using WebApplication1Generated.Model;

namespace WebApplication1Generated.Pages.Produit
{
    public class CreateModel : PageModel
    {
        private readonly ProduitController _produitController;

        public CreateModel(ProduitController produitController)
        {
            _produitController = produitController;
        }

        // Liste des catégories pour la liste déroulante
        public SelectList Categories { get; set; }

        [BindProperty]
        public Model.Produit Produit { get; set; } = new Model.Produit();

        [BindProperty]
        public IFormFile UploadedImage { get; set; }

        public IActionResult OnGet()
        {
            Categories = _produitController.GetCategories();
            return Page();
        }

        public IActionResult OnPost()
        {
            ModelState.Remove("Produit.ImageUrl");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (UploadedImage != null)
            {
                _produitController.ProcessImage(UploadedImage, Produit);
            }

            _produitController.AddProduct(Produit);

            return RedirectToPage("./Index");
        }
    }
}