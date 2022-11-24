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
    public class KhachHangController : Controller
    {
        dbTravelSmartDataContext data = new dbTravelSmartDataContext();
        // GET: Admin/KhachHang
        public ActionResult IndexKH(int ? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 12;
            return View(data.KHACHHANGs.ToList().OrderBy(n => n.MaKH).ToPagedList(iPageNum, iPageSize));
          
        }
        
    }
}