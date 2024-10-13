using QLBANHANG1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBANHANG1.Controllers
{
    public class DanhMucController : Controller
    {
        private readonly DataClasses1DataContext db = new DataClasses1DataContext("Data Source=YENNGTH-0803\\MSSQLSERVER01;Initial Catalog=QLBANHANG1;Integrated Security=True");

        // GET: DanhMuc
        public ActionResult Index()
        {
            return View();
        }

        /* Action method for AJAX */
        [HttpPost]
        public JsonResult GetListDanhMuc()
        {
            var categories = db.Categories.Select(c => new
            {
                CategoryID = c.CategoryID,
                CategoryName = c.CategoryName,
                Description = c.Description,
                ParentCategoryID = c.ParentCategoryID,
                ParentCategoryName = db.Categories
    .Where(parent => parent.CategoryID == c.ParentCategoryID)
    .Select(parent => parent.CategoryName)
    .FirstOrDefault()

            }).ToList();

            return Json(categories, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
              ViewBag.ParentCategories = new SelectList(
                db.Categories.Select(c => new { c.CategoryID, c.CategoryName }),
                "CategoryID",
                "CategoryName"
            );
            var categories = db.Categories.ToList();
            ViewBag.ParentCategories = categories; // Truyền danh sách Category vào ViewBag
            return View();
        }

        // POST: DanhMuc/Create
        [HttpPost]

        public ActionResult Create(string categoryID, string categoryName, string description, string parentCategoryID)
        {
            if (ModelState.IsValid)
            {
                var newCategory = new Category
                {
                    CategoryID = categoryID,
                    CategoryName = categoryName,
                    Description = description,
                    ParentCategoryID = string.IsNullOrEmpty(parentCategoryID) ? null : parentCategoryID // Đảm bảo kiểu dữ liệu là string
                };

                db.Categories.InsertOnSubmit(newCategory);
                db.SubmitChanges();

                // Redirect to Index after successful creation
                return RedirectToAction("Index");
            }

            // If model is not valid, return to Create view with current data
            ViewBag.ParentCategories = new SelectList(
                db.Categories.Select(c => new { c.CategoryID, c.CategoryName }),
                "CategoryID",
                "CategoryName",
                parentCategoryID
            );
            return View(); ;
        }
    }
}

 
