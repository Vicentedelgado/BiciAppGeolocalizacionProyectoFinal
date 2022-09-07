using BiciAppGeoPF.Model;
using Firebase.Auth;
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
    public partial class RegisterUser : ContentPage
    {
        UserRepository _userRepository = new UserRepository();
        UsuarioRepository usuarioRepository = new UsuarioRepository();
        string webAPIKey = "AIzaSyDARo0ISZeXl5A9uXBowEyxKkLoUZDkHvc";
        FirebaseAuthProvider authProvider;
        public RegisterUser()
        {
            InitializeComponent();
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIKey));
        }

        private async void Register_button(object sender, EventArgs e)
        {
            try
            {
                string name = TxtName.Text;
                string email = TxtEmail.Text;
                string password = TxtPassword.Text;
                string lastname = TxtLastName.Text;
                var uid = "";
                string confirmPassword = TxtConfirmPassword.Text;

                if (String.IsNullOrEmpty(name))
                {
                    await DisplayAlert("Atención", "Escribe tu nombre", "Aceptar");
                    return;
                }
                if (String.IsNullOrEmpty(lastname))
                {
                    await DisplayAlert("Atención", "Escribe tu apellido", "Aceptar");
                    return;
                }
                if (String.IsNullOrEmpty(email))
                {
                    await DisplayAlert("Atención", "Escribe un correo electrónico", "Aceptar");
                    return;
                }
                if (password.Length < 6)
                {
                    await DisplayAlert("Atención", "La contraseña debe tener 6 dígitos.", "Aceptar");
                    return;
                }

                if (String.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Atención", "Escribe una contraseña", "Aceptar");
                    return;
                }
                if (String.IsNullOrEmpty(confirmPassword))
                {
                    await DisplayAlert("Atención", "Confirma tu contraseña", "Aceptar");
                    return;
                }
                if (password != confirmPassword)
                {
                    await DisplayAlert("Atención", "La contraseña no coincide", "Aceptar");
                    return;
                }

                bool isSave = await _userRepository.Register(email, name, password);

                uid = _userRepository.GetUid();
                MUsers users = new MUsers();
                users.Name = name;
                users.Uid = uid;
                users.LastName = lastname;
                users.Password = "";
                users.Email = email;

                var isSaveUsers = await usuarioRepository.Save(users);
                if (isSave)
                {
                    await DisplayAlert("Registro", "Completo", "Aceptar");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Registro del usuario", "Falló", "Aceptar");
                }



            }
            catch (Exception exception)
            {
                if (exception.Message.Contains("EMAIL_EXISTS"))
                {
                    await DisplayAlert("Atención", "Este correo ya existe", "Aceptar");
                }
                else
                {
                    await DisplayAlert("Error", exception.Message, "Aceptar");
                }


            }



        }
    }
}