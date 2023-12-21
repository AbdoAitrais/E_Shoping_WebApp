namespace WebApplication1.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PurchasedProduct
{
    public int Id { get; set; }
    [Required] public Product? Product { get; set; } // Navigation property for the product
    public int Quantity { get; set; }
    // Other properties related to the purchased product
}