using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelSmart.Models;
using PagedList;
using PagedList.Mvc;

namespace TravelSmart.Controllers
{
    public class TravelController : Controller
    {
        dbTravelSmartDataContext data = new dbTravelSmartDataContext();
        // GET: Travel

        private List<DIADIEM>LayDiaDiem(int count)
        {
            return data.DIADIEMs.OrderByDescending(a => a.Luot_YeuThich).Take(count).ToList();
        }
        public ActionResult Index()
        {
            // lay 6 dia diem
            var listDiaDiem = LayDiaDiem(6);
            return View(listDiaDiem);
        }
       public ActionResult ChiTietDiaDiem(int id)
        {
            var dd = data.DIADIEMs.SingleOrDefault(n => n.Ma_DD == id);
            return View(dd);    
        }
       
        private List<TOUR> LayTOUR(int count)
        {
            return data.TOURs.OrderBy(a => a.SoLuong).Take(count).ToList();
        }
        public ActionResult TourPartial()
        {
            var dsTour = LayTOUR(3);
            return PartialView(dsTour);
        }
        private List<HOTEL> LayLUUTRU(int count)
        {
            return data.HOTELs.OrderByDescending(a => a.Ma_HOTEL).Take(count).ToList();
        }
        public ActionResult LUUTRU()
        {
            var hotel = from lt in data.HOTELs select lt;
            return View(hotel);
        }
        public ActionResult TOUR(int ? page)
        {
            int iSize = 3 ;
            int IPageNum = (page ?? 1);
            var tour = from t in data.TOURs select t;
            return View(tour.ToPagedList(IPageNum,iSize));
        }
        public ActionResult AMTHUC()
        {
            var at = from a in data.AMTHUCs select a;
            return View(at);
        }
        public ActionResult ChiTietAMTHUC(int id)
        {
            var dd = data.AMTHUCs.SingleOrDefault(n => n.Ma_AT == id);
            return View(dd);
        }
        public ActionResult GIAITRI()
        {
            var gt = from g in data.GIAITRIs select g;
            return View(gt);
        }
        public ActionResult ChiTietGIAITRI(int id)
        {
            var dd = data.GIAITRIs.SingleOrDefault(n => n.Ma_GT == id);
            return View(dd);
        }
        public ActionResult LuuTruPartial()
        {
            var dsluutru = LayLUUTRU(3);
            return PartialView(dsluutru);
        }
        public ActionResult HeaderPartial()
        {
            return PartialView();
        }
        public ActionResult SlidePartial()
        {
            return PartialView();
        }
        public ActionResult DanhMucPartial()
        {
            return PartialView();
        }
        public ActionResult ChitietLUUTRU(int id)
        {
            var lt = from l in data.HOTELs
                     where l.Ma_HOTEL == id
                     select l;
            return View(lt.Single());
        }
       

       
       

        public ActionResult ChitietTour(int id)
        {
            var tour = from t in data.TOURs where t.Ma_TOUR == id select t;
            return View(tour.Single());
        }
        public ActionResult LoginLogout()
        {
            return PartialView("LoginLogout");
        }
        public ActionResult LienKet(int ? page)
        {
            int iSize = 3;
            int IPageNum = (page ?? 1);
           var lk = from kn in data.LIENKETs select kn;
            
            return View(lk.ToPagedList(IPageNum, iSize));
        }
        public ActionResult ChitietLienKet(int id)
        {
            var lk = from kn in data.LIENKETs
                     where kn.Ma_LK == id
                     select kn;
            return View(lk.Single());
        }
       
        public ActionResult Gioithieu()
        {
            return View();
        }
    }
}