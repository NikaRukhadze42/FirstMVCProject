namespace ProductsManagementSystem.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }
    }
}
