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
            var uri = new Uri(string.Format("http://10.0.2.2:50001/api/posts"));

            HttpClient client = new HttpClient();
            IEnumerable<PostViewModelCopy> post = null;



            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                post = JsonConvert.DeserializeObject< IEnumerable<PostViewModelCopy>>(content);

            }
            else
            {
                throw new Exception("Ingen forbindelse til api");
            }


            listView.ItemsSource = post;
        }

        public static async Task<PostViewModelCopy> GetUsersPost(int id)
        {
            HttpClient client = new HttpClient();
            PostViewModelCopy post = null;

            //Idet skal udskiftes med brugers id
            var uri = new Uri(string.Format("http://10.0.2.2:50001/api/post/getuserspost/" + id));

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                post = JsonConvert.DeserializeObject<PostViewModelCopy>(content);
            } 

            return post;
        }
        public static async Task<PostViewModel> GetPost()
        {
            HttpClient client = new HttpClient();
            PostViewModel post = null;

            var uri = new Uri(string.Format("http://10.0.2.2:50001/api/post/9"));

            var response = await client.GetAsync(uri);

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                post = JsonConvert.DeserializeObject<PostViewModel>(content);
            }
            return post;
        }

        public async Task<bool> ClaimPost(JObject user)
        {
            var uri = new Uri(string.Format("http://10.0.2.2:50001/api/claimpost/9"));
            HttpResponseMessage response = null;
            bool postClaimed = false;

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    postClaimed = true;
                }
            }

            return postClaimed;
        }

        public static async Task<bool> CreatePostInDb(JObject post)
        {
            var uri = new Uri(string.Format("http://10.0.2.2:50001/api/post/"));

             HttpResponseMessage response = null;
            bool postCreated = false;


            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(post);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    postCreated = true;
                }
            }
            return postCreated;
        }

        public async Task<bool> DeletePost (int id)
        {
            var uri = new Uri("http://10.0.2.2:50001/api/post/" + id);
            HttpResponseMessage response = null;
            bool postDeleted = false;

            using (HttpClient client = new HttpClient())
            {
                response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    postDeleted = true;
                }
            }
            return postDeleted;
        }

        public TimeSpan ConvertIntegerToTimeSpan(int minutesAfterMidnight)
        {
            int hours = minutesAfterMidnight / 60;
            int minutes = minutesAfterMidnight % 60;

            TimeSpan time = new TimeSpan(hours, minutes, 0);

            return time;
        }
    }
}

