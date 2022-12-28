using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webmarket.Models;
using WebMarket.DataAccess.Services.Interface;

namespace WebMarket.Web.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "ادمین")]

    public class CoverTypeController : Controller
    {
        private readonly ICoverTypeService _coverTypeService;

        public CoverTypeController(ICoverTypeService coverTypeService)
        {
            _coverTypeService = coverTypeService;
        }

        public IActionResult Index()
        {
            IEnumerable<CoverType> CoverTypeList = _coverTypeService.GetAll();
            return View(CoverTypeList);
        }

        //Get
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //post
        [HttpPost]
        public IActionResult Create(CoverType obj)
        {

            if (ModelState.IsValid)
            {
                _coverTypeService.Add(obj);
                TempData["success"] = "کاور تایپ جدید با موفقیت ایجاد شد";

                return RedirectToAction("Index");

            }
            return View(obj);


        }

        //Get
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            //var categoryFromDb = _db.Categories.Find(id);
            var coverTypeFromDbFirst = _coverTypeService.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (coverTypeFromDbFirst == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDbFirst);
        }

        //post
        [HttpPost]
        public IActionResult Edit(CoverType obj)
        {

            if (ModelState.IsValid)
            {
                _coverTypeService.Update(obj);
                TempData["success"] = "تایپ با موفقیت ویرایش شد";
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


            var coverTypeFromDbFirst = _coverTypeService.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (coverTypeFromDbFirst == null)
            {
                return NotFound();
            }

            return View(coverTypeFromDbFirst);
        }

        //post
        [HttpPost]
        public IActionResult DeletePost(int? id)
        {
            var obj = _coverTypeService.GetFirstOrDefault(u => u.Id == id);
            _coverTypeService.Remove(obj);
            TempData["success"] = "تایپ با موفقیت حذف شد";
            return RedirectToAction("Index");
        }

    }
}
