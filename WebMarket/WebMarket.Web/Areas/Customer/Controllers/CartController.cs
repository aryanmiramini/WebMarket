using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebMarket.DataAccess.Services;
using WebMarket.DataAccess.Services.Interface;
using WebMarket.Models;
using WebMarket.Models.ViewModels;
using WebMarket.Utility;
using ShoppingCart = WebMarket.Models.ShoppingCart;

namespace WebMarket.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IShoppingCartService _shoppingCart;
        private readonly SignInManager<IdentityUser> _signInManager;

        public CartController(IShoppingCartService shoppingCart,
            SignInManager<IdentityUser> signInManager)
        {
            _shoppingCart = shoppingCart;
            _signInManager = signInManager;
        }

        public int OrderTotal { get; set; }

        [HttpGet]
        public IActionResult Index()
        {

            var user = _signInManager.UserManager.FindByNameAsync(User.Identity.Name).Result;

            ShoppingCart shoppingCart = new ShoppingCart()
            {
                ListCart = _shoppingCart.GetAll(user.Id)
            };
            foreach (var cart in shoppingCart.ListCart)
            {
                cart.Price = GetPriceBasedOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50,
                    cart.Product.Price100);
                shoppingCart.CartTotal += (cart.Price * cart.Count);
            }

            return View(shoppingCart);
        }


        private double GetPriceBasedOnQuantity(double quantity, double price, double price50, double price100)
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


        //public  IActionResult Increament(int cartId)
        //{

        //    //ShoppingCartVM shoppingCartVM = new ShoppingCartVM();

        //    //int cartCount = shoppingCartVM.Count;
        //    //cartCount =  _shoppingCart.IncrementCountVM(shoppingCartVM, cartCount);

        //    //return RedirectToAction(nameof(Index));

        //}

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var obj = _shoppingCart.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return RedirectToAction(nameof(Index));
            }

            _shoppingCart.Remove(obj);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Decrease(ShoppingCart shoppingCart,int count)
        {
            ShoppingCart cartFromDb = _shoppingCart.GetFirstOrDefault(u => u.ProductId == shoppingCart.ProductId);
            shoppingCart.Count = count;
            if (cartFromDb != null)
            {
                 _shoppingCart.DecrementCount( cartFromDb,shoppingCart.Count);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

