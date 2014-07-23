using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherInformation.Utlity
{
    public static class ObjectExtensions
    {
        public static bool IsNotNull(this object Value)
        {
            return Value != null;
        }
    }
}
