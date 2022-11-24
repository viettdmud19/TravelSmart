using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelSmart.Models;
namespace TravelSmart.Controllers
{
    public class SearchController : Controller
    {
        dbTravelSmartDataContext data = new dbTravelSmartDataContext();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string strSearch)
        {

            ViewBag.Search = strSearch;
            if (!string.IsNullOrEmpty(strSearch))
            {
               
                  var kq = from s in data.TOURs where s.Ten_TOUR.Contains(strSearch) || s.Lich_Trinh.Contains(strSearch) select s;
                //  var kq = from s in data.SACHes where s.SoLuongBan >= 5 && s.SoLuongBan <= 10 orderby (s.SoLuongBan) descending select s;
               
                //var kq = data.SACHes.Where(s => s.MaNXB == int.Parse(strSearch)).OrderBy(s => s.SoLuongBan).ToList();
                return View(kq);
            }
            return View();
        }
    }
}