using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace WeatherInformation.Utlity
{
    public class ErrorLog
    {

        public ErrorLog()
        {

        }

        public void LogError(Exception ex, string extrainfo,bool isclear=true)
        {

            var innerexception = ex.GetOriginalException();
            var nameofassembly = GetWebEntryAssembly();

            if (innerexception == null)
            {
                innerexception = ex;
            }

                    //Here we able to do email,text file that we want
            if (isclear)
            {
                HttpContext.Current.Server.ClearError();
            }
        }

        public void LogError(Exception ex)
        {
            LogError(ex, "");
        }

       

        private string GetStackTrace(Exception e)
        {
            Exception ex = e;
            var stacktrace = new StringBuilder();
            while (ex != null)
            {
                stacktrace.AppendFormat("{0}<br/>", ex.StackTrace);
                ex = ex.InnerException;
            }

            return stacktrace.ToString();
        }

        public Exception GetInnerException(Exception ex)
        {

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            return ex;
        }



        private string GetWebEntryAssembly()
        {
            if (HttpContext.Current == null ||
                HttpContext.Current.ApplicationInstance == null)
            {
                return "";
            }

            var type = HttpContext.Current.ApplicationInstance.GetType();
            while (type != null && type.Namespace == "ASP")
            {
                type = type.BaseType;
            }

            return type == null ? "" : type.Assembly.GetName().Name;
        }

       
    }
    public static class InnerExceptionHelper
    {
        public static Exception GetOriginalException(this Exception ex)
        {
            if (ex.InnerException == null) return ex;

            return ex.InnerException.GetOriginalException();
        }
    }
}

