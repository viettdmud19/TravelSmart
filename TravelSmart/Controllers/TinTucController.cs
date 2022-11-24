using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelSmart.Models;
using PagedList.Mvc;
using PagedList;
using System.IO;
    
namespace TravelSmart.Controllers
{
    public class TinTucController : Controller
    {
        dbTravelSmartDataContext data = new dbTravelSmartDataContext();
        // GET: TinTuc
        public ActionResult Index_TT(int ? page)
        {
            int iSize = 6;
            int IPageNum = (page ?? 1);
            var tt = from t in data.TINTUCs select t;
            return View(tt.ToPagedList(IPageNum, iSize));
          
        }
        [HttpGet]
        public ActionResult Create_TT()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return Redirect("~/User/DangNhap?id=1");
            }
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create_TT(TINTUC t, FormCollection f, HttpPostedFileBase fFileUpload)
        {

            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            if (fFileUpload == null)
            {
                // nội dung thông báo yêu cầu chọn ảnh bìa
                ViewBag.ThongBao = "Hãy chọn ảnh bìa.";
                //lưu thông tin để khi load lại trang do yêu cầu
                // chọn ảnh bìa sẽ hiển thị các thông tin này lên trang
                ViewBag.TieudeTT = f["sTieuDe_TT"];
                ViewBag.MotaTT = f["sMoTa_TT"];
                return View();

            }
            else
            {
                if (ModelState.IsValid)
                {
                    // lấy tên file (khai báo thư viện :System.Io
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    // lấy đường dẫn lưu file
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    // kiểm tra ảnh bìa đã tồn tại chưa để lưu lên thư mục
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    // lưu Sách vào csdl
                    t.TieuDe_TT = f["sTieuDe_TT"];
                    t.MoTa_TT = f["sMoTa_TT"].Replace("<p>", "").Replace("<p>", "");
                    t.Anh_TT = sFileName; 
                    t.NgayDang_TT = DateTime.Now;
                    t.Ma_KH = kh.MaKH;
                   
                    data.TINTUCs.InsertOnSubmit(t);
                    data.SubmitChanges();
                    // về lại trang quản lý sách
                    return RedirectToAction("Index_TT");
                }
                return View();
            }
        }
        public ActionResult Details_TT(int id)
        {
            var dd = data.TINTUCs.SingleOrDefault(n => n.ma_TT == id);
            if (dd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dd);
        }
    }
}