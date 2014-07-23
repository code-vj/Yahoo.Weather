using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeatherInformation.Utlity
{
    public static class StringExtensions
    {
        public static bool IsNotNullOrEmpty(this string Value)
        {
            return !(string.IsNullOrEmpty(Value));
        }
    }
}
