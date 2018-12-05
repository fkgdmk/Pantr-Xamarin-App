using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pantr.Models;
using Xamarin.Forms;

namespace Pantr.DB
{
    public class PostService
    {
        public static async void GetAllPosts(ListView listView)
        {

            HttpClient client = new HttpClient();
            IEnumerable<PostViewModel> post = null;

            var uri = new Uri(string.Format("http://10.111.180.139:45457/api/posts"));

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                post = JsonConvert.DeserializeObject<IEnumerable<PostViewModel>>(content);
            }

            TimeSpan.FromMinutes(352345235);
            listView.ItemsSource = post;
        }

        

    }



}

