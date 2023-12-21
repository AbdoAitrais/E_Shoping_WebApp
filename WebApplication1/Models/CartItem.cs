namespace WebApplication1.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
public class CartItem
{
    // product
    public Product? Product { get; set; }
    public int Quantity { get; set; }
}