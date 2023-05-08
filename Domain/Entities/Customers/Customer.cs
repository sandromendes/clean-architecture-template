using Domain.Entities.Purchases;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Customers
{
    public class Customer
    {
        private List<Purchase> purchases = new();

        public Guid Id { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        public IReadOnlyCollection<Purchase> Purchases { get => purchases.AsReadOnly(); }

        public Purchase Checkout(Cart cart)
        {
            var purchase = new Purchase()
            {
                Customer = this,
                Products = cart.Products,
                Created = DateTime.Now
            };

            if(purchases == null || !purchases.Any())
                purchases = new List<Purchase>() { };

            purchases.Add(purchase);
            return purchase;
        }
    }
}
