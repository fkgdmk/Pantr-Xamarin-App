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
            UserViewModelTest authenticated = null;
            var controllerName = "login";
            //var basicClientApi = string.Format("http://10.0.2.2:50001/api/{0}", controllerName);
            var basicClientApi = string.Format(IService.basicApi+ "{0}", controllerName);
            try
            {
                //Bruger "using" så httpclient bliver disposed automatisk når det ikke bruges
                using (var httpClient = new HttpClient())
                {
                    //serializer jobject til json og initialiserer et nyt stringcontent object
                    //som sendes med som content til API
                    var json = JsonConvert.SerializeObject(login); 
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    //Her foretages API kaldet med url og content
                    var response = await httpClient.PostAsync(basicClientApi, content);

                    //hvis statuscode er alt andet en OK så skal det håndteres
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Error");
                    }
                    else
                    {
                        //læser data fra API(som er en en string(jobject)) ind i en variable
                        var rawResponse = await response.Content.ReadAsStringAsync();

                        //parser det til et jobject
                        JObject o = JObject.Parse(rawResponse);

                        //deserializer til en viewmodel/eller hvad vi nu kalder det
                        authenticated = JsonConvert.DeserializeObject<UserViewModelTest>(o.ToString());
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return authenticated;
        }

    }
}
