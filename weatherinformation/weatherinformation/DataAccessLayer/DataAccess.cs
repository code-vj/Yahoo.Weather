using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using weatherinformation.BussinessObjectLayer;
using weatherinformation.Utlity;

namespace weatherinformation.DataAccessLayer
{
    public class DataAccess
    {
        DbHelper dbhelper = new DbHelper();

        public int SaveState(Place statesOrCities)
        {
            const string q = "insert into States(Name,woeid)values(@Name,@Woeid) SELECT SCOPE_IDENTITY()";
             dbhelper.ExecuteQuery(statesOrCities, q);

            return IsStateExists(statesOrCities.Name, statesOrCities.Woeid);
        }
        public int IsStateExists(string name,int woeid)
        {
            const string q = "select id from states where name=@Name and Woeid=@Woeid";
            return dbhelper.SelectQuery<int>(new { Name = name, Woeid = woeid }, q).FirstOrDefault(); ;
        }
        public int SaveCity(Place statesOrCities,int stateId)
        {
            const string q = "insert into Cities(Name,woeid,state_id)values(@Name,@Woeid,@Stateid) SELECT SCOPE_IDENTITY()";
            return dbhelper.ExecuteQuery(new { Name = statesOrCities.Name, Woeid = statesOrCities.Woeid, Stateid = stateId }, q);

           
        }
        public int IsCityExists(string name, int woeid, int stateId)
        {
            const string q = "select id from Cities where name=@Name and Woeid=@Woeid and state_id=@StateId";
            return dbhelper.SelectQuery<int>(new { Name = name, Woeid = woeid, StateId = stateId }, q).FirstOrDefault(); ;
        }

        public int SaveWeatherInfo(YahooWeatherRssItem yahooWeatherRssItem, int cityId)
        {
            const string q = "insert into WeatherInfo(Title,Description,Date,Temperature,city_id)values(@Title,@Description,@Date,@Temperature,@Cityid) SELECT SCOPE_IDENTITY()";
            return dbhelper.ExecuteQuery(new { Title = yahooWeatherRssItem.Title, Description = yahooWeatherRssItem.Description, Date = yahooWeatherRssItem.Date, Temperature = yahooWeatherRssItem.temp, Cityid = cityId }, q);


        }
    }
}
