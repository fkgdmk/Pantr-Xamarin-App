using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pantr.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Pantr.DB
{
    class TransactionService
    {
        //Metode som returnerer en observablecollection(liste) af ens egne reservationer
        public async Task<ObservableCollection<PostViewModelCopy>> GetOwnReservations(int id)
        {
            ObservableCollection<PostViewModelCopy> fromDb = new ObservableCollection<PostViewModelCopy>();

            var controllerName = "transaction/users";
            var basicClientApi = string.Format("http://10.0.2.2:50001/api/{0}/{1}", controllerName, id);
            try
            {
                //bruger using så resourcer blive disposed med det samme
                using (var httpClient = new HttpClient())
                {
                    //api kald
                    var response = await httpClient.GetAsync(basicClientApi);

                    //hvis statuscode ikke er OK
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode);
                    }
                    else
                    {
                        //læser data fra api
                        var rawResponse = await response.Content.ReadAsStringAsync();

                        //deserializer til en liste(observablecollection<postviewmodel)
                        fromDb = JsonConvert.DeserializeObject<ObservableCollection<PostViewModelCopy>>(rawResponse.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return fromDb;
        }

        //metode som får post- og panterid og annullere en transaction og frigiver en post
        //metoden returnerer true hvis den blev annuleret eller false hvis det ikke var muligt
        public async Task<bool> CancelReservation(JObject postAndPanterId)
        {
            bool success = false;
            var controllerName = "transaction";
            var basicClientApi = string.Format("http://10.0.2.2:50001/api/{0}", controllerName);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    //pakker post og panter id i et json som bruges i apiet til at finde den rigtige reservation/transaction
                    var json = JsonConvert.SerializeObject(postAndPanterId);

                    //sætter content, encoding og contentType
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    //api kald
                    var response = await httpClient.PutAsync(basicClientApi, content);

                    //hvis statuscode ikke er OK
                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode);
                    }
                    else
                    {
                        success = true;
                    }
                }
            } catch(Exception e)
            {

            }
            return success;
        }
    }
}
