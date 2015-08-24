using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetroitEats.Models
{
    public class Restaurant
    {
        //int restaurantID;
        string name;
        string priceLevel;
        string webSite;
        string rating;
        bool openNow;
        string time;
        string day;
        string street;
        string zip;
        string city;
        string addressNumber;
        string state;
        string phoneNumber;

        public int RestaurantID { get; set; }
        public string Name { get { return name; } set { name = value; } }
        public string PriceLevel { get { return priceLevel; } set { priceLevel = value; } }
        public string WebSite { get { return webSite; } set { webSite = value; } }
        public string Rating { get { return rating; } set { rating = value; } }
        public bool OpenNow { get { return openNow; } set { openNow = value; } }
        public string Time { get { return time; } set { time = value; } }
        public string Day { get { return day; } set { day = value; } }
        public string Street { get { return street; } set { street = value; } }
        public string Zip { get { return zip; } set { zip = value; } }
        public string City { get { return city; } set { city = value; } }
        public string AddressNumber { get { return addressNumber; } set { addressNumber = value; } }
        public string State { get { return state; } set { state = value; } }
        public string PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; } }



        public virtual ICollection<User> Users { get; set; }

    }
}
