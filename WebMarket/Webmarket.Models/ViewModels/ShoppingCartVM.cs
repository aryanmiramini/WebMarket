namespace WebMarket.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public double CartTotal { get; set; }
        public IEnumerable<ShoppingCart> ListCart { get; set; }
        
        //public int Count { get; set; }
    }
}
