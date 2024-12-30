namespace Ponude.Api.Models
{
    public class OfferItem
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public int OfferId { get; set; }
        public OfferItem() { }

    }
}