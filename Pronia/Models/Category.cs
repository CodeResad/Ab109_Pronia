using Pronia.Models.Base;

namespace Pronia.Models;

public class Category:BaseEntity
{
    public string Name { get; set; }
    public List<Product>? Products { get; set; }
}