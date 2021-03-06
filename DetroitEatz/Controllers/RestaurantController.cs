﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DetroitEatz.Models;
using DetroitEatz.DAL;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Google.GData.Client;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace DetroitEatz.Controllers
{
    public class RestaurantController : ApiController
    {
        string TravisKey = "AIzaSyDC6t7o26kX3LTfxraVFebdSXG-cF7wcGo";
        string KevKey = "AIzaSyA5_ZOFKsL19JZR2mDEKcEy6MB6CMwnKlg";
        
        private RestaurantContext db = new RestaurantContext();

       
        //Coordinate
        public struct Coordinate
        {
            private double lat;
            private double lon;

            public Coordinate(double latitude, double longitude)
            {
                lat = latitude;
                lon = longitude;

            }

            public double Latitude { get { return lat; } set { lat = value; } }
            public double Longitude { get { return lon; } set { lon = value; } }

        }

        //Get Coordinates
        public Coordinate getCoordinate(string searchPlace)
        {

            using (var client = new WebClient())
            {



                string uri = "https://maps.googleapis.com/maps/api/geocode/json?address=" + searchPlace.ToString() + "&key=" + TravisKey;

                string results = client.DownloadString(uri);

                JavaScriptSerializer js = new JavaScriptSerializer();


                GeoResponse georesults = js.Deserialize<GeoResponse>(results);


                if (georesults.Status == "OK")
                {


                    return new Coordinate(Convert.ToDouble(georesults.Results[0].Geometry.Location.Lat), Convert.ToDouble(georesults.Results[0].Geometry.Location.Lng));


                }
                else
                {


                    return new Coordinate(42.3347, -83.0497);
                }








            }



        }

        //Get
        [Route("Home/api/GetRestaurants/{place}/{rad}")]
        [HttpGet]
        public IEnumerable<Restaurant> GetRestaurants(string place, double rad)
        {
            using (WebClient client = new WebClient())
            {

                double lat = 42.331427;
                double lon = -83.0457538;

                List<Restaurant> restaurants = new List<Restaurant>();

                Coordinate coords = getCoordinate(place);

                lat = coords.Latitude;
                lon = coords.Longitude;


                double radius = rad;
                string uri = "https://maps.googleapis.com/maps/api/place/radarsearch/json?";
                    //"https://maps.googleapis.com/maps/api/place/nearbysearch/json?";
                uri += "key=" + TravisKey + "&";
                uri += "location=" + lat.ToString() + "," + lon.ToString() + "&";
                uri += "radius=" + radius.ToString() + "&";
                //uri += "rankby=distance&";
                uri += "types=restaurant";
                //uri += "&keyword=food";
                //uri += "&name=''";
                

                string detailUri = "https://maps.googleapis.com/maps/api/place/details/json?";


                string results = client.DownloadString(uri);

                JavaScriptSerializer js = new JavaScriptSerializer();

                PlacesResponse placesresults = js.Deserialize<PlacesResponse>(results);


                if (placesresults.Results.Length < 30)
                {
                    for (int i = 0; i < placesresults.Results.Length; i++)
                    {
                        if (placesresults.Status == "OK")
                        {
                            detailUri = "https://maps.googleapis.com/maps/api/place/details/json?";
                            detailUri += "placeid=" + placesresults.Results[i].Place_Id + "&key=" + TravisKey;

                            string details = client.DownloadString(detailUri);



                            RootObject detailresults = js.Deserialize<RootObject>(details);

                            if (detailresults.status == "OK")
                            {
                                restaurants.Add(new Restaurant()
                                    {
                                        Name = detailresults.result.name,
                                        PlaceID = detailresults.result.place_id,
                                        AddressNumber = detailresults.result.formatted_address,
                                        PhoneNumber = detailresults.result.formatted_phone_number,
                                        Rating = detailresults.result.rating.ToString(),
                                        WebSite = detailresults.result.website,
                                        Lat = detailresults.result.geometry.location.lat,
                                        Lon = detailresults.result.geometry.location.lng,
                                        PriceLevel = detailresults.result.price_level.ToString()
                                        
                                    });

                            }

                        }





                    }
                }
                else
                {
                    for (int i = 0; i <= 30; i++)
                    {
                        if (placesresults.Status == "OK")
                        {
                            detailUri = "https://maps.googleapis.com/maps/api/place/details/json?";
                            detailUri += "placeid=" + placesresults.Results[i].Place_Id + "&key=" + TravisKey;

                            string details = client.DownloadString(detailUri);



                            RootObject detailresults = js.Deserialize<RootObject>(details);

                            if (detailresults.status == "OK")
                            {
                                restaurants.Add(new Restaurant()
                                {
                                    Name = detailresults.result.name,
                                    PlaceID = detailresults.result.place_id,
                                    AddressNumber = detailresults.result.formatted_address,
                                    PhoneNumber = detailresults.result.formatted_phone_number,
                                    Rating = detailresults.result.rating.ToString(),
                                    WebSite = detailresults.result.website,
                                    Lat = detailresults.result.geometry.location.lat,
                                    Lon = detailresults.result.geometry.location.lng,                                                               
                                    PriceLevel = detailresults.result.price_level.ToString()
                                    
                                });

                            }

                        }





                    }
                }


                


                return restaurants;



            }
        }

        [Route("Home/api/GetFavorites")]
        [HttpGet]
        public IEnumerable<Favorite> getFavorites()
        {

            string user = User.Identity.Name;

            var favorites = from f in db.Favorites
                                where f.UserID == user
                                select f;
            List<Favorite> favoritesList = new List<Favorite>();
            favoritesList = favorites.ToList();


            return favoritesList;

        }


        [ResponseType(typeof(Restaurant))]
        [Route("Home/api/AddToFavorites")]
        [HttpPost]
        public Favorite AddToFavorites(Restaurant restaurant)
        {
           
             Favorite newFav = new Favorite
            {
                PlaceID = restaurant.PlaceID,
                UserID = User.Identity.Name,
                Lat = restaurant.Lat,
                Lon = restaurant.Lon,
                RestaurantName = restaurant.Name,
                Address = restaurant.AddressNumber,
                PhoneNumber = restaurant.PhoneNumber


            };

            if(ModelState.IsValid)
            {
                db.Favorites.Add(newFav);
                db.SaveChanges();
            }


             return newFav;
            //return CreatedAtRoute("DefaultApi", new { id = restaurant.RestaurantID }, restaurant);
        }


        [ResponseType(typeof(Favorite))]
        [Route("Home/api/RemoveFromFavorites")]
        [HttpPost]
        public void RemoveFromFavorites(Favorite deleteFav)
        {



            if (ModelState.IsValid)
            {
                var deleteQry = from f in db.Favorites
                                where f.PlaceID == deleteFav.PlaceID && f.UserID == deleteFav.UserID
                                select f;
                var deleteList = deleteQry.ToList();

                db.Favorites.RemoveRange(deleteList);
                db.SaveChanges();
            }




        }




        //public IEnumerable<Interested> getFavorites()
        //{

        //    string user = User.Identity.GetUserId();


        //    var interestedlist = from i in db.Interested
        //                        where i.UserID == user
        //                        select i;



        //    return interestedlist;

        //}
    }
}
