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
        public async Task<ObservableCollection<PostViewModelCopy>> GetOwnReservations(int id)
        {
            ObservableCollection<PostViewModelCopy> fromDb = new ObservableCollection<PostViewModelCopy>();

            var controllerName = "transaction/users";
            var basicClientApi = string.Format("http://10.0.2.2:50001/api/{0}/{1}", controllerName, id);
            try
            {
                using (var httpClient = new HttpClient())
                {

                    var response = await httpClient.GetAsync(basicClientApi);

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode);
                    }
                    else
                    {
                        var rawResponse = await response.Content.ReadAsStringAsync();

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

        public async void CancelReservation(PostViewModelCopy post)
        {
            var controllerName = "transaction";
            var basicClientApi = string.Format("http://10.0.2.2:50001/api/{0}", controllerName);
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(post);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await httpClient.PutAsync(basicClientApi, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode);
                    }
                    else
                    {

                    }
                }
            } catch(Exception e)
            {

            }
            
        }
    }
}
