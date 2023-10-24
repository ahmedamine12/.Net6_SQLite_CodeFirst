using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1Generated.Model;

public class AppContextDB : DbContext
    {
        public AppContextDB (DbContextOptions<AppContextDB> options)
            : base(options)
        {
        }

        public DbSet<Etudiant> Etudiant { get; set; } = default!;
        
        public DbSet<Produit> Produits { get; set; }
        
        public DbSet<Categorie> Categorie { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        
        
    }
