using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BiciAppGeoPF.Views.Usuarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsuariosListPage : ContentPage
    {
        UsuarioRepository usuarioRepository = new UsuarioRepository();

        string idgrupo1;
        public UsuariosListPage()
        {
            InitializeComponent();
            UsuariosListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }
        protected override async void OnAppearing()
        {
            var users = await usuarioRepository.GetAll();
            UsuariosListView.ItemsSource = null;
            UsuariosListView.ItemsSource = users;
            UsuariosListView.IsRefreshing = false;

        }

        private async void UsuariosListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            var users = e.Item as MUsers;
            users.IdGroups = idgrupo1;
            var isUpdate = await usuarioRepository.Update(users);
            if (isUpdate)
            {
                await DisplayAlert("Usuario", "Añadido con exito", "Aceptar");
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Usuario", "Falló al añadir", "Aceptar");
            }



        }

        private async void AddUserGpSwipeItem_Invoked(object sender, EventArgs e)
        {
            MUsers users = new MUsers();
            users.IdGroups = idgrupo1;
            var isUpdate = await usuarioRepository.Update(users);
            if (isUpdate)
            {
                await DisplayAlert("Usuario", "Añadido con exito", "Aceptar");
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Usuario", "Falló al añadir", "Aceptar");
            }


        }

        public string GetIdGrupo()
        {
            return idgrupo1;
        }
    }
}