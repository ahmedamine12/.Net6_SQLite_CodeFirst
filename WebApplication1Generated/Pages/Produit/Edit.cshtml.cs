using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1Generated.Model;

namespace WebApplication1Generated.Pages.Produit
{
    public class UpdateModel : PageModel
    {
        private readonly AppContextDB _context;
        private readonly IWebHostEnvironment _environment;

        public UpdateModel(AppContextDB context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Model.Produit Produit { get; set; }

        [BindProperty]
        public IFormFile UploadedImage { get; set; }
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        public void OnGet(int id)
        {
            Produit = _context.Produits.FirstOrDefault(p => p.Id == id);
            Categories = _context.Categorie.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nom
            }).ToList();
        }


       

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Produit.ImageUrl");
            Console.WriteLine("OnPostAsync started");
            
            if (!ModelState.IsValid)
            {
                foreach (var modelError in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(modelError.ErrorMessage);
                }
                return Page();
            }

            var existingProduit = _context.Produits.FirstOrDefault(p => p.Id == Produit.Id);
            
            if (existingProduit == null)
            {
                return NotFound("Product not found");
            }

            existingProduit.Nom = Produit.Nom;
            existingProduit.Description = Produit.Description;
            existingProduit.CategorieId = Produit.CategorieId;

            if (UploadedImage != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(UploadedImage.FileName);
                var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await UploadedImage.CopyToAsync(stream);
                }

                existingProduit.ImageUrl = $"/images/{fileName}";
            }
            else
            {
                Produit.ImageUrl = string.Empty; 
            }

            _context.Produits.Update(existingProduit);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
