using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BiciAppGeoPF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        UserRepository _userRepository = new UserRepository();
        public ICommand TapCommand => new Command(async () => await Navigation.PushModalAsync(new RegisterUser()));

        public LoginPage()
        {
            InitializeComponent();
            bool hasKey = Preferences.ContainsKey("token");
            if (hasKey)
            {
                string token = Preferences.Get("token", "");
                if (!string.IsNullOrEmpty(token))
                {
                    Navigation.PushAsync(new HomePage());
                }
            }

        }

        private async void BtnSignIn_Clicked(object sender, EventArgs e)
        {
            try
            {
                string email = TxtEmail.Text;
                string password = TxtPassword.Text;
                if (String.IsNullOrEmpty(email))
                {
                    await DisplayAlert("Atención", "Escribe un correo electrónico", "Aceptar");
                    return;
                }
                if (String.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Atención", "Escribe una contraseña", "Aceptar");
                    return;
                }
                string token = await _userRepository.SignIn(email, password);
                if (!string.IsNullOrEmpty(token))
                {
                    Preferences.Set("token", token);
                    Preferences.Set("userEmail", email);
                    await Navigation.PushAsync(new HomePage());
                }
                else
                {
                    await DisplayAlert("Login", "Falló", "Aceptar");
                }
            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("EMAIL_NOT_FOUND"))
                {
                    await DisplayAlert("No autorizado", "Este email no existe", "Aceptar");
                }
                else if (exception.Message.Contains("INVALID_PASSWORD"))
                {
                    await DisplayAlert("No autorizado", "Contraseña incorrecta", "Aceptar");
                }
                else
                {
                    await DisplayAlert("Error", exception.Message, "Aceptar");
                }
            }

        }

        private async void RegisterTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterUser());
        }

        private async void ForgotTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ForgotPasswordPage());
        }

    }
}