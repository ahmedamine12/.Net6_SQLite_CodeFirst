using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1Generated.Model;

namespace WebApplication1Generated.Controllers
{
    public class ProduitController : Controller
    {
        private readonly AppContextDB _context;
        private readonly IWebHostEnvironment _environment;

        public ProduitController(AppContextDB context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public SelectList GetCategories()
        {
            return new SelectList(_context.Categorie, "Id", "Nom");
        }

        public async Task<IActionResult> Index()
        {
            var produits = await _context.Produits.ToListAsync();
            return View(produits);
        }

        public IActionResult Create()
        {
            ViewData["CategorieId"] = GetCategories();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Description,ImageUrl,CategorieId")] Produit produit, IFormFile uploadedImage)
        {
            if (ModelState.IsValid)
            {
                if (uploadedImage != null)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(uploadedImage.FileName);
                    var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await uploadedImage.CopyToAsync(stream);
                    }

                    produit.ImageUrl = $"/images/{fileName}";
                }

                _context.Produits.Add(produit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategorieId"] = GetCategories();
            return View(produit);
        }

        public void ProcessImage(IFormFile uploadedImage, Produit produit)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(uploadedImage.FileName);
            var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                uploadedImage.CopyTo(stream);
            }

            produit.ImageUrl = $"/images/{fileName}";
        }

        public void AddProduct(Produit produit)
        {
            _context.Produits.Add(produit);
            _context.SaveChanges();
        }
    }
}
