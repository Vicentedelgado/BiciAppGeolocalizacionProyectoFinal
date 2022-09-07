using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BiciAppGeoPF.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BiciAppGeoPF.Views.Groups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupsEdit : ContentPage
    {
        GroupsRepository groupsRepository = new GroupsRepository();
        public GroupsEdit(MGroups groups)
        {
            InitializeComponent();
            TxtName.Text = groups.NameGrup;
            TxtId.Text = groups.Id;
        }

        private  async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            string name = TxtName.Text;
            if (string.IsNullOrEmpty(name))
            {
                await DisplayAlert("Atención", "Ingresa un nombre de grupo", "Aceptar");
            }

            MGroups groups = new MGroups();
            groups.Id = TxtId.Text;
            groups.NameGrup = name;
            
            bool isUpdated = await groupsRepository.Update(groups);
            if (isUpdated)
            {
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "Folló actualizar grupo.", "Cancelar");
            }

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}