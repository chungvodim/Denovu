using Denovu.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Denovu.Web.Controllers
{
    public class HomeController : Controller
    {
        protected DenovuEntities db = new DenovuEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetNearestLocations(double longtitude, double lattitude, int numberOfLocations)
        {
            try
            {
                var locations = db.Locations.Where(l => l.Longitude != null && l.Latitude != null).ToList();
                foreach (var loc in locations)
                {
                    loc.Distance = DetermineDistance(longtitude, lattitude, loc.Longitude.Value, loc.Latitude.Value);
                }
                locations = locations.OrderBy(l => l.Distance).Take(numberOfLocations).ToList();
                foreach (var loc in locations)
                {
                    loc.Route = DetermineRoute(loc) + ">" + loc.Name + "(" + loc.Id + ")";
                }
                var result = locations.OrderBy(l => l.Distance).Take(numberOfLocations).Select(l => l.Route).ToList();
                return Json(new { status = 1, message = "successful", data = JsonConvert.SerializeObject(result).Replace(",", "<br/>").Replace("\"", "").Replace("[", "").Replace("]", "") }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, message = ex.Message, data = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        private string DetermineRoute(Location loc)
        {
            if (loc.ParentId == null) return string.Empty;
            var parent = db.Locations.Find(loc.ParentId);
            if (parent == null) return string.Empty;
            return DetermineRoute(parent) + ">" + parent.Name;
        }

        private double DetermineDistance(double longtitude, double latitude, double longtitude1, double latitude1)
        {
            double distance = 0.0;
            distance = Math.Sqrt(Math.Pow(longtitude - longtitude1, 2.0) + Math.Pow(latitude - latitude1, 2.0));
            return distance;
        }
    }
}