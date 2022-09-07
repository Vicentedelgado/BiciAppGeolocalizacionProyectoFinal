using BiciAppGeoPF.Model;
using BiciAppGeoPF.Views.Geolocation;
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
            string idgrup = groups.Id;
        }

        private void AgresarUsuario_Clicked(object sender, EventArgs e)
        {
             Navigation.PushModalAsync(new UsuariosListPage());
        }

        private  void BtnListGrupOnw_Clicked(object sender, EventArgs e)
        {
             Navigation.PushModalAsync(new UsuariosListPage());

        }

        private void ButtonGeo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new GeoLoPage());

        }
    }
}