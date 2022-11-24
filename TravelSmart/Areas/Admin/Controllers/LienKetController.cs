using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelSmart.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace TravelSmart.Areas.Admin.Controllers
{
    
    public class LienKetController : Controller
    {
        dbTravelSmartDataContext data = new dbTravelSmartDataContext();
        // GET: Admin/LienKet
        public ActionResult Index(int ? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 6;
            return View(data.LIENKETs.ToList().OrderBy(n => n.Ma_LK).ToPagedList(iPageNum, iPageSize));
        }
        public ActionResult Detail_LK(int id)
        {
            var dd = data.LIENKETs.SingleOrDefault(n => n.Ma_LK == id);
            if (dd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dd);
        }
        [HttpGet]
        public ActionResult Create_LK()
        {

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create_LK(LIENKET dd, FormCollection f, HttpPostedFileBase fFileUpload)
        {

            if (fFileUpload == null)
            {
                // nội dung thông báo yêu cầu chọn ảnh bìa
                ViewBag.ThongBao = "Hãy chọn ảnh bìa.";
                //lưu thông tin để khi load lại trang do yêu cầu
                // chọn ảnh bìa sẽ hiển thị các thông tin này lên trang
                ViewBag.Ten_LK = f["sTen_LK"];
                ViewBag.DienThoai = f["sDienThoai"];
                ViewBag.Email = f["sEmail"];
                ViewBag.DiaChi = f["sDiaChi"];
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
                    dd.Ten_LK = f["sTen_LK"];
                    dd.DienThoai = f["sDienThoai"];
                    dd.Anh_LK = sFileName;
                    dd.Email = f["sEmail"];
                    dd.DiaChi = f["sDiaChi"];
                    data.LIENKETs.InsertOnSubmit(dd);
                    data.SubmitChanges();
               
                    return RedirectToAction("Index");
                }
                return View();
            }
        }
        [HttpGet]
        public ActionResult EditLK(int id)
        {
            var t = data.LIENKETs.SingleOrDefault(n => n.Ma_LK == id);
            if (t == null)
            {
                Response.StatusCode = 404;
                return null;
            }

           
            return View(t);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditLK(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            var t = data.LIENKETs.SingleOrDefault(n => n.Ma_LK == int.Parse(f["iMa_LK"]));
            

            if (ModelState.IsValid)
            {
                if (fFileUpload != null)
                // kiểm tra để xác nhận cho thay đổi ảnh bìa
                {
                    // lấy tên file(khai báo thư viện: system.io)
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                  
                    // lấy đường dẫn lưu file
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                  
                    // kiểm tra file đã tồn tại chưa
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);

                    }
                  
                    t.Anh_LK = sFileName;
                   
                }
                // lưu sách vào csdl
                t.Ten_LK = f["sTen_LK"];
                t.DienThoai = f["sDienThoai"];
              
                t.Email = f["sEmail"];
                t.DiaChi = f["sDiaChi"];
              
                data.SubmitChanges();

                return RedirectToAction("Index");
            }
            return View(t);
        }
        [HttpGet]
        public ActionResult DeleteLK(int id)
        {
            var t = data.LIENKETs.SingleOrDefault(n => n.Ma_LK == id);
            if (t == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(t);
        }
        [HttpPost, ActionName("DeleteLK")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var t = data.LIENKETs.SingleOrDefault(n => n.Ma_LK == id);
            if (t == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var tour = data.TOURs.Where(n => n.Ma_LK == id);
            if (tour.Count() > 0)
            {
                // nội dung sẽ hiển thị khi sách cần xóa  đã có trong table
                // // chi tiết đơn hàng
                ViewBag.ThongBao = "Tour này đang có trong bảng Tour<br>" +
                    "Nếu muốn xóa thì phải xóa hết tour này trong bảng TOUR";
                return View(t);
            }
            // xóa hết thông tin của cuốn sách trong table Vietsach trước khi xóa sách này
            var tour1 = data.TOURs.Where(vs => vs.Ma_TOUR == id).ToList();
            if (tour1 != null)
            {
                data.TOURs.DeleteAllOnSubmit(tour1);
                data.SubmitChanges();
            }
            // xóa sách
            data.LIENKETs.DeleteOnSubmit(t);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}