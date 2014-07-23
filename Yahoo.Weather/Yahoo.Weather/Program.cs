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
using WeatherInformation.BusinessLogicLayer;
using WeatherInformation.BussinessObjectLayer;
using WeatherInformation.Utlity;


namespace WeatherInformation
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
        private readonly BLLData _bllData = new BLLData();

        /// <summary>
        /// Get Weather Information
        /// </summary>
        public void GetWeatherInformation()
        {
            Console.WriteLine("Welcome Guest");

            var yahooWeather = new List<YahooWeatherRssItem>();

            //Send Request and Get Json
            var stateswithotherinfo = RequestData(PrepareRequestEndPoint("StatesEndPoint"));

            var statesListJson = new JsonDeserializer<StatesOrCity>();
            //Deserialize Json
            var statesList = statesListJson.DeSerialize(stateswithotherinfo);
            Console.WriteLine("States Loading Complete");


            Console.WriteLine("City Loading Start");
            if (statesList.Places.IsNotNull() && statesList.Places.Place.IsNotNull())
            {
                //Get only States from List 
                var states = statesList.Places.Place.Where(x => x.PlaceTypeName == "State").ToList();

                foreach (var state in states)
                {
                   
                        //Check State in DB .if state not exists in db then save in db
                        var stateId = _bllData.IsStateExists(state.Name, state.Woeid);

                        if (stateId <= 0)
                        {
                            stateId = _bllData.SaveState(state);
                        }

                        //Send Request and Get Json
                        var cityjson = RequestData(PrepareRequestEndPoint("CityEndPoint", state.Woeid));

                        var deserializecitylist = new JsonDeserializer<StatesOrCity>();
                        //Deserialize Json
                        var citylist = deserializecitylist.DeSerialize(cityjson);

                        Console.WriteLine("{0} City Loading Complete", state.Name);

                        //Get only Districts
                        var cities = citylist.Places.Place.Where(x => x.PlaceTypeName == "District").ToList();
                        foreach (var city in cities)
                        {
                            //Check City in DB .if city not exists in db then save in db
                            int cityId = _bllData.IsStateExists(state.Name, state.Woeid);
                            if (cityId <= 0)
                            {
                                cityId = _bllData.SaveCity(state, stateId);
                            }

                            //Send rssfeed request
                            var rssXml = XDocument.Load(PrepareRequestEndPoint("rssFeedEndPoint", city.Woeid));

                            const string ns = "http://xml.weather.yahoo.com/ns/rss/1.0";

                            var feeds = rssXml.Descendants("item").Select(feed => new YahooWeatherRssItem
                            {
                                Title = feed.Element("title").IsNotNull() ? feed.Element("title").Value : " ",
                                Link = feed.Element("link").IsNotNull() ? feed.Element("link").Value : " ",
                                Description =
                                    feed.Element("description").IsNotNull() ? feed.Element("description").Value : " ",
                                temp =
                                    feed.Element(ns + "condition").IsNotNull()
                                        ? feed.Element(ns + "condition").Attribute("temp").Value
                                        : " ",
                                Date =
                                    feed.Element(ns + "condition").IsNotNull()
                                        ? feed.Element(ns + "condition").Attribute("date").Value
                                        : " ",
                                City = city.Name
                                //like above line, you can get other items 
                            });

                            try
                            {
                                var yahooWeatherRssItems = feeds as IList<YahooWeatherRssItem> ?? feeds.ToList();

                                var weatherInfo = new YahooWeatherRssItem
                                {
                                    Title = yahooWeatherRssItems.Select(x => x.Title).FirstOrDefault(),
                                    Link = yahooWeatherRssItems.Select(x => x.Link).FirstOrDefault(),
                                    Description = yahooWeatherRssItems.Select(x => x.Description).FirstOrDefault(),
                                    temp = yahooWeatherRssItems.Select(x => x.temp).FirstOrDefault(),
                                    Date = yahooWeatherRssItems.Select(x => x.Date).FirstOrDefault(),
                                    City = city.Name
                                };
                                yahooWeather.Add(weatherInfo);

                                _bllData.SaveWeatherInfo(weatherInfo, cityId);
                            }
                            catch (Exception)
                            {


                            }


                        }
                 

                }

                using (var csv = new CsvWriter(new StreamWriter("Weatherinfo.csv")))
                {
                    csv.WriteRecords(yahooWeather);
                }
                Console.WriteLine("Your CSV File Save Successfully ");

            }
            Console.ReadLine();

        }
        /// <summary>
        /// Prepare reuqtest end point
        /// </summary>
        /// <param name="request">api end point</param>
        /// <param name="woeid">woeid for api end point</param>
        /// <returns></returns>
        public string PrepareRequestEndPoint(string request, int woeid = 0)
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
        /// <summary>
        /// send Request to yahoo using api and get data in json fromat 
        /// </summary>
        /// <param name="requestEndPoint">reauest api end point</param>
        /// <returns>json in string fromat</returns>
        public string RequestData(string requestEndPoint)
        {
            var wc = new WebClient();
            var jsonData = wc.DownloadString(requestEndPoint);
            return jsonData;
        }


    }

}
