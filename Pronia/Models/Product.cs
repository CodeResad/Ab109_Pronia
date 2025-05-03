using System.ComponentModel.DataAnnotations;
using Pronia.Models.Base;

namespace Pronia.Models;

public class Product:BaseEntity
{
    public string Name { get; set; }
    [Required, MaxLength(50, ErrorMessage = "Maksimum uzunluq 50 ola biler")]
    public string Description { get; set; }
    [Required]
    public double Price { get; set; }
    public List<ProductImage>? Images { get; set; }
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
}