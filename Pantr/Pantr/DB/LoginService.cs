using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pantr.Models;

namespace Pantr.DB
{
    class LoginService
    {

        private string Uri = "http://94.18.243.144:50001/api/login";



        public async Task Login(LoginViewModel login)
        {
            var controllerName = "login";
            var basicClientApi = string.Format("http://94.18.243.144:50001/api/{0}", controllerName);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(login); // no need to call `JObject.FromObject`
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await httpClient.PostAsync(basicClientApi, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode);
                    }
                    else
                    {
                        var rawResponse = await response.Content.ReadAsStringAsync();

                        JObject o = JObject.Parse(rawResponse);
                        Console.WriteLine(o.ToString());
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            
        }

    }
}
