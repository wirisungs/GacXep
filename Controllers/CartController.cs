using GacXep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace GacXep.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        

        public List<CartItem> GetCart() 
        {
            List<CartItem> myCart = Session["GioHang"] as List<CartItem>;

            if(myCart == null)
            {
                myCart = new List<CartItem>();
                Session["GioHang"] = myCart;
            }
            return myCart;
        }

        public ActionResult AddToCart(int id)
        {
            List<CartItem> myCart = GetCart();

            CartItem currentBook = myCart.FirstOrDefault(b => b.ProID == id);
            if(currentBook == null)
            {
                currentBook = new CartItem(id);
                myCart.Add(currentBook);
            }
            else
            {
                currentBook.Quantity++;
            }
            return RedirectToAction("Index", "CustomerBooks", new { id = id });
        }

        private int GetTotalNumber()
        {
            int totalQuantity = 0;
            List<CartItem> myCart = GetCart();
            if(myCart != null)
                totalQuantity = myCart.Sum(sp => sp.Quantity);
            return totalQuantity;
        }

        private decimal GetTotalPrice()
        {
            decimal totalPrice = 0;
            List<CartItem> myCart = GetCart();
            if (myCart != null)
                totalPrice = myCart.Sum(sp => sp.FinalPrice());
            return totalPrice;
        }

        public ActionResult GetCartInfo()
        {
            List<CartItem> myCart = GetCart();

            //nếu giỏ hàng trống thì trả về trang all books

            if(myCart == null || myCart.Count == 0)
            {
                return RedirectToAction("EmptyCart", "Cart");
            }

            ViewBag.TotalNumber = GetTotalNumber();
            ViewBag.TotalPrice = GetTotalPrice();
            return View(myCart); //Trả về view hiển thị giỏ hàng
        }

        public ActionResult CartPartial()
        {
            ViewBag.TotalNumber = GetTotalNumber();
            ViewBag.TotalPrice = GetTotalPrice();
            return PartialView();
        }

        public ActionResult DeleteCartItem(int id)
        {
            List<CartItem> myCart = GetCart();

            var currentBook = myCart.FirstOrDefault(p => p.ProID == id);
            if (currentBook != null)
            {
                myCart.RemoveAll(p => p.ProID == id);
                return RedirectToAction("GetCartInfo");
            }
            if (myCart.Count == 0)
                return RedirectToAction("EmptyCart", "Cart");
            return RedirectToAction("GetCartInfo");
        }

        public ActionResult UpdateCartItem(int id, int Quantity)
        {
            List<CartItem> myCart = GetCart();
            //Lấy sản phẩm trong giỏ hàng
            var currentBook = myCart.FirstOrDefault (p => p.ProID == id);
            if (currentBook != null)
            {
                //Cập nhật lại số lượng tương ứng
                //Lưu ý số lượng phải >= 1

                currentBook.Quantity = Quantity;
            }
            return RedirectToAction("GetCartInfo"); //quay về trang giỏ hàng
        }

        public ActionResult ConfirmCart()
        {
            if (Session["TaiKhoan"] == null) //Chưa đăng nhập
                return RedirectToAction("Login", "Customers");
            List<CartItem> myCart = GetCart();
            if (myCart == null || myCart.Count == 0) //Chưa có giỏ hàng hoặc chưa có sp
                return RedirectToAction("EmptyCart", "Cart");

            ViewBag.TotalNumber = GetTotalNumber();
            ViewBag.TotalPrice = GetTotalPrice();
            return View(myCart); //Trả về View xác nhận đặt hàng
        }
        DBGacXepBookstoreEntities database = new DBGacXepBookstoreEntities();

        public ActionResult AgreeCart(FormCollection Form)
        {
            Customer khachhang = Session["TaiKhoan"] as Customer; //Khách
            List<CartItem> myCart = GetCart(); //Giỏ hàng

            Order DonHang = new Order();
            DonHang.CusPhone = khachhang.CusPhone;
            DonHang.OrderDate = DateTime.Now;
            DonHang.AddressDeliverry = Form["AddressDeliverry"];
            DonHang.TotalValue = (double)GetTotalPrice();

            database.Orders.Add(DonHang);
            database.SaveChanges();

            foreach(var book in myCart)
            {
                OrderDetail chitiet = new OrderDetail();
                chitiet.OrderID = DonHang.OrderID;
                chitiet.OrderDetailID = book.ProID;
                chitiet.Quantity = book.Quantity;
                chitiet.UnitPrice = (double)book.Price;
                database.OrderDetails.Add(chitiet);
            }
            database.SaveChanges();


            //xoá giỏ hàng
            Session["GioHang"] = null;
            return RedirectToAction("CheckOut_Success", "Cart");
        }


        
        public ActionResult CheckOut_Success()
        {
            return View();
        }

        public ActionResult EmptyCart()
        {
            return View();
        }
    }
}