using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BiciAppGeoPF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : ContentPage
    {
        UserRepository _userRepository = new UserRepository();
        public ForgotPasswordPage()
        {
            InitializeComponent();
        }

        private async void ButtonSendLink_Clicked(object sender, EventArgs e)
        {
            string email = TxtEmail.Text;
            if (string.IsNullOrEmpty(email))
            {
                await DisplayAlert("Atención", "Ingresa un correo.", "Aceptar");
                return;
            }

            bool isSend = await _userRepository.ResetPassword(email);
            if (isSend)
            {
                await DisplayAlert("Recupera", "Se envió un link a tu correo.", "Aceptar");
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Recupera", "Falló.", "Aceptar");
            }
        }

    }
}