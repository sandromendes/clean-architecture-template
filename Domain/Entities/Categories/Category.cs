using Domain.Common;
using Domain.Entities.Products;

namespace Domain.Entities.Categories
{
    public class Category : IdentityEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
