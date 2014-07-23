using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeatherInformation.BussinessObjectLayer;
using WeatherInformation.DataAccessLayer;
using WeatherInformation.Utlity;

namespace WeatherInformation.BusinessLogicLayer
{
    public class BLLData
    {
        readonly DataAccess _dateAccess = new DataAccess();

        public int SaveState(Place statesOrCities)
        {
           return _dateAccess.SaveState(statesOrCities);
        }

        public int IsStateExists(string name, int woeid)
        {
            return _dateAccess.IsStateExists(name, woeid);
        }
        public int SaveCity(Place statesOrCities,int stateId)
        {
            return _dateAccess.SaveCity(statesOrCities, stateId);
        }

        public int IsCityExists(string name, int woeid, int stateId)
        {
            return _dateAccess.IsCityExists(name, woeid, stateId);
        }

        public int SaveWeatherInfo(YahooWeatherRssItem yahooWeatherRssItem, int cityId)
        {
            return _dateAccess.SaveWeatherInfo(yahooWeatherRssItem, cityId);
        }
    }
}
