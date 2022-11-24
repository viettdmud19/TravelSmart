using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelSmart.Models;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

namespace TravelSmart.Controllers
{
    public class FileAndEmailController : Controller
    {
        dbTravelSmartDataContext data = new dbTravelSmartDataContext();
        // GET: FileAndEmail
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SendMail()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return Redirect("~/User/DangNhap?id=1");
            }
            return View();
        }
        [HttpPost]
        public ActionResult SendMail(Mail model)
        {
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            var mail = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(kh.Email, "0947848841v"),
                EnableSsl = true
            };
            // tạo email
            var message = new MailMessage();
            message.From = new MailAddress(model.Form);
            message.ReplyToList.Add(model.Form);
            message.To.Add(new MailAddress(model.To));
            message.Subject = model.Subject;
            message.Body = model.Notes;

            var f = Request.Files["attachment"];
            var path = Path.Combine(Server.MapPath("~/UploadFile"), f.FileName);
            if (!System.IO.File.Exists(path))
            {
                f.SaveAs(path);
            }
            /// KHai báo thư viện using System.Net.Mine
            Attachment data = new Attachment(Server.MapPath("~/UploadFile/" + f.FileName), MediaTypeNames.Application.Octet);
            message.Attachments.Add(data);
            mail.Send(message);
            ViewBag.ThongBao = "Gửi thành công";
            return View("SendMail");
        }
    }
}