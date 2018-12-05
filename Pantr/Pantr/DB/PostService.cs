﻿using System;
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
            IEnumerable<PostViewModel> post = null;



            var response = await client.GetAsync(uri);


            TimeSpan.FromMinutes(352345235);
            listView.ItemsSource = post;
        }

        public static async Task<PostViewModel> GetUsersPost()
        {
            HttpClient client = new HttpClient();
            PostViewModel post = null;

            var uri = new Uri(string.Format("http://10.0.2.2:50001/api/post/getuserspost/1"));

            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                post = JsonConvert.DeserializeObject<PostViewModel>(content);
            } 
            //if (response.IsSuccessStatusCode)
            //{
            //    var content = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine(content);
            //    post = JsonConvert.DeserializeObject<IEnumerable<PostViewModel>>(content);
            //}
            return post;
        }
        public static async Task<PostViewModel> GetPost()
        {
            HttpClient client = new HttpClient();
            PostViewModel post = null;

            var uri = new Uri(string.Format("http://10.0.2.2:50001/api/post/1"));

            var response = await client.GetAsync(uri);

            if(response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                post = JsonConvert.DeserializeObject<PostViewModel>(content);
            }
            return post;
        }

        public static async Task<HttpResponseMessage> CreatePostInDb(PostViewModel post)
        {
            var uri = new Uri(string.Format("http://10.0.2.2:50001/api/post/"));

            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(post);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            response = await client.PostAsync(uri, content);
            return response;
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

