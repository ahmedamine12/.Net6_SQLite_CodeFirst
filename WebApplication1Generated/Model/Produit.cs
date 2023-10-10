namespace WebApplication1Generated.Model
{
    public class Produit
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        // Propriétés pour la relation avec Categorie
        public int? CategorieId { get; set; }// Clé étrangère
        public Categorie? Categorie { get; set; } // Propriété de navigation
    }
}