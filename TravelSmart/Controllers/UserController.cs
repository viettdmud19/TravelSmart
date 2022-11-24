using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelSmart.Models;

namespace TravelSmart.Controllers
{
    public class UserController : Controller
    {
        dbTravelSmartDataContext data = new dbTravelSmartDataContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {

            int state = int.Parse(Request.QueryString["id"]);
            var sTenDN = collection["TaiKhoan"];
            var sMatKhau = collection["MatKhau"];
            if (String.IsNullOrEmpty(sTenDN))
            {
                ViewData["Err1"] = "Bạn chưa nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(sMatKhau))
            {
                ViewData["Err2"] = "Phải nhập mật khẩu";
            }
            else
            {

                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == sTenDN && n.MatKhau == sMatKhau);
                if (kh != null)
                {
                    Session["TaiKhoan"] = kh;
                    if (state == 1)
                    {  
                       
                        if (collection["remember"].Contains("true"))
                        {
                            Response.Cookies["TaiKhoan"].Value = sTenDN;
                            Response.Cookies["MatKhau"].Value = sMatKhau;
                            Response.Cookies["TaiKhoan"].Expires = DateTime.Now.AddDays(1);
                            Response.Cookies["MatKhau"].Expires = DateTime.Now.AddDays(1);
                        }
                        else
                        {
                            Response.Cookies["TaiKhoan"].Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies["MatKhau"].Expires = DateTime.Now.AddDays(-1);
                        }
                        return RedirectToAction("Index", "Travel");
                      
                    }

                    else
                    {
                        return RedirectToAction("DatHang", "GioHang");
                    }
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            var sHoTen = collection["HoTen"];
            var sTenDN = collection["TaiKhoan"];
            var sMatKhau = collection["MatKhau"];
            var sMatKhauNhapLai = collection["MatKhauNL"];
            var sDiaChi = collection["DiaChi"];
            var sEmail = collection["Email"];
            var sDienThoai = collection["DienThoai"];
            var dNgaySinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);

            if (String.IsNullOrEmpty(sMatKhauNhapLai))
            {
                ViewData["err4"] = "Phải nhập lại mật khẩu";
            }
            else if (sMatKhau != sMatKhauNhapLai)
            {
                ViewData["err4"] = "Mật khẩu nhập lại không khớp";
            }

            else if (data.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == sTenDN) != null)
            {
                ViewBag.ThongBao = " Tên đăng nhập đã tồn tại";
            }
            else if (data.KHACHHANGs.SingleOrDefault(n => n.Email == sEmail) != null)
            {
                ViewBag.ThongBao = " Email đã sử dụng";
            }
            else if (ModelState.IsValid)
            {
                kh.HoTen = sHoTen;
                kh.TaiKhoan = sTenDN;
                kh.MatKhau = sMatKhau;
                kh.Email = sEmail;
                kh.DiaChi = sDiaChi;
                kh.DienThoai = sDienThoai;
                kh.NgaySinh = DateTime.Parse(dNgaySinh);
                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();
                return Redirect("~/User/DangNhap?id=1");
            }
            return this.DangKy();
        }

        public ActionResult DangXuat()
        {
            Session.Clear();

            return Redirect("~/User/DangNhap?id=1");
        }
      
    }
}