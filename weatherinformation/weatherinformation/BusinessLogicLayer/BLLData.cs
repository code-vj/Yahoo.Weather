using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using weatherinformation.BussinessObjectLayer;
using weatherinformation.DataAccessLayer;
using weatherinformation.Utlity;

namespace weatherinformation.BusinessLogicLayer
{
    public class BLLData
    {
        DataAccess dateAccess = new DataAccess();

        public int SaveState(Place statesOrCities)
        {
           return dateAccess.SaveState(statesOrCities);
        }

        public int IsStateExists(string name, int woeid)
        {
            return dateAccess.IsStateExists(name, woeid);
        }
        public int SaveCity(Place statesOrCities,int stateId)
        {
            return dateAccess.SaveCity(statesOrCities, stateId);
        }

        public int IsCityExists(string name, int woeid, int stateId)
        {
            return dateAccess.IsCityExists(name, woeid, stateId);
        }

        public int SaveWeatherInfo(YahooWeatherRssItem yahooWeatherRssItem, int cityId)
        {
            return dateAccess.SaveWeatherInfo(yahooWeatherRssItem, cityId);
        }
    }
}
