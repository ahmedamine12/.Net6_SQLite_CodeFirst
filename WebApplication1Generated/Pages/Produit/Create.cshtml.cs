using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using WebApplication1Generated.Model;

namespace WebApplication1Generated.Pages.Produit
{
    public class CreateModel : PageModel
    {
        private readonly AppContextDB _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(AppContextDB context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // Liste des catégories pour la liste déroulante
        public List<Model.Categorie> Categories { get; set; } = new List<Model.Categorie>();

        public IActionResult OnGet()
        {
            // Récupérer toutes les catégories pour la liste déroulante
            Categories = _context.Categorie.ToList();
            return Page();
        }

        [BindProperty]
        public Model.Produit Produit { get; set; } = default!;

        [BindProperty]
        public IFormFile UploadedImage { get; set; }
        
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Produit.ImageUrl");
            Console.WriteLine("OnPostAsync started");

            // Model State Errors
            foreach (var error in ModelState.Values.SelectMany(modelState => modelState.Errors))
            {
                Console.WriteLine($"ModelState Error: {error.ErrorMessage}");
            }

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model is not valid");
                return Page();
            }

            if (UploadedImage != null)
            {
                Console.WriteLine("Processing uploaded image");

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(UploadedImage.FileName);
                var filePath = Path.Combine(_environment.WebRootPath, "images", fileName); 

                Console.WriteLine($"Saving image to: {filePath}");

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await UploadedImage.CopyToAsync(stream);
                }

                Produit.ImageUrl = $"/images/{fileName}";
            }
            else
            {
                Produit.ImageUrl = string.Empty; // Assign a default value or a placeholder image URL if necessary
            }
            Console.WriteLine("Adding product to context");
            _context.Produits.Add(Produit);
    
            try
            {
                Console.WriteLine("Attempting to save changes to database");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while saving to database: {ex.Message}");
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }

            Console.WriteLine("OnPostAsync finished");
            return RedirectToPage("./Index");
        }
    }
}
