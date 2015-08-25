using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
namespace DetroitEatz.Models
{
   public class Favorite
    {
      // [Key]
      // [Required]
        public int FavoriteID {get;set;}
        public string UserID { get; set; }
        public string PlaceID { get; set; }
        public string RestaurantName { get; set; }

        public virtual ICollection<Restaurant> FavoriteRestaurants { get; set; }
    }
}
