using Ponude.Api.Models;

namespace Ponude.Web.Models
{
    public class OfferViewModel
    {
        public int OfferId { get; set; }

        public DateTime OfferDate { get; set; }

        public List<OfferItemViewModel> Items { get; set; } = new List<OfferItemViewModel>();

        public decimal TotalAmount => Items.Sum(item => item.TotalPrice);

    }
}
