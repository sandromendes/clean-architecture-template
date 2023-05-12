using Domain.Common;
using Domain.Common.Interface;
using Domain.Entities.Categories;
using Domain.Entities.Purchases;
using Domain.Interfaces.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Products
{
    public class Product : IdentityEntity, IIdentityEntity, IProduct
    {
        [Required]
        public string? Code { get; set; }
        [Required]
        public string? Name { get; set; }    
        public string? Description { get; set; }
        public decimal Price { get; set; } = decimal.Zero;
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }  
        public bool IsActive { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        
        [NotMapped]
        public ICollection<Purchase> Purchases { get; set;}
    }
}
