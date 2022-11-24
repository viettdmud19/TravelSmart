using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TravelSmart
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            System.IO.StreamReader stReader = new System.IO.StreamReader(HttpContext.Current.Server.MapPath("~/LuotTruyCap.txt"));
            String s = stReader.ReadLine();
            stReader.Close();
            Application.Add("Hitcounter", s);
            Application["Online"] = 0;
        }
        void Session_Start(object Index, EventArgs e)
        {
            Application.Lock();
            Application["Online"] = int.Parse(Application["Online"].ToString()) + 1;
            Application["HitCounter"] = int.Parse(Application["HitCounter"].ToString()) + 1;
            Application.UnLock();
            System.IO.StreamWriter stWriter = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("~/LuotTruyCap.txt"));
            stWriter.Write(Application["HitCounter"]);
            stWriter.Close();
        }

        void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["Online"] = int.Parse(Application["Online"].ToString()) - 1;
            Application.UnLock();
        }
    }
    }

