using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web.UI;
using System.Configuration;

namespace CardHolder.Utility
{
   public  class Helper
    {

        public string RandomDigits()
        {
            int length = 3;
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s + DateTime.Now.ToString("mmssFFF");
        }

        public string GetResponse(string Request)
       {
           HttpWebResponse response = null;
           var result = string.Empty;
           try
           {
               string jettyUrl = ConfigurationManager.AppSettings["JettyServerUrl"];
               var httpWebRequest = (HttpWebRequest)WebRequest.Create(jettyUrl);
               httpWebRequest.ContentType = "application/json";
               httpWebRequest.Method = "POST";
               using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
               {
                   streamWriter.Write(Request);
               }
               response = (HttpWebResponse)httpWebRequest.GetResponse();
               using (var streamReader = new StreamReader(response.GetResponseStream()))
               {
                   result = streamReader.ReadToEnd();
               }               
           }

           catch(Exception ex)
           {
              
                 return null;
           }

           return result;
       }


    }
}
