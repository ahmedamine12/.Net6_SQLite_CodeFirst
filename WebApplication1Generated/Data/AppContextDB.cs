using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

    public class AppContextDB : DbContext
    {
        public AppContextDB (DbContextOptions<AppContextDB> options)
            : base(options)
        {
        }

        public DbSet<Etudiant> Etudiant { get; set; } = default!;
    }
