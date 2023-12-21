using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Purchase
{
    public int Id { get; set; }
    [Required] public User? User { get; set; } // Navigation property for the user
    public DateTime PurchaseDate { get; set; }
    [Required] public ICollection<PurchasedProduct> PurchasedProducts { get; set; }
    // Other properties related to the purchase
}