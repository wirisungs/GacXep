using GacXep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GacXep.Controllers
{
    public class AdminController : Controller
    {
        private DBGacXepBookstoreEntities db = new DBGacXepBookstoreEntities();

        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(user.Username))
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                if (string.IsNullOrEmpty(user.Password))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                


                var quantrivien = db.Users.FirstOrDefault(q  => q.Username == user.Username);
                if (quantrivien != null)
                    ModelState.AddModelError(string.Empty, "Đã có người đăng kí tài khoản này, nếu chưa xin hãy liên hệ với bộ phận IT");

                if (ModelState.IsValid)
                {
                    db.Users.Add(quantrivien);
                    db.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("Login", "Admin");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(user.Username))
                        ModelState.AddModelError(string.Empty, "Tên đăng nhập không được để trống");
                    if (string.IsNullOrEmpty(user.Password))
                        ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                    var quantrivien = db.Users.FirstOrDefault(q => q.Username == user.Username);
                    if (quantrivien != null)
                    {
                        ViewBag.ThongBao = "Bạn đã đăng nhập thành công vào trang quản trị GacXepBookstore";
                        Session["QuanTri"] = quantrivien;
                    }
                    else
                        ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return RedirectToAction("Index", "Admin");
        }
    }
}