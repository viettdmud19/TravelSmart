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
    public class DiaDiemController : Controller
    {
        dbTravelSmartDataContext data = new dbTravelSmartDataContext();
        // GET: Admin/DiaDiem
        public ActionResult Index(int ? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 6;
            return View(data.DIADIEMs.ToList().OrderBy(n=>n.Ma_DD).ToPagedList(iPageNum,iPageSize));
        }
        public ActionResult Details(int id)
        {
            var dd = data.DIADIEMs.SingleOrDefault(n => n.Ma_DD == id);
            if (dd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dd);
        }
        [HttpGet]
        public ActionResult Create()
        {
          
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(DIADIEM dd, FormCollection f, HttpPostedFileBase fFileUpload)
        {

            if (fFileUpload == null)
            {
                // nội dung thông báo yêu cầu chọn ảnh bìa
                ViewBag.ThongBao = "Hãy chọn ảnh bìa.";
                //lưu thông tin để khi load lại trang do yêu cầu
                // chọn ảnh bìa sẽ hiển thị các thông tin này lên trang
                ViewBag.Ten_DD = f["sTen_DD"];
                ViewBag.MoTa = f["sMoTa_DD"];
                
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
                    dd.Ten_DD= f["sTen_DD"];
                    dd.MoTa_DD = f["sMoTa_DD"].Replace("<p>", "").Replace("<p>", "");
                    dd.ANH_DD = sFileName;
                    data.DIADIEMs.InsertOnSubmit(dd);
                    data.SubmitChanges();
                    // về lại trang quản lý sách
                    return RedirectToAction("Index");
                }
                return View();
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var dd = data.DIADIEMs.SingleOrDefault(n => n.Ma_DD == id);
            if (dd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dd);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var dd = data.DIADIEMs.SingleOrDefault(n => n.Ma_DD == id);
            if (dd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var tour = data.TOURs.Where(t => t.Ma_DD == id);
           
            if (tour.Count() > 0 )
            {
                // nội dung sẽ hiển thị khi sách cần xóa  đã có trong table
                // // chi tiết đơn hàng
                ViewBag.ThongBao = "Sách này đang có trong bảng TOUR <br>" +
                    "Nếu muốn xóa thì phải xóa hết mã sách này trong bảng TOUR";
                return View(dd);
            }
            // xóa hết thông tin của cuốn sách trong table Vietsach trước khi xóa sách này
            var tour1 = data.TOURs.Where(vs => vs.Ma_TOUR == id).ToList();
           
            if (tour1 != null)
            {
                data.TOURs.DeleteAllOnSubmit(tour1);
                data.SubmitChanges();
            }
            // xóa sách
            data.DIADIEMs.DeleteOnSubmit(dd);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dd = data.DIADIEMs.SingleOrDefault(n => n.Ma_DD == id);
            if (dd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
           
            
            return View(dd);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            var dd = data.DIADIEMs.SingleOrDefault(n => n.Ma_DD == int.Parse(f["iMa_DD"]));
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
                    dd.ANH_DD = sFileName;
                }
                // lưu sách vào csdl
                dd.Ten_DD = f["sTen_DD"];
                dd.MoTa_DD = f["sMoTa_DD"].Replace("<p>", "").Replace("<p>", "\n");
              
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(dd);
        }
    }
}