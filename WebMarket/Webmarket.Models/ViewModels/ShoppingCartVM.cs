namespace WebMarket.Models.ViewModels
{
    public class ShoppingCart
    {
        public double CartTotal { get; set; }
        public IEnumerable<Models.ShoppingCart> ListCart { get; set; }

        public int Count { get; set; }
    }
}
