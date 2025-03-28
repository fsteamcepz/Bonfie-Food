using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonfieFood
{
    public class CityResponse
    {
        public const string CitiesAPIUrl = "http://api.geonames.org/searchJSON?country={0}&featureClass=P&maxRows=1000&username=bonfie_food";
        [JsonProperty("geonames")]
        public List<CityInfo> Geonames { get; set; }
    }

    public class CityInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}