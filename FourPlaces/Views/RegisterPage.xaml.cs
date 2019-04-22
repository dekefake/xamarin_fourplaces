using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Storm.Mvvm.Forms;
using Xamarin.Forms.Xaml;
using System.Net;
using FourPlaces.Models;
using FourPlaces.Utils;

namespace FourPlaces.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : BaseContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();

            if (Constantes.authModel == null)
            {
                Constantes.authModel = new AuthModel();
            }
            else
            {
                Constantes.authModel.AutoRefreshToken();
            }
            IsRegisterPage(Constantes.authModel.User != null && Constantes.authModel.access_token != null);
        }

        async void RegisterButtonClick(object sender, EventArgs args)
        {
            bool reg = await Constantes.authModel.Register(email.Text, firstname.Text, lastname.Text, pass.Text);

            string good = "welcome, "+ Constantes.authModel?.User?.first_name+".";
            string bad = "Sorry, for some reason, your registration failed";
            DependencyService.Get<IMessage>().ShortAlert(reg?good:bad);
        }

        async void RefreshTokenClick(object sender, EventArgs args)
        {
            bool tokenRefreshed = await Constantes.authModel.RefreshToken();

            string good = "Token refreshed";
            string bad = "Sorry, for some reason, the token has failed to refresh itself. Please try again";

            if (!tokenRefreshed)
            {
                // Le token na pas pu etre rafraichi. Une nouvelle connection est obligatoire
                Constantes.authModel = new AuthModel();
            }

            IsRegisterPage(Constantes.authModel.User != null && Constantes.authModel.access_token != null);

            DependencyService.Get<IMessage>().ShortAlert(tokenRefreshed ? good : bad);
        }

        async void LoginClick(object sender, EventArgs args)
        {
            bool login_success = await Constantes.authModel.Login(login_MailEntry.Text, login_passwordEntry.Text);

            string good = "welcome, " + Constantes.authModel?.User?.first_name + ".";
            string bad = "Bad credentials. Please try again.";

            IsRegisterPage(Constantes.authModel?.User != null && Constantes.authModel?.access_token != null);

            DependencyService.Get<IMessage>().ShortAlert(login_success ? good : bad);
        }

        async void DisconnectClick(object sender, EventArgs args)
        {
            Constantes.authModel = new AuthModel();
            DependencyService.Get<IMessage>().ShortAlert("Disconnected");
            IsRegisterPage(false);

        }

        private void IsRegisterPage(bool UserIsConnected)
        {
            login_Label.IsVisible = !UserIsConnected;
            login_Mail.IsVisible = !UserIsConnected;
            login_MailEntry.IsVisible = !UserIsConnected;
            login_password.IsVisible = !UserIsConnected;
            login_passwordEntry.IsVisible = !UserIsConnected;
            LoginButton.IsVisible = !UserIsConnected;

            registerLabel.IsVisible = !UserIsConnected;

            registerPrenom.IsVisible = !UserIsConnected;
            registerNom.IsVisible = !UserIsConnected;
            registerEmail.IsVisible = !UserIsConnected;
            registerPassword.IsVisible = !UserIsConnected;
            email.IsVisible = !UserIsConnected;
            firstname.IsVisible = !UserIsConnected;
            lastname.IsVisible = !UserIsConnected;
            pass.IsVisible = !UserIsConnected;
            registerButton.IsVisible = !UserIsConnected;

            AboutLabel.IsVisible = UserIsConnected;

            account_firstname.IsVisible = UserIsConnected;
            account_firstname.Text = "Prenom : "+Constantes.authModel.User?.first_name;

            account_lastname.IsVisible = UserIsConnected;
            account_lastname.Text = "Nom : " + Constantes.authModel.User?.last_name;

            account_email.IsVisible = UserIsConnected;
            account_email.Text = "Email : " + Constantes.authModel.User?.email;

            account_accesstoken.IsVisible = UserIsConnected;
            account_accesstoken.Text = "Access_token : " + Constantes.authModel.access_token;

            account_refreshtoken.IsVisible = UserIsConnected;
            account_refreshtoken.Text = "Refresh_token : " + Constantes.authModel.refresh_token;

            account_connecteDepuis.IsVisible = UserIsConnected;
            account_connecteDepuis.Text = "Connecté depuis le " + Constantes.authModel?.dateToken.ToLocalTime().ToLongDateString() + " a "+ Constantes.authModel?.dateToken.ToLocalTime().ToLongTimeString();

            RefreshTokenButton.IsVisible = UserIsConnected;
            DisconnectButton.IsVisible = UserIsConnected;
        }
    }
}
