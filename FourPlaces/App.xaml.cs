﻿using System;
using FourPlaces.Models;
using FourPlaces.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FourPlaces
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            MainPage = new NavigationPage(new MainPage());
        }

        
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
