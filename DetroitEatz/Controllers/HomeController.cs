﻿using System;
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
        public ActionResult Index(/*List<Restaurant>? unvalidatedRestaurants*/)
        {
            //Use DBContext
           
            
            //Create new Favorite
                //Favorite newFav = new Favorite
                //{

                //    UserID = User.Identity.GetUserName(),
                //    PlaceID = "k",
                //    RestaurantName = "k"

                //};
                //    //Add That value to private DB
                //db2.Favorites.Add(newFav);
                //    //Save those changes from private DB to main DB
                //db2.SaveChanges();
            
                        
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

            //var remove = from r in db.Restaurants
            //             select r;

            //db.Restaurants.RemoveRange(remove);
            //db.SaveChanges();
           //Test of Viewbag
            //List<Restaurant> validRestaurants = new List<Restaurant>();
            //if (ViewBag.ListOfRestaurants != null)
            //{
            //    foreach (Restaurant r in (new List<Restaurant>(ViewBag.ListOfRestaurants)))
            //    {
            //        if (ModelState.IsValid)
            //        {
            //            validRestaurants.Add(r);
            //        }


            //    }
            //    ViewBag.ValidRestaurants = validRestaurants;
            //}  

            //Test of Nullable Parameter
            //List<Restaurant> validRestaurants = new List<Restaurant>();

            //foreach (Restaurant r in unvalidatedRestaurants)
            //{
            //    if (ModelState.IsValid)
            //    {
            //        validRestaurants.Add(r);
            //    }

            //}


            




            //Create List of Fake Favorites

                //var favorites = new List<Favorite>();
                //favorites.Add(new Favorite { PlaceID = "fav1", RestaurantName = "Restaurant 1", UserID = User.Identity.GetUserId() });
                //favorites.Add(new Favorite { PlaceID = "fav2", RestaurantName = "Restaurant 2", UserID = User.Identity.GetUserId() });
                //ViewBag.Favorites = favorites;

                return View();
           


            
        }
         public ActionResult About()
        {

            return View();
        }
         public ActionResult ContactUs()
         {

             return View();
         }
    }
}
