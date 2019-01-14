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
        /**
         * Henter via et API kald til vores Web API en IEnumerable-liste af PostViewModel-typen. 
         * Zipcode-parameteret har en default værdi der er tom, så vi ved om vi skal kalde vores API metode med eller uden parameter.
         * Statisk, så vi ikke behøver at lave en instans af PostService
        */
        public async static Task<ObservableCollection<PostViewModel>> GetAllPosts(String zipcode="")
        {
            Uri uri = null;
            if (zipcode.Equals(""))
            {
                //Laver URI'en til get metoden uden parameter
                //uri = new Uri(string.Format("http://10.0.2.2:50001/api/posts"));
                uri = new Uri(string.Format(IService.basicApi + "posts"));

            }
            else
            {
                //Laver URI'en til get metoden uden parameter
                //uri = new Uri(string.Format("http://10.0.2.2:50001/api/posts/{0}",zipcode));
                uri = new Uri(string.Format(IService.basicApi + "posts/{0}", zipcode));
            }
            HttpClient client = new HttpClient();
            ObservableCollection<PostViewModel> post = null;

            //Laver API-kald
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                //content (resultatet fra vores APIkald) bliver de-serialiseret
                // fra JSon til en observable collection
                var content = await response.Content.ReadAsStringAsync();
                post = JsonConvert.DeserializeObject<ObservableCollection<PostViewModel>>(content);
            }
            else
            {
                throw new Exception("Ingen forbindelse til api");
            }
            //observable collection bliver returneret
            return post;
        }

        public async Task<PostViewModel> GetUsersPost(int id)
        {
            HttpClient client = new HttpClient();
            PostViewModel post = null;

            //var uri = new Uri(string.Format("http://10.0.2.2:50001/api/post/getuserspost/" + id));
            var uri = new Uri(string.Format(IService.basicApi + "post/getuserspost/" + id));


            //Laver rest kald 
            var response = await client.GetAsync(uri);

            //Tjekker om responsen er succesfuld
            if (response.IsSuccessStatusCode)
            {
                //Uddrager indholdet fra responsen
                var content = await response.Content.ReadAsStringAsync();
                //Deserializer indholdet
                post = JsonConvert.DeserializeObject<PostViewModel>(content);
            } 

            return post;
        }
        public static async Task<PostViewModel> GetPost()
        {
            HttpClient client = new HttpClient();
            PostViewModel post = null;

            //var uri = new Uri(string.Format("http://10.0.2.2:50001/api/post/9"));
            var uri = new Uri(string.Format(IService.basicApi + "post/9"));


            //Laver rest kald 
            var response = await client.GetAsync(uri);

            //Tjekker om responsen er succesfuld
            if (response.IsSuccessStatusCode)
            {
                //Uddrager indholdet fra responsen
                var content = await response.Content.ReadAsStringAsync();
                //Deserializer indholdet
                post = JsonConvert.DeserializeObject<PostViewModel>(content);
            }
            return post;
        }

        public async Task<bool> ClaimPost(JObject userGiverId)
        {
            //var uri = new Uri(string.Format("http://10.0.2.2:50001/api/claimpost/" + userId));
            var uri = new Uri(string.Format(IService.basicApi + "claimpost"));

            HttpResponseMessage response = null;
            bool postClaimed = false;

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(userGiverId);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                response = await client.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    postClaimed = true;
                }
            }

            return postClaimed;
        }

        public async Task<bool> CreatePostInDb(JObject post)
        {
            //var uri = new Uri(string.Format("http://10.0.2.2:50001/api/post/"));
            var uri = new Uri(string.Format(IService.basicApi + "post/"));


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

        public async Task<bool> UpdatePost (int id, JObject updatedPost)
        {
            //var uri = new Uri(string.Format("http://10.0.2.2:50001/api/updatepost/" + id));
            var uri = new Uri(string.Format(IService.basicApi + "updatepost/" + id));

            HttpResponseMessage response = null;
            bool postUpdated = false;

            using (HttpClient client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(updatedPost);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                response = await client.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    postUpdated = true;
                }
            }
            return postUpdated;
        }

        public async Task<bool> DeletePost (int id)
        {
            //var uri = new Uri("http://10.0.2.2:50001/api/post/" + id);
            var uri = new Uri(IService.basicApi + "post/" + id);

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
    }
}

