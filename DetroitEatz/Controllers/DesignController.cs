using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DetroitEatz.Controllers
{
    public class DesignController : Controller
    {
        // GET: Design
        public ActionResult Index(/*List<Restaurant>? unvalidatedRestaurants*/)
        {


            return View();
        }
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }


    }
}