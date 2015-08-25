using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using DetroitEatz.Models;
using DetroitEatz.DAL;
namespace DetroitEatz.Controllers
{
    public class HomeController : Controller
    {
        RestaurantContext db = new RestaurantContext();
        RestaurantContext db2 = new RestaurantContext();
        public ActionResult Index()
        {
            //Use DBContext
           
            
            //Create new Favorite
                Favorite newFav = new Favorite
                {

                    UserID = User.Identity.GetUserName(),
                    PlaceID = "k",
                    RestaurantName = "k"

                };
                    //Add That value to private DB
                db2.Favorites.Add(newFav);
                    //Save those changes from private DB to main DB
                db2.SaveChanges();
            
                        
            //Getting User Information
            string userName;
            string userID;
            
            if (User.Identity.IsAuthenticated)
            {
                userID = User.Identity.GetUserId();
                userName = HttpContext.User.Identity.Name;
                ViewBag.userName = userName;
                ViewBag.userID = userID;
            }

            //Creating Restaurant Tables

           
                //var restaurantList = new List<string>();


                var restaurants =  from r in db.Restaurants
                                   select r;

                //restaurantList.AddRange(restaurants.Distinct().ToList().ToString());

                //ViewBag.Restaurants = 
                return View(restaurants);
           


            
        }



    }
}
