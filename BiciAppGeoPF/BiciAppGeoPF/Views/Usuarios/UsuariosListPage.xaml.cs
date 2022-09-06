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

        private void UsuariosListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {


        }
    }
}