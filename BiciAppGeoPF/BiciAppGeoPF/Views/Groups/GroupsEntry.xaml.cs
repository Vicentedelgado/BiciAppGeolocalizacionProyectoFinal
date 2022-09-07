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
    public partial class GroupsEntry : ContentPage
    {
        GroupsRepository repository = new GroupsRepository();
        public GroupsEntry()
        {
            InitializeComponent();
        }

        private async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            string name = TxtName.Text;
            if (string.IsNullOrEmpty(name))
            {
                await DisplayAlert("Atención", "Ingresa un nombre de grupo", "Aceptar");
            }
            

            MGroups groups = new MGroups();
            groups.NameGrup = name;

            var isSaved = await repository.Save(groups);
            if (isSaved)
            {
                await DisplayAlert("Atención", "Grupo creado con éxito.", "Aceptar");
                Clear();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo crear el grupo.", "Aceptar");
            }


        }
        public void Clear()
        {
            TxtName.Text = string.Empty;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}