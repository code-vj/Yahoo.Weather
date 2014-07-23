using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace weatherinformation.BussinessObjectLayer
{
    public class StatesOrCity
    {
        [JsonProperty("places")]
        public Places Places { get; set; }
    }
    public class PlaceTypeNameAttrs
    {
        [JsonProperty("code")]
        public int Code { get; set; }
    }

    public class Place
    {
        [JsonProperty("woeid")]
        public int Woeid { get; set; }
        [JsonProperty("placeTypeName")]
        public string PlaceTypeName { get; set; }
        //   public PlaceTypeNameAttrs __invalid_name__placeTypeName attrs { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("uri")]
        public string Uri { get; set; }
        [JsonProperty("lang")]
        public string Lang { get; set; }
    }

    public class Places
    {
        [JsonProperty("place")]
        public List<Place> Place { get; set; }
        [JsonProperty("start")]
        public int start { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
