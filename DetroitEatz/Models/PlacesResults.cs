using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetroitEatz.Models
{
    public class PlacesResponse
    {
        public string Status {get;set;}
        public PlacesResults[] Results { get; set; }
    }
    public class PlacesResults
    {
        public PlacesGeometry Geometry { get; set; }
        public string Place_Id { get; set; }
        
    }
    public class PlacesGeometry
    {
        PlacesLocation Location {get; set;}
    }
    public class PlacesLocation
    {
        public double Lat {get;set;}
        public double Lon {get;set;}
    }
    
}
