using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http;
using System.Web;
using System.IO;

namespace Application.Presentation
{
    public static class ObjectFactory
    {
        public static string GetStringAsync(string theAddress, string theParameter, string theController)
        {
            string theUrl = ConfigurationManager.AppSettings["BaseAddress"].ToString() + theAddress;
            string theAuthHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}:{2}", ConfigurationManager.AppSettings["ApiUser"].ToString(), ConfigurationManager.AppSettings["ApiUserPwd"].ToString(), theController)));
            if (theParameter != "")
                theUrl += theParameter;
            HttpClient theClient = new HttpClient();
            theClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", theAuthHeader);
            return theClient.GetStringAsync(new Uri(theUrl)).Result;
        }

        public static Stream GetStreamAsync(string theAddress, string theParameter, string theController)
        {
            string theUrl = ConfigurationManager.AppSettings["BaseAddress"].ToString() + theAddress;
            string theAuthHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}:{2}", ConfigurationManager.AppSettings["ApiUser"].ToString(), ConfigurationManager.AppSettings["ApiUserPwd"].ToString(), theController)));
            if (theParameter != "")
                theUrl += theParameter;
            HttpClient theClient = new HttpClient();
            theClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", theAuthHeader);
            return theClient.GetStreamAsync(new Uri(theUrl)).Result;
        }

        public static async Task GetAsync(string theAddress, string theParameter, HttpContext theContext, string theController)
        {
            string theUrl = ConfigurationManager.AppSettings["BaseAddress"].ToString() + theAddress;
            string theAuthHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}:{2}", ConfigurationManager.AppSettings["ApiUser"].ToString(), ConfigurationManager.AppSettings["ApiUserPwd"].ToString(), theController)));
            if (theParameter != "")
                theUrl += theParameter;
            HttpClient theClient = new HttpClient();
            theClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", theAuthHeader);
            var theResponse = await theClient.GetAsync(new Uri(theUrl));
            if (!theResponse.IsSuccessStatusCode)
            {
                throw new ArgumentException("Operation was Unsuccessful. Exception: " + theResponse.ToString(), nameof(theResponse));
            }
        }

        public static HttpResponseMessage PostOnServerAsync(string theAddress, FormUrlEncodedContent theParameter, string theController, HttpContextBase theContext)
        {
            string theUrl = ConfigurationManager.AppSettings["BaseAddress"].ToString() + theAddress;
            string theAuthHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}:{2}", ConfigurationManager.AppSettings["ApiUser"].ToString(), ConfigurationManager.AppSettings["ApiUserPwd"].ToString(), theController)));
            HttpClient theClient = new HttpClient();
            theClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", theAuthHeader);
            var theResponse = theClient.PostAsync(new Uri(theUrl), theParameter).Result;
            if (!theResponse.IsSuccessStatusCode)
            {
                throw new ArgumentException("Operation was Unsuccessful. Exception: " + theResponse.ToString(), nameof(theResponse));
                //theContext.AddError(new Exception(theResponse.StatusCode + " Error. Please try again."));
            }
            return theResponse;
        }

        public static HttpResponseMessage PostOnServerAsyncWithXML(string theAddress, FormUrlEncodedContent theParameter, string XMLData, string theController, HttpContextBase theContext)
        {
            string theUrl = ConfigurationManager.AppSettings["BaseAddress"].ToString() + theAddress;
            string theAuthHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}:{2}", ConfigurationManager.AppSettings["ApiUser"].ToString(), ConfigurationManager.AppSettings["ApiUserPwd"].ToString(), theController)));
            HttpClient theClient = new HttpClient();
            ////Add XML Data////
            MultipartFormDataContent theForm = new MultipartFormDataContent();
            HttpContent theContent = new StringContent(XMLData);
            theForm.Add(theContent, "XMLData");
            theForm.Add(theParameter, "FormUrl");
            ////////////////////
            theClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", theAuthHeader);
            var theResponse = theClient.PostAsync(new Uri(theUrl), theForm).GetAwaiter().GetResult();
            if (!theResponse.IsSuccessStatusCode)
            {
                throw new ArgumentException("Operation was Unsuccessful. Exception: " + theResponse.ToString(), nameof(theResponse));
                //theContext.AddError(new Exception(theResponse.StatusCode + " Error. Please try again."));
            }
            return theResponse;
        }


    }
}
