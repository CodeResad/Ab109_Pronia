using System.ComponentModel.DataAnnotations;
using Pronia.Models.Base;

namespace Pronia.Models;

public class Category:BaseEntity
{
    [Required,MaxLength(50,ErrorMessage = "Name is too long, maximum length is 50")]
    public string Name { get; set; }
    public List<Product>? Products { get; set; }
}