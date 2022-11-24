using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelSmart.Models;
using PagedList;
using PagedList.Mvc;

namespace TravelSmart.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        dbTravelSmartDataContext data = new dbTravelSmartDataContext();
        // GET: Admin/DonHang
        public ActionResult Index_DonHang(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 6;
            return View(data.DONDATHANGs.ToList().OrderBy(n => n.NgayDat).ToPagedList(iPageNum, iPageSize));
        }
        public ActionResult Chitiet(int id)
        {
            var dd = data.CHITIETDATHANGs.SingleOrDefault(n => n.MaDonHang == id);
            if (dd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dd);
        }
      
    }
}