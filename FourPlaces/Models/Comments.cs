using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FourPlaces.Utils;
using Newtonsoft.Json;

namespace FourPlaces.Models
{
    public class Author : User
    {
        public int? image_id { get; set; } 

        public Author(string email, string fn, string ln, string pass) : base(email, fn, ln, pass)
        {
        }
    }

    public class Comment
    {
        public DateTime date { get; set; }
        public Author author { get; set; }
        public string text { get; set; }

        public string AuthorName
        {
            get
            {
                return author.first_name + " " + author.last_name+" ("+author.email+")";
            }
        }

        public string DateString
        {
            get
            {
                return date.ToShortDateString() + " " + date.ToShortTimeString();
            }
        }
    }

    public class JSONCommentsAnswer
    {
        public Place data { get; set; }
        public bool is_success { get; set; }
        public string error_code { get; set; }
        public string error_message { get; set; }
    }

    public class AuthorAnswer
    {
        public Author data { get; set; }
        public bool is_success { get; set; }
        public string error_code { get; set; }
        public string error_message { get; set; }
    }

    public static class CommentsModel
    {
        public static Place GetPlaceWithComments(int PlaceID)
        {
            using (WebClient wc = new WebClient())
            {
                String json = wc.DownloadString(Constantes.LienAPIPlaces+"/"+PlaceID);
                JSONCommentsAnswer Answer = JsonConvert.DeserializeObject<JSONCommentsAnswer>(json);

                return Answer.data;
            }
        }

        public static async Task<bool> PostComment(String comment, int PlaceID)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(Constantes.authModel.tokenType, Constantes.authModel.access_token);

            var content = new StringContent("{\"text\": \""+comment+"\"}", Encoding.UTF8, "application/json");
            var result = client.PostAsync(Constantes.API + "/places/" + PlaceID + "/comments", content).Result;
            return result.IsSuccessStatusCode;
        }
    }

    public class PostCommentAnswer
    {
        public bool is_success { get; set; }
        public string error_code { get; set; }
        public string error_message { get; set; }
    }

}
