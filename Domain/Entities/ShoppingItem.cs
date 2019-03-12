namespace Buisiness
{
    public class ShoppingItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public ShoppingBag Bag { get; set; }

        public Product Product { get; set; }

    }
}