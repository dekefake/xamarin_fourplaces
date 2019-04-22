using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FourPlaces.Utils;
using Newtonsoft.Json;

namespace FourPlaces.Models
{
    public class AuthModel
    {
        public User User { get; internal set; }
        public String access_token { get; internal set; }
        public String refresh_token { get; internal set; }
        public int tokenvalidity { get; internal set; }
        public DateTime dateToken { get; internal set; } 

        public async Task<bool> Register(string email, string fn, string ln, string mdp)
        {
            User u = new User(email, fn, ln, mdp);
            string r;

            try
            {
                r = Http_Utils.JSONPost(u, Constantes.LienAPIAuth + "register");
            }
            catch
            {
                return false;
            }
    
            RegisterResponse RegRes = JsonConvert.DeserializeObject<RegisterResponse>(r);

            if (RegRes == null || !RegRes.is_success)
            {
                return true;
            }

            await Login(email, mdp);

            // We do not keep passwords ;p
            u.password = null;

            User = u;
            
            return true;
        }

        public async Task<bool> Login(string email, string mdp)
        {
            string r;
            try
            {
                r = Http_Utils.JSONPost(new { email = email, password = mdp }, Constantes.LienAPIAuth + "login");
            }
            catch
            {
                return false;
            }
            RegisterResponse RegRes = JsonConvert.DeserializeObject<RegisterResponse>(r);

            if (RegRes == null || !RegRes.is_success)
            {
                return false;
            }
            
            dateToken = DateTime.UtcNow;
            access_token = RegRes.data.access_token;
            refresh_token = RegRes.data.refresh_token;
            tokenvalidity = RegRes.data.expires_in;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(RegRes.data.token_type, RegRes.data.access_token);
            var response = await client.GetAsync(Constantes.API + "/me");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Author author = JsonConvert.DeserializeObject<AuthorAnswer>(content).data;
                Constantes.authModel.User = author;
            }
            else
            {
                return false;
            }

            return true;
        }

        public async Task<bool> RefreshToken()
        {
            string r;
            try
            {
                r = Http_Utils.JSONPost(new { refresh_token = refresh_token }, Constantes.LienAPIAuth + "refresh");
            }
            catch
            {
                return false;
            }

            RegisterResponse RegRes = JsonConvert.DeserializeObject<RegisterResponse>(r);

            if (RegRes == null || !RegRes.is_success)
            {
                return false;
            }

            dateToken = DateTime.UtcNow;
            access_token = RegRes.data.access_token;
            refresh_token = RegRes.data.refresh_token;
            tokenvalidity = RegRes.data.expires_in;

            return true;
        }

        public async Task<bool> AutoRefreshToken()
        {
            TimeSpan interval = DateTime.UtcNow - dateToken;

            // Si le token expire dans moins de 10 minutes
            if (interval.Seconds > tokenvalidity-600)
            {
                await RefreshToken();
                return true;
            }
            
            return false;
        }
    }

    public class User
    {
        public String email;
        public String first_name;
        public String last_name;
        public String password;

        public User(String e, String f, String l, String p)
        {
            email = e;
            first_name = f;
            last_name = l;
            password = p;
        }
    }

    public class RegisterResponseData
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
    }

    public class RegisterResponse
    {
        public RegisterResponseData data { get; set; }
        public bool is_success { get; set; }
        public string error_code { get; set; }
        public string error_message { get; set; }
    }
}
