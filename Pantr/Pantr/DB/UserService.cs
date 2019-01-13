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

        //Metode som forsøger at registrerer en bruge og returnerer en boolean alt efter om det gik godt eller dårligt
        public async Task<bool> RegisterUser(JObject registerUser)
        {
            bool registered = false;

            var controllerName = "users";
            //var basicClientApi = string.Format("http://10.0.2.2:50001/api/{0}", controllerName);
            var basicClientApi = string.Format(IService.basicApi + "{0}", controllerName);

            try
            {
                //bruger using så resourcerne bliver disposed med det samme
                using (var httpClient = new HttpClient())
                {
                    //serializer jobjectet fra viewet til json
                    var json = JsonConvert.SerializeObject(registerUser);

                    //sætter content, encoding og contenttype så det er klart til apikaldet
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    //foretager api kaldet
                    var response = await httpClient.PostAsync(basicClientApi, content);

                    //sætter registered til true hvis alt går godt
                    //skal håndteres hvis der returneres noget andet end statuscode ok
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode);
                    }
                    else
                    {
                        registered = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return registered;
        }
    }
}
