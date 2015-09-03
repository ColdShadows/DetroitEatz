using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetroitEatz.Models
{
    
    public class GeoLocation
    {
        public decimal Lat { get; set; }
 
        public decimal Lng { get; set; }
    }
 
    public class GeoGeometry
    {
        public GeoLocation Location { get; set; }
    }
 
    public class GeoResult
    {
        public GeoGeometry Geometry { get; set; }
        public GeoAddress_Component[] Address_Components {get;set;}
    }
 
    public class GeoResponse
    {
        public string Status { get; set; }
 
        public GeoResult[] Results { get; set; }
    }
    public class GeoAddress_Component
    {
        public string Long_Name { get; set; }
        public string Short_Name { get; set; }
        public List<string> Types { get; set; }
    }
}
