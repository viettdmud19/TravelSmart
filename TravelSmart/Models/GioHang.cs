using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelSmart.Models;
namespace TravelSmart.Models
{
    public class GioHang
    {
        dbTravelSmartDataContext data = new dbTravelSmartDataContext();
        public int iMaTour { get; set; }
        public string sTenTour { get; set; }
        public string sAnhTour { get; set; }
        public double  dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        public GioHang(int mt)
        {
            iMaTour = mt;
            TOUR s = data.TOURs.Single(n => n.Ma_TOUR == iMaTour);
            sTenTour = s.Ten_TOUR;
            sAnhTour = s.Anh_TOUR ;
            dDonGia = double.Parse(s.GiaTien_TOUR.ToString());
            iSoLuong = 1;
        }
    }
}