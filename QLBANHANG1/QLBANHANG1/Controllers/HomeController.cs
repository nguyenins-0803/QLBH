using QLBANHANG1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLBANHANG1.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataClasses1DataContext db = new DataClasses1DataContext("Data Source=YENNGTH-0803\\MSSQLSERVER01;Initial Catalog=QLBANHANG1;Integrated Security=True");

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string searchTerm, string categoryId)
        {
            var products = db.Products.AsQueryable(); // Bắt đầu với tất cả sản phẩm

            // Kiểm tra nếu có từ khóa tìm kiếm
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                products = products.Where(p => p.ProductName.Contains(searchTerm)); // Tìm theo tên sản phẩm
            }

            // Kiểm tra nếu có danh mục được chọn
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                products = products.Where(p => p.CategoryID == categoryId); // Tìm theo danh mục
            }

            // Chuyển đổi danh sách sản phẩm thành ViewModel hoặc ViewBag nếu cần
            var productList = products.ToList(); // Lấy danh sách sản phẩm

            ViewBag.Categories = db.Categories.ToList(); // Lấy danh sách danh mục để hiển thị trong view

            return View(productList); // Trả về view với danh sách sản phẩm
        }
    }

}
