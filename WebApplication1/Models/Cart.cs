using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Cart
{
    public Collection<CartItem> CartItems { get; set; } = new Collection<CartItem>();
}