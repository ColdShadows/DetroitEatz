using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using DetroitEatz.Models;
using DetroitEatz.DAL;
using System.Net.Http;
using System.Net;
using System.Web.Mvc;
using System.Data;


namespace DetroitEatz.Controllers
{
    public class HomeController : Controller
    {
        RestaurantContext db = new RestaurantContext();
        RestaurantContext db2 = new RestaurantContext();
        //public IEnumerable<Restaurant> Index(string foodChoice, string searchString)
        public ActionResult Index(string foodChoice, string searchString)
        {

            

            //Use DBContext

            List<string> Foods = new List<string>() { "All", "African", "All You Can Eat", "Bangladeshi", "barbecue", "breakfast", "brunch", 
                "buffet", "burgers", "Cantonese", "Carribean", "chicken", "Chinese", "Coney Island", 
                "Creole", "fish", "gyros", "hotdogs", "Indian", "Italian", "Japanese", "Korean", "Lebanese", 
                "Mediterranean", "Mexican", "Morroccan", "Nigerian", "organic", "Pakistani", "pizza", "ribs", "salad", "seafood", 
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


//            string APIKey = "AIzaSyA5_ZOFKsL19JZR2mDEKcEy6MB6CMwnKlg";
//            // GET /api/places           

//            List<Restaurant> places = new List<Restaurant>();

//            using (WebClient client = new WebClient())
//            {
//                string requestURL = string.Format("https://maps.googleapis.com/maps/api/place/search/json?key={0}&location={1}&rankby=distance&types=cafe|bakery&sensor=false", APIKey, centerPoint.Latitude + "," + centerPoint.Longitude);
//                string results = client.DownloadString(requestURL);
//                var placesResponse = System.Web.Helpers.Json.Decode(results);
//                foreach (var result in placesResponse.results)
//                {
//                    places.Add(new Restaurant()
//                    {
//                        Name = result.name,
//                        Rating = Convert.ToString(result.rating),
//                        Latitude = Convert.ToDouble(result.geometry.location.lat),
//                        Longitude = Convert.ToDouble(result.geometry.location.lng)
//                    });
//                }
//                return places;
//            }
//        }
//    }
//}
           // Creating Restaurant Tables

           
                //var restaurantList = new List<string>();
            

                var restaurants =  from r in db.Restaurants
                                   select r;

                restaurants = restaurants.OrderBy(s => s.PriceLevel);
                //restaurantList.AddRange(restaurants.Distinct().ToList().ToString());

                //ViewBag.Restaurants = 
                //eturn View(restaurantList);
            return View(restaurants.ToList());
                //return Json(users, JsonRequestBehavior.AllowGet); 
            //return View();
        }



        //public JsonResult GetRestaurantDetails()  
        //{  
        //  return Json(restaurantList, JsonRequestBehavior.AllowGet);  
        //}



        //[HttpPost]
        //public JsonResult Create(string places)
        //{
        //    var js = new JavaScriptSerializer();
        //    Restaurant[] restaurants = js.Deserialize<Restaurant[]>(places);
        //    restaurantList = restaurants.ToList();
        //    return Json("Restaurant Details Are Updated"); //dummy example, just serialize back the received Person object
        //}

    }
}
