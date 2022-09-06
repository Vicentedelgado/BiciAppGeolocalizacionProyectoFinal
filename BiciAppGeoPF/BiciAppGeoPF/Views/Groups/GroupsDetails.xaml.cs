using BiciAppGeoPF.Views.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BiciAppGeoPF.Views.Groups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupsDetails : ContentPage
    {
        public GroupsDetails(MGroups groups)
        {
            InitializeComponent();

            LabelName.Text = groups.NameGrup;
        }

        private async void AgresarUsuario_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new UsuariosListPage());
        }
    }
}