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
    public class TourController : Controller
    {
        dbTravelSmartDataContext data = new dbTravelSmartDataContext();
        // GET: Admin/Tour
        public ActionResult IndexTour(int ? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 6;
            return View(data.TOURs.ToList().OrderBy(n => n.Ma_TOUR).ToPagedList(iPageNum, iPageSize));
        }
        public ActionResult Details_Tour(int id)
        {
            var dd = data.TOURs.SingleOrDefault(n => n.Ma_TOUR == id);
            if (dd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dd);
        }
        [HttpGet]
        public ActionResult CreateTour()
        {
            // lấy danh sách từ các table Chude, nhaxuatban, hien thi ten,khi chon sẽ lấy mã
            ViewBag.MaDD = new SelectList(data.DIADIEMs.ToList().OrderBy(n => n.Ten_DD), "Ma_DD", "Ten_DD");
            ViewBag.MaLK = new SelectList(data.LIENKETs.ToList().OrderBy(n => n.Ten_LK), "Ma_LK", "Ten_LK");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateTour(TOUR t, FormCollection f, HttpPostedFileBase fFileUpload, HttpPostedFileBase fFileUp)
        {
            //đưa dữ liệu vào DropDown
            ViewBag.MaDD = new SelectList(data.DIADIEMs.ToList().OrderBy(n => n.Ten_DD), "Ma_DD", "Ten_DD");
            ViewBag.MaLK = new SelectList(data.LIENKETs.ToList().OrderBy(n => n.Ten_LK), "Ma_LK", "Ten_LK");

            if (fFileUpload == null)
            {
                // nội dung thông báo yêu cầu chọn ảnh bìa
                ViewBag.ThongBao = "Hãy chọn ảnh bìa.";
                //lưu thông tin để khi load lại trang do yêu cầu
                // chọn ảnh bìa sẽ hiển thị các thông tin này lên trang
                ViewBag.Ten_Tour = f["sTen_TOUR"];
                ViewBag.LichTrinh = f["sLich_Trinh"];
                ViewBag.GiaTOUR = decimal.Parse(f["mGiaTien_TOUR"]);
                ViewBag.SoLuong = int.Parse(f["iSoLuong"]);
                ViewBag.NgayTOUR = Convert.ToDateTime(f["dNgayTOUR"]);
                ViewBag.MaDD = new SelectList(data.DIADIEMs.ToList().OrderBy(n => n.Ten_DD), "Ma_DD", "Ten_DD", int.Parse(f["Ma_DD"]));
                ViewBag.MaLK = new SelectList(data.LIENKETs.ToList().OrderBy(n => n.Ten_LK), "Ma_LK", "Ten_LK", int.Parse(f["Ma_LK"]));

                return View();

            }
            else
            {
                if (ModelState.IsValid)
                {
                    // lấy tên file (khai báo thư viện :System.Io
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var sFile = Path.GetFileName(fFileUp.FileName);
                    // lấy đường dẫn lưu file
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    var path1 = Path.Combine(Server.MapPath("~/Images"), sFile);
                    // kiểm tra ảnh bìa đã tồn tại chưa để lưu lên thư mục
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    if (!System.IO.File.Exists(path1))
                    {
                        fFileUp.SaveAs(path1);
                    }
                    // lưu Sách vào csdl
                    t.Ten_TOUR = f["sTen_TOUR"];
                    t.Anh_MAIN = sFileName;
                    t.Anh_TOUR = sFile;
                    t.Lich_Trinh = f["sLich_Trinh"].Replace("<p>", "").Replace("<p>", "");
                    t.GiaTien_TOUR = decimal.Parse(f["mGiaTien_TOUR"]);
                    t.SoLuong = int.Parse(f["iSoLuong"]);
                    t.NgayTOUR = Convert.ToDateTime(f["dNgayTOUR"]);
                    t.Ma_DD = int.Parse(f["MaDD"]);
                    t.Ma_LK = int.Parse(f["MaLK"]);
                    data.TOURs.InsertOnSubmit(t);
                    data.SubmitChanges();
                   
                    return RedirectToAction("IndexTour");
                }
                return View();
            }
        }
        [HttpGet]
        public ActionResult EditTour(int id)
        {
            var t = data.TOURs.SingleOrDefault(n => n.Ma_TOUR == id);
            if (t == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            
            ViewBag.MaDD = new SelectList(data.DIADIEMs.ToList().OrderBy(n => n.Ten_DD), "Ma_DD", "Ten_DD", t.Ma_DD);
            ViewBag.MaLK = new SelectList(data.LIENKETs.ToList().OrderBy(n => n.Ten_LK), "Ma_LK", "Ten_LK", t.Ma_LK);
            return View(t);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditTour(FormCollection f, HttpPostedFileBase fFileUpload , HttpPostedFileBase fFileUp)
        {
            var t = data.TOURs.SingleOrDefault(n => n.Ma_TOUR == int.Parse(f["iMa_TOUR"]));
            ViewBag.MaDD = new SelectList(data.DIADIEMs.ToList().OrderBy(n => n.Ten_DD), "Ma_DD", "Ten_DD", t.Ma_DD);
            ViewBag.MaLK = new SelectList(data.LIENKETs.ToList().OrderBy(n => n.Ten_LK), "Ma_LK", "Ten_LK", t.Ma_LK);
          
            if (ModelState.IsValid)
            {
                if (fFileUpload != null)
                // kiểm tra để xác nhận cho thay đổi ảnh bìa
                {
                    // lấy tên file(khai báo thư viện: system.io)
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var sFile = Path.GetFileName(fFileUp.FileName);
                    // lấy đường dẫn lưu file
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    var path1 = Path.Combine(Server.MapPath("~/Images"), sFile);
                    // kiểm tra file đã tồn tại chưa
                    if (!System.IO.File.Exists(path) )
                    {
                        fFileUpload.SaveAs(path);
                       
                    }
                    if(!System.IO.File.Exists(path1))
                    {
                        fFileUp.SaveAs(path1);
                    }    
                    t.Anh_MAIN = sFileName;
                    t.Anh_TOUR = sFile;
                }
                // lưu sách vào csdl
                t.Ten_TOUR = f["sTen_TOUR"];
                t.Lich_Trinh = f["sLich_Trinh"].Replace("<p>", "").Replace("<p>", "");
                t.GiaTien_TOUR = decimal.Parse(f["mGiaTien_TOUR"]);
                t.SoLuong = int.Parse(f["iSoLuong"]);
                t.NgayTOUR = Convert.ToDateTime(f["dNgayTOUR"]);
                t.Ma_DD = int.Parse(f["MaDD"]);
                t.Ma_LK = int.Parse(f["MaLK"]);
            
                data.SubmitChanges();
                return RedirectToAction("IndexTour");
            }
            return View(t);
        }
        [HttpGet]
        public ActionResult DeleteTour(int id)
        {
            var t = data.TOURs.SingleOrDefault(n => n.Ma_TOUR == id);
            if (t == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(t);
        }
        [HttpPost, ActionName("DeleteTour")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var t = data.TOURs.SingleOrDefault(n => n.Ma_TOUR == id);
            if (t == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var cthd = data.CHITIETDATHANGs.Where(ct => ct.MaTour == id);
            if (cthd.Count() > 0)
            {
                
                ViewBag.ThongBao = "Tour này đang có trong bảng đặt vé<br>" +
                    "Nếu muốn xóa thì phải xóa hết tour này trong bảng đặt vé";
                return View(t);
            }
 
            var ctdh1 = data.CHITIETDATHANGs.Where(vs => vs.MaTour == id).ToList();
            if (ctdh1 != null)
            {
                data.CHITIETDATHANGs.DeleteAllOnSubmit(ctdh1);
                data.SubmitChanges();
            }
           
            data.TOURs.DeleteOnSubmit(t);
            data.SubmitChanges();
            return RedirectToAction("IndexTour");
        }
    }
}