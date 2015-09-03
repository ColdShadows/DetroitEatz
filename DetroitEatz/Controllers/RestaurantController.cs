using System;
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
using System.Data.Entity;
using Google.GData.Client;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace DetroitEatz.Controllers
{
    public class RestaurantController : ApiController
    {
        string mapkey = "AIzaSyDC6t7o26kX3LTfxraVFebdSXG-cF7wcGo";
        string geokey = "AIzaSyBvYhRMHuDQUfyIv_HlVaMZXbxs8L5ZPko";

        private RestaurantContext db = new RestaurantContext();

       //public async Task<IHttpActionResult> getRestaurants(List<Restaurant> restaurants)
       // {
       //    List<Restaurant> validRestaurants = new List<Restaurant>();

       //    foreach(Restaurant r in restaurants)
       //        if(ModelState.IsValid)
       //        {
       //            validRestaurants.Add(r);
       //        }

           



       //    return (validRestaurants);
       // }

        //Coordinate
        public struct Coordinate
        {
            private double lat;
            private double lon;

            public Coordinate( double latitude, double longitude)
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
            
            using(var client = new WebClient())
            {



                string uri = "https://maps.googleapis.com/maps/api/geocode/json?address=" + searchPlace.ToString() + "&components=administrative_components:MI|country:US&key=" + geokey;

                string results = client.DownloadString(uri);

                JavaScriptSerializer js = new JavaScriptSerializer();
                
               
                

                return new Coordinate(Convert.ToDouble(jsonResults), Convert.ToDouble(jsonResults));

            }

            

        }

        //Get
        [Route( "Home/api/GetRestaurants/{place}")]
        [HttpGet]
        public IEnumerable<Restaurant> GetRestaurants(string place)
       {
           using (WebClient client = new WebClient())
           {

               double lat = 42.331427;
               double lon = -83.0457538;

               List<Restaurant> restaurants = new List<Restaurant>();
               
                   Coordinate coords = getCoordinate(place);

                   lat = coords.Latitude;
                   lon = coords.Longitude;
                  
               
               double radius = 2000;
               string uri = "https://maps.googleapis.com/maps/api/place/radarsearch/json?";

               uri += "key=" + mapkey + "&";
               uri += "location=" + lat.ToString() + "," + lon.ToString() + "&";
               uri += "radius=" + radius.ToString() + "&";
               uri += "types=restaurant";

               string detailUri = "https://maps.googleapis.com/maps/api/place/details/json?";


               string results = client.DownloadString(uri);

               var placesResponse = System.Web.Helpers.Json.Decode(results);

               foreach( var result in placesResponse.results)
               {

                   detailUri += "placeid=" + result.placeId + "&key=" + mapkey;

                   string details = client.DownloadString(detailUri);

                   var detailsResponse = System.Web.Helpers.Json.Decode(details);

                   foreach( var detail in detailsResponse.results)
                   {

                       restaurants.Add(new Restaurant()
                           {
                               Name = detail.name,
                               PlaceID = detail.place_id,
                               AddressNumber = detail.formatted_address,
                               PhoneNumber = detail.formatted_phone_number,
                               Rating = detail.rating,
                               WebSite = detail.website
                           });

                   }
                   

                       
                       
                       
                  

               }





               return restaurants;


               //WebRequest request = WebRequest.Create(uri);
               //request.Method = "GET";
               //request.ContentType = "application/json";
               //request.GetResponse();
           }
       }


        public IEnumerable<Favorite> getFavorites()
        {
            
            string user = User.Identity.GetUserId();


            var favoriteslist = from f in db.Favorites
                            where f.UserID == user
                            select f; 
                                


                return favoriteslist;

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
