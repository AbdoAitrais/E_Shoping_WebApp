
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models;

public class Product
{
    [Key] public int Id { get; set; }
    [Required] public string? Name { get; set; }
    [Required] public string? Description { get; set; }
    [Required] public string? Categorie { get; set; }
    [AllowNull] public string? Image { get; set; }
    [AllowNull] [NotMapped] public IFormFile ImageFile { get; set; }
    [Required] public decimal Price { get; set; }
    [AllowNull] [NotMapped] public DateTime CreatedAt { get; set; } = DateTime.Now;
    
}