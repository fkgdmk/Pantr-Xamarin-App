using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Pantr.DB
{
    class PostService
    {
        public async Task<PostViewModel> GetUsersPost ()
        {
            PostViewModel post = null;

            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;

            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/json"));

            //HttpResponseMessage response = await client.GetAsync("http://localhost:50001/api/post/getuserspost/1");

            //if (response.IsSuccessStatusCode)
            //{
            //    post = await response.Content.ReadAsAsync;
            //}

            var uri = new Uri(string.Format("http://localhost:50001/api/post/getuserspost/1", string.Empty));

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                post = JsonConvert.DeserializeObject<PostViewModel>(content);
            }
            return post;
        }
    }
}
