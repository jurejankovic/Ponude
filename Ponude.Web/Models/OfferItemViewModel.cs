using System.ComponentModel.DataAnnotations;

namespace Ponude.Web.Models
{
    public class OfferItemViewModel
    {
        public int ItemId { get; set; }

        public string ItemDescription { get; set; }

        public decimal UnitPrice { get; set; }

        [Range (1, int.MaxValue, ErrorMessage = "Količina mora biti veća od 0!")]
        public int Quantity { get; set; }

        public decimal TotalPrice => UnitPrice * Quantity;
    }
}
