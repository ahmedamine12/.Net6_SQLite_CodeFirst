namespace WebApplication1Generated.Model;

public class Cart
{
    public int Id { get; set; }
    public int EtudiantId { get; set; }
    public Etudiant Etudiant { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();
}