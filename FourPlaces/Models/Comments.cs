using System;
using System.Collections.Generic;
using System.Net;
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
    }
}
