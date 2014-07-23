using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace weatherinformation.BussinessObjectLayer
{
    public class CityInfromation
    {
        [JsonProperty("places")]
        public Places Places { get; set; }
    }


    public class CountryAttrs
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("woeid")]
        public int Woeid { get; set; }
    }

    public class Admin1Attrs
    {
        [JsonProperty("Type")]
        public string Type { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("woeid")]
        public int Woeid { get; set; }
    }

    public class Admin2Attrs
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("woeid")]
        public int Woeid { get; set; }

    }

    public class Centroid
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }

    public class SouthWest
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }

    public class NorthEast
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
    }

    public class BoundingBox
    {
        [JsonProperty("southWest")]
        public SouthWest SouthWest { get; set; }
        [JsonProperty("northEast")]
        public NorthEast NorthEast { get; set; }
    }

    public class TimezoneAttrs
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("woeid")]
        public int Woeid { get; set; }
    }

    public class CityPlace
    {
        [JsonProperty("woeid")]
        public int Woeid { get; set; }
        [JsonProperty("placeTypeName")]
        public string PlaceTypeName { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("admin1")]

        public string Admin1 { get; set; }
        [JsonProperty("admin2")]
        public string Admin2 { get; set; }
        [JsonProperty("admin3")]
        public string Admin3 { get; set; }
        [JsonProperty("locality1")]
        public string Locality1 { get; set; }
        [JsonProperty("locality2")]
        public string Locality2 { get; set; }
        [JsonProperty("postal")]
        public string Postal { get; set; }
        [JsonProperty("centroid")]
        public Centroid Centroid { get; set; }
        [JsonProperty("boundingBox")]
        public BoundingBox BoundingBox { get; set; }
        [JsonProperty("areaRank")]
        public int AreaRank { get; set; }
        [JsonProperty("popRank")]
        public int PopRank { get; set; }
        [JsonProperty("timezone")]
        public string Timezone { get; set; }
        [JsonProperty("uri")]
        public string Uri { get; set; }
        [JsonProperty("lang")]
        public string Lang { get; set; }
    }

    public class YahooWeatherRssItem
    {

        public string City { get; set; }
        public string Title { get; set; }
      
        public string Link { get; set; }
        public string temp { get; set; }

        public string Description { get; set; }
   

        public string Date { get; set; }
        
    }
}
