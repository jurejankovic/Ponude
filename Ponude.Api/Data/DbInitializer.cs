using Ponude.Api.Models;

namespace Ponude.Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PonudeDbContext context)
        {
            // Ensure the database is created
            context.Database.EnsureCreated();

            // Check if there are already items in the database
            if (context.Products.Any() && context.Offers.Any() && context.OfferItems.Any())
            {
                return; // Database has already been seeded
            }

            // mock products
            var products = new[]
            {
            new Product { Name = "Item 1", Description = "Description for Item 1", Price = 100.00m },
            new Product { Name = "Item 2", Description = "Description for Item 2", Price = 50.00m },
            new Product { Name = "Item 3", Description = "Description for Item 3", Price = 30.00m }
        };
            context.Products.AddRange(products);
            context.SaveChanges(); // Save to generate Ids for items

            // Seed Offers (Ponude)
            var offers = new[]
            {
            new Offer { Name = "Offer 1", Description = "Description for Offer 1", Date = DateTime.UtcNow.AddDays(-1) },
            new Offer { Name = "Offer 2", Description = "Description for Offer 2", Date = DateTime.UtcNow }
        };
            context.Offers.AddRange(offers);
            context.SaveChanges(); // Save to generate Ids for offers

            // Seed OfferItems (Stavke) with correct OfferId references
            var offerItems = new[]
            {
            new OfferItem { Article = "Item 1", Quantity = 2, Price = 100.00m, OfferId = offers[0].Id },
            new OfferItem { Article = "Item 2", Quantity = 1, Price = 50.00m, OfferId = offers[1].Id },
            new OfferItem { Article = "Item 3", Quantity = 5, Price = 30.00m, OfferId = offers[1].Id }
        };
            context.OfferItems.AddRange(offerItems);

            // Save changes to the database
            context.SaveChanges();
        }
    }

}
