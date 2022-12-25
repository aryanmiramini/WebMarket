using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webmarket.Models;
using WebMarket.DataAccess.Services.Interface;
using WebMarket.Models;

namespace WebMarket.Web.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService _shoppingCart;

        public ShoppingCartController(IShoppingCartService shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IActionResult Index()
        {
            IEnumerable<ShoppingCart> shoppingCarts = _shoppingCart.GetAll();
            return View(shoppingCarts);
        }

        //Get
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //post
        [HttpPost]
        public IActionResult Create(ShoppingCart obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "مقدار فیلد ترتیب نمایش نباید با مقدار فیلد نام برابر باشد");
            }

            if (ModelState.IsValid)
            {
                _shoppingcart.Add(obj);
                TempData["success"] = "دسته جدید با موفقیت ایجاد شد";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //var categoryFromDb = _db.Categories.Find(id);
            var categoryFromDbFirst = _shoppingcartService.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
        }

        //post
        [HttpPost]
        public IActionResult Edit(ShoppingCart obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "مقدار فیلد ترتیب نمایش نباید با مقدار فیلد نام برابر باشد");
            }

            if (ModelState.IsValid)
            {
                _shoppingcartService.Update(obj);
                TempData["success"] = "دسته با موفقیت ویرایش شد";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }


            var categoryFromDbFirst = _shoppingcartService.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            return View(categoryFromDbFirst);
        }

        //post
        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var obj = _shoppingcartService.GetFirstOrDefault(u => u.Id == id);
            _shoppingcartService.Remove(obj);
            TempData["success"] = "دسته با موفقیت حذف شد";
            return RedirectToAction("Index");
        }

    }
}
