using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using DetroitEatz.Models;
namespace DetroitEatz.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {

            //Create new Favorite
            Favorite newFav = new Favorite
            {

                UserID = User.Identity.GetUserName(),
                PlaceID = "k",
                RestaurantName = "k"

            };

            
            
            
            
            string userName;
            string userID;
            
            if (User.Identity.IsAuthenticated)
            {
                userID = User.Identity.GetUserId();
                userName = HttpContext.User.Identity.Name;
                ViewBag.userName = userName;
                ViewBag.userID = userID;
            }
            return View();
        }



    }
}
