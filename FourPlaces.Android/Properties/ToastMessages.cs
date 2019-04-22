using System;
using Android.App;
using Android.Widget;
using FourPlaces.Droid.Properties;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace FourPlaces.Droid.Properties
{
        
    public class MessageAndroid : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}
