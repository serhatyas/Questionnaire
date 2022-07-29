using Newtonsoft.Json;
using Questionnaire.Models.ServiceModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Questionnaire.Helpers
{
    public class IpAddressManager
    {
        public string GetClientIp()
        {
            var ipAddress = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            else if (HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"] != null && HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"].Length != 0)
                ipAddress = HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"];
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
                ipAddress = HttpContext.Current.Request.UserHostName;
            if (ipAddress == "::1" || ipAddress == "127.0.0.1")
                ipAddress = "212.133.233.6";
            return ipAddress;
        }
        public IpAddressVM GetCountry(string ip)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://ip-api.com/json/" + ip);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"user\":\"test\"," +
                              "\"password\":\"bla\"}";

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                IpAddressVM reseliazeIpAddressVM = JsonConvert.DeserializeObject<IpAddressVM>(streamReader.ReadToEnd());
                return reseliazeIpAddressVM;
            }
        }
    }
}