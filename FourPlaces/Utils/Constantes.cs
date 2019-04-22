using System;
using System.Collections.Generic;
using FourPlaces.Models;

namespace FourPlaces.Utils
{
    public static class Constantes
    {
        public const string GoogleAPIKey = "AIzaSyBc2O1QobSE1_E1v2nPYUu7-ElVIoO1LEQ";
        public const string API = "https://td-api.julienmialon.com";
        public const string LienAPIPlaces = API + "/places";
        public const string LienAPIImages = API + "/images/";
        public const string LienAPIAuth = API + "/auth/";

        public static AuthModel authModel;
        //public static List<Place> Places;
    }
}
