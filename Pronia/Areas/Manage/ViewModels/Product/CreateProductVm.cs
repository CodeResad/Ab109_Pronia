namespace Pronia.Areas.Manage.ViewModels.Product;
using System.ComponentModel.DataAnnotations;

public class CreateProductVm
{
    public string Name { get; set; }
    [Required, MaxLength(50, ErrorMessage = "Maksimum uzunluq 50 ola biler")]
    public string Description { get; set; }
    [Required]
    public double Price { get; set; }
    public int? CategoryId { get; set; }
    public List<int>? TagIds { get; set; }
}