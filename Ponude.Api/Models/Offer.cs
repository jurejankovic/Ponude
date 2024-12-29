using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ponude.Api.Models
{
    public class Offer
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Mora postojati makar jedna stavka")]
        public virtual ICollection<OfferItem> Items { get; set; }

        public Offer() 
        {
            Name = "Nova ponuda " + Id;
            Date = DateTime.Now;

            // stavke ponude
            Items = new HashSet<OfferItem>();

        }

    }
}