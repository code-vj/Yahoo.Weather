using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Linq;
using CsvHelper;
using weatherinformation.BusinessLogicLayer;
using weatherinformation.BussinessObjectLayer;


namespace weatherinformation
{
    class Program
    {
        static void Main(string[] args)
        {
            var weatherInformation = new WeatherInformation();
            weatherInformation.GetWeatherInformation();
        }


    }

    public class WeatherInformation
    {
        private BLLData bllData = new BLLData();
        public void GetWeatherInformation()
        {
            Console.WriteLine("Welcome Guest");

            var wc = new WebClient();

            var yahooweather = new List<YahooWeatherRssItem>();
            var stateswithotherinfo = wc.DownloadString(PrepareSatesRequest("StatesEndPoint"));
            var serializer = new JavaScriptSerializer();
            var statesList = serializer.Deserialize<StatesOrCity>(stateswithotherinfo);
            //Fillter States
            
            Console.WriteLine("States Loading Complete");
            Console.WriteLine("City Loading Start");
            if (statesList.Places != null && statesList.Places.Place != null)
            {
             var states =  statesList.Places.Place.Where(x => x.PlaceTypeName == "State").ToList();
             foreach (var state in states)
                {
                    try
                    {
                        int stateId = bllData.IsStateExists(state.Name, state.Woeid);
                        if (stateId <= 0)
                        {
                            stateId = bllData.SaveState(state);
                        }
                        var cityjsonlist = wc.DownloadString(PrepareSatesRequest("CityEndPoint", state.Woeid));

                        var citylist = serializer.Deserialize<StatesOrCity>(cityjsonlist);
                        Console.WriteLine(state.Name + " City Loading Complete");

                        //Fillter Districts
                     var cities = citylist.Places.Place.Where(x => x.PlaceTypeName == "District").ToList();
                     foreach (var city in cities)
                        {

                            int cityId = bllData.IsStateExists(state.Name, state.Woeid);
                            if (cityId <= 0)
                            {
                                cityId = bllData.SaveCity(state, stateId);
                            }
                            XDocument rssXml = XDocument.Load(PrepareSatesRequest("rssFeedEndPoint", city.Woeid));
                            XNamespace ns = "http://xml.weather.yahoo.com/ns/rss/1.0";
                            var feeds = from feed in rssXml.Descendants("item")
                                        select new YahooWeatherRssItem
                                        {

                                            Title = feed.Element("title").Value,
                                            Link = feed.Element("link").Value,
                                            Description = feed.Element("description").Value,
                                            temp = feed.Element(ns + "condition").Attribute("temp").Value,
                                            Date = feed.Element(ns + "condition").Attribute("date").Value,
                                            City = city.Name
                                            //like above line, you can get other items 
                                        };

                            try
                            {
                                var weatherInfo = new YahooWeatherRssItem()
                                {
                                    Title = feeds.Select(x => x.Title).FirstOrDefault(),
                                    Link = feeds.Select(x => x.Link).FirstOrDefault(),
                                    Description = feeds.Select(x => x.Description).FirstOrDefault(),
                                    temp = feeds.Select(x => x.temp).FirstOrDefault(),
                                    Date = feeds.Select(x => x.Date).FirstOrDefault(),
                                    City = city.Name
                                };
                                yahooweather.Add(weatherInfo);

                                bllData.SaveWeatherInfo(weatherInfo, cityId);
                            }
                            catch (Exception)
                            {


                            }


                        }
                    }
                    catch (Exception)
                    {


                    }

                }

                using (var csv = new CsvWriter(new StreamWriter("Weatherinfo.csv")))
                {
                    csv.WriteRecords(yahooweather);
                }
                Console.WriteLine("Your CSV File Save Successfully ");

            }
            Console.ReadLine();

        }

        public string PrepareSatesRequest(string request, int woeid = 0)
        {
            string endPoint = ConfigurationManager.AppSettings[request];
            string appId = ConfigurationManager.AppSettings["AppId"];
            if (woeid > 0)
            {
                endPoint = endPoint.Replace("#woeid", woeid.ToString());
                if (request == "rssFeedEndPoint")
                {
                    endPoint = endPoint.Replace("&amp;", "&");
                    return endPoint;
                }
            }

            string requestendpoint = endPoint + "?appid=" + appId + "&format=json";
            return requestendpoint;
        }
    }

}
