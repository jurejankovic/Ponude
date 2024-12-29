using System.ComponentModel.DataAnnotations;

namespace Ponude.Api.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv je obavezan.")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public Product() { } 

    }
}