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
        public async Task<UserViewModelTest> AuthenticateUser(JObject login)
        {
            UserViewModelTest authenticatedUser = null;
            var controllerName = "login";
            var basicClientApi = string.Format("http://10.0.2.2:50001/api/{0}", controllerName);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(login); 
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
                        authenticatedUser = JsonConvert.DeserializeObject<UserViewModelTest>(o.ToString());

                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return authenticatedUser;
        }

    }
}
