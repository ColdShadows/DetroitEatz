using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetroitEats.Models
{
    public class User
    {

        string userName;
        string password;

        public int UserID { get; set; }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public virtual ICollection<Restaurant> Restaurants { get; set; }

    }
}
