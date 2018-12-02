using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using Pantr.Models;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace Pantr.DB
{
    class UserService
    {
        public async Task<bool> RegisterUser(JObject registerUser)
        //public async Task<UserViewModelTest> RegisterUser(UserViewModelTest registerUser)
        {
            //UserViewModelTest registeredUser = null;
            bool registered = false;
            var controllerName = "users";
            var basicClientApi = string.Format("http://10.0.2.2:50001/api/{0}", controllerName);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(registerUser);
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
                        //registeredUser = JsonConvert.DeserializeObject<UserViewModelTest>(o.ToString());
                        registered = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return registered;
            //return registeredUser;
        }
    }
}
