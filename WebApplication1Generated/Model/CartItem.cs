namespace WebApplication1Generated.Model;

public class CartItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Produit Product { get; set; }
    public int CartId { get; set; }
    public Cart Cart { get; set; }

    public int Quantity { get; set; }
    public decimal Price { get; set; }
}