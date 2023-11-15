using GacXep.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GacXep.Controllers
{
    public class CustomersController : Controller
    {
        private DBGacXepBookstoreEntities db = new DBGacXepBookstoreEntities();
        // GET: Customers
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Customer customer)
        {
            if(ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(customer.CusName))
                    ModelState.AddModelError(string.Empty, "Họ và tên không được để trống");
                if (string.IsNullOrEmpty(customer.CusEmail))
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                if (string.IsNullOrEmpty(customer.CusPhone))
                    ModelState.AddModelError(string.Empty, "Số điện thoại không được để trống");
                if (string.IsNullOrEmpty(customer.CusPassword))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");

                var khachhang = db.Customers.FirstOrDefault(k => k.CusEmail == customer.CusEmail);
                if (khachhang != null)
                    ModelState.AddModelError(string.Empty, "Đã có người đăng kí email này");
                if (ModelState.IsValid)
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                }
                else
                {
                    return View();
                }
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login (Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(customer.CusEmail))
                    ModelState.AddModelError(string.Empty, "Email không được để trống");
                if (string.IsNullOrEmpty(customer.CusPassword))
                    ModelState.AddModelError(string.Empty, "Mật khẩu không được để trống");
                if(ModelState.IsValid)
                {
                    var khachhang = db.Customers.FirstOrDefault(k => k.CusEmail == customer.CusEmail && k.CusPassword == customer.CusPassword);
                    if(khachhang != null)
                    {
                        ViewBag.ThongBao = "Bạn đã đăng nhập thành công";
                        Session["TaiKhoan"] = khachhang;
                    }
                    else
                    {
                        ViewBag.ThongBa = "Tên đăng nhập hoặc mật khẩu không đúng";
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index, Home");
        }
        public ActionResult Profile()
        {
            return View();
        }
    }
}