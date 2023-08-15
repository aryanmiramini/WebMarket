using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebMarket.DataAccess.Services.Interface;
using WebMarket.Models.ViewModels;

namespace WebMarket.Web.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ادمین")]

    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly ICoverTypeService _coverTypeService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IProductService productService
            , ICategoryService categoryService, ICoverTypeService coverTypeService,
            IWebHostEnvironment hostEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _coverTypeService = coverTypeService;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Get
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _categoryService.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _coverTypeService.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
               
                return View(productVM);
            }
            else
            {
                
                productVM.Product = _productService.GetFirstOrDefault(u => u.Id == id);
                if(productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if (productVM.Product.Id == 0)
                {
                    //Creating
                    string upload = Path.Combine(wwwRootPath, @"images\products");
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productVM.Product.ImageUrl = fileName + extension;

                    _productService.Add(productVM);
                }
                else
                {
                    //updating
                    var objFromDb = _productService.GetFirstOrDefault(u => u.Id == productVM.Product.Id);

                    if (files.Count > 0)
                    {
                        string upload = Path.Combine(wwwRootPath, @"images\products");
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var oldFile = Path.Combine(upload, objFromDb.ImageUrl);

                        if (System.IO.File.Exists(oldFile))
                        {
                            System.IO.File.Delete(oldFile);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }

                        productVM.Product.ImageUrl = fileName + extension;
                    }
                    else
                    {
                        productVM.Product.ImageUrl = objFromDb.ImageUrl;
                    }
                    _productService.Update(productVM.Product);
                }

                TempData["success"] = "محصول با موفقیت ویرایش شد";
                return RedirectToAction("Index");
            }


            productVM = new()
            {
                Product = new(),
                CategoryList = _categoryService.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _coverTypeService.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            return View(productVM);
        }


        #region API CALL
        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _productService.GetAll();
            return Json(new { data = productList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _productService.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "خطا در حذف " });
            }
            string wwwRootPath = _hostEnvironment.WebRootPath;
            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            var uploads = Path.Combine(wwwRootPath, @"images\products");
            var oldfile = Path.Combine(uploads, obj.ImageUrl);

            if (System.IO.File.Exists(oldfile))
            {
                System.IO.File.Delete(oldfile);
            }
            _productService.Remove(obj);
            return Json(new { success = true, message = "حذف موفقیت آمیز بود" });
        }
        #endregion

    }
}
