using Pronia.Models.Base;

namespace Pronia.Models;

public class Slider:BaseEntity
{
    public string Description { get; set; }
    public string Title { get; set; }
    public string Offer { get; set; }
    public string SliderImgUrl { get; set; }
}