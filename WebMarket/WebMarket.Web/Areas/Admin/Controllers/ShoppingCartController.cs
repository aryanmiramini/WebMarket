using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebMarket.DataAccess.Services.Interface;
using WebMarket.Models.ViewModels;

namespace WebMarket.Web.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ادمین")]

    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCart;
        private readonly SignInManager<IdentityUser> _signInManager;


        public ShoppingCartController(IShoppingCartService shoppingCart, SignInManager<IdentityUser> signInManager)
        {
            _shoppingCart = shoppingCart;
            _signInManager = signInManager;

        }

        private static double GetPriceBasedOnQuantity(double quantity, double price, double price50, double price100)
        {
            if (quantity <= 50)
            {
                return price;
            }
            else
            {
                if (quantity <= 100)
                {
                    return price50;
                }
                else
                {
                    return price100;
                }
            }

        }

        //Get
        public IActionResult Index()
        {

            var user = _signInManager.UserManager.FindByNameAsync(User.Identity!.Name).Result;

            ShoppingCart shoppingCart = new ShoppingCart()
            {
                ListCart = _shoppingCart.GetAll(user.Id)
            };
            foreach (var cart in shoppingCart.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
                shoppingCart.CartTotal += (cart.Price * cart.Count);
            }
            return View();
        }
        //post
        [HttpPost]
        public async Task <ActionResult> OnPostAsync()
        {

            var user = _signInManager.UserManager.FindByNameAsync(User.Identity!.Name).Result;

            ShoppingCart shoppingCart = new ShoppingCart()
            {
                ListCart = _shoppingCart.GetAll(user.Id)
            };
            foreach (var cart in shoppingCart.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
                shoppingCart.CartTotal += (cart.Price * cart.Count);
            }
            return View();
        }
        [HttpDelete]
        public async Task <IActionResult> Delete(int? id)
        {
            var obj = _shoppingCart.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return  Json(new { success = false, message = "خطا در حذف " });
            }
           
                  _shoppingCart.Remove(obj);
             return Json(new { success = true, message = "حذف موفقیت آمیز بود" });
        }





    }
}