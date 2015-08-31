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
        public ActionResult Index(string foodChoice, string searchString)
        {
            //Use DBContext
            List<string> Foods = new List<string>() { "All", "African", "All You Can Eat", "Bangladeshi", "barbecue", "breakfast", "brunch", 
                "buffet", "burgers", "Cantonese", "Carribean", "Chicken", "Chinese", "Coney Island", 
                "Creole", "fish", "gyros", "Indian", "Italian", "Japanese", "Korean", "Lebanese", 
                "Mediterranean", "Mexican", "Morroccan", "Nigerian", "organic", "Pakistani", "salad", "seafood", 
                "shwarmas", "Soul", "soup", "steak", "subs", "sushi", "tacos",
                "Thai", "Vietnamese"};
            ViewBag.foodChoice = new SelectList(Foods);
            ViewBag.foodValue = foodChoice;
            ViewBag.location = searchString;
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

                restaurants = restaurants.OrderBy(s => s.PriceLevel);
                //restaurantList.AddRange(restaurants.Distinct().ToList().ToString());

                //ViewBag.Restaurants = 
                return View(restaurants.ToList());
          
        }



    }
}
