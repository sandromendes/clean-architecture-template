namespace Domain.Interfaces.Products
{
    public interface IProduct
    {
        string? Code { get; set; }
        string? Name { get; set; }
        string? Description { get; set; }
        decimal Price { get; set; }
    }
}
