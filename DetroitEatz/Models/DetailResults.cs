using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetroitEatz.Models
{
    public class DetailResponse
    {
        public string Status {get;set;}
        public DetailResult Results { get; set; }
    }
    public class DetailResult
    {
        public string Name { get; set; }
        public string Website { get; set; }
        public double Rating { get; set; }
        public string Formatted_Address { get; set; }
        public string Formatted_Phone_Number { get; set; }
        public DetailAddress_Components[] Adress_Components { get; set; }
    }
    public class DetailAddress_Components
    {
        public string[] Types { get; set; }
        public string Long_Name { get; set; }
        public string Short_Name { get; set; }

    }
    public class DetailGeometry
    {
        public DetailLocation Location { get; set; }
    }
    public class DetailLocation
    {
        public string Lat {get;set;}
        public string Lon {get;set;}
    }
}
