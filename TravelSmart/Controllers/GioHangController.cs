using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelSmart.Models;
namespace TravelSmart.Controllers
{
    public class GioHangController : Controller
    {
        dbTravelSmartDataContext data = new dbTravelSmartDataContext();
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                //khởi tạo giỏ hàng ( giỏ hàng chưa tồn tại)
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int mt, string url)
        {
            // lấy giỏ hàng hiện tại
            List<GioHang> lstGioHang = LayGioHang();
            // Kiểm tra nếu sản phẩm chưa có trong giỏ thì thêm vào, nếu đã có thì tăng số lượng
            GioHang sp = lstGioHang.Find(n => n.iMaTour == mt);
            if (sp == null)
            {
                sp = new GioHang(mt);
                lstGioHang.Add(sp);
            }
            else
            {
                sp.iSoLuong++;
            }
            return Redirect(url);
        }
        private double TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dThanhTien);
            }
            return dTongTien;
        }
        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Travel");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult XoaSPKhoiGioHang(int iMaTour)
        {
            List<GioHang> lstGioHang = LayGioHang();
            //kiem tra sach da co trong session["GioHang"]
            GioHang sp = lstGioHang.SingleOrDefault(n => n.iMaTour == iMaTour);
            // xóa sp khỏi giỏ hàng
            if (sp != null)
            {
                lstGioHang.RemoveAll(n => n.iMaTour == iMaTour);
                if (lstGioHang.Count == 0)
                {
                    return RedirectToAction("Index", "Travel");
                }
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapNhatGioHang(int iMaTour, FormCollection f)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.iMaTour == iMaTour);
            // nếu tồn tại thì cho sửa số lượng
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaGioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "Travel");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            //Kiểm tra đăng nhập chưa
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return Redirect("~/User/DangNhap?id=2");
            }
            // kiểm tra có hàng trong giỏ chưa
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Travel");
            }
            // lấy hàng từ session
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            //thêm đơn hàng
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            List<GioHang> lstGioHang = LayGioHang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            ddh.DaThanhToan = false;
            data.DONDATHANGs.InsertOnSubmit(ddh);
            data.SubmitChanges();
            //thêm chi tiết đơn hàng
            foreach (var item in lstGioHang)
            {
                CHITIETDATHANG ctdh = new CHITIETDATHANG();
                ctdh.MaDonHang = ddh.MaDonHang;
                ctdh.MaTour = item.iMaTour;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.DonGia = (decimal)item.dDonGia;
                data.CHITIETDATHANGs.InsertOnSubmit(ctdh);

            }
            data.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}