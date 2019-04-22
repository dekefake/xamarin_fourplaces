using System;
using System.Collections.Generic;
using FourPlaces.Utils;
using Xamarin.Essentials;

namespace FourPlaces.Models
{
    public class Place
    {
        public int id { get; set; }
        public int image_id { get; set; }
        public String title { get; set; }
        public String description { get; set; }

        public double latitude { get; set; }
        public double longitude { get; set; }

        public List<Comment> comments { get; set; }

        public string ImgUrl
        {
            get
            {
                return Constantes.LienAPIImages + image_id;
            }
        }

        public double distance
        {
            get
            {
                Location loc = MainPageModel.getLoc().Result;
                if (loc == null)
                {
                    return 0;
                }
                double res = Location.CalculateDistance(loc, latitude, longitude, DistanceUnits.Kilometers);
                if (res < 100)
                {
                    return Math.Round(res, 2);
                }
                if (res < 1000)
                {
                    return Math.Round(res, 1);
                }
                return Math.Round(res);
            }
        }

        public string distanceStr
        {
            get
            {
                double d = distance;
                return "À " + d + " km" + (d<2?"":"s");
            }
        }
    }

    public class ListePlaces
    {
        public List<Place> data { get; set; }
        public object error_code { get; set; }
        public object error_message { get; set; }
        public bool is_success { get; set; }
    }
}
