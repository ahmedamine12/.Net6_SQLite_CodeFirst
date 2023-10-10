namespace WebApplication1Generated.Model
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        // Navigation property pour les produits associés à cette catégorie
        public ICollection<Produit>? Produits { get; set; }
    }
}