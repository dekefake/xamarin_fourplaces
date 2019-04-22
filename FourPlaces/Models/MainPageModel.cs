using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FourPlaces.Utils;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace FourPlaces.Models
{
    public static class MainPageModel 
    {
        

        public static List<Place> GetPlaces()
        {
            using (WebClient wc = new WebClient())
            {
                String json = wc.DownloadString(Constantes.LienAPIPlaces);
                ListePlaces Places = JsonConvert.DeserializeObject<ListePlaces>(json);

                Places?.data.Sort((o1, o2) =>
                {
                        return o1.distance.CompareTo(o2.distance);
                });
                
                return Places?.data;
            }
        }

        public static async Task<Location> getLoc()
        {
            Location location = null;
            try
            {
                location = await Geolocation.GetLastKnownLocationAsync();
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

            return location;
        }
    }
}
