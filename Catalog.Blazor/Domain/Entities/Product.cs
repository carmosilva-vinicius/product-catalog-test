using System.ComponentModel.DataAnnotations;
using Catalog.Domain.Enums;

namespace Catalog.Domain.Entities;

public class Product
{
    
    public Guid Id { get; set; }
    [Required(ErrorMessage = "O nome do produto deve ser informado")]
    public string Name { get; set; } = string.Empty;
    public decimal SalePrice { get; set; }
    [Required(ErrorMessage = "A descrição do produto deve ser informada")]
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public ProductType Type { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
}
