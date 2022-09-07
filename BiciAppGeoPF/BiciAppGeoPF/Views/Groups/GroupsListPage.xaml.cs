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
    public partial class GroupsListPage : ContentPage
    {
        GroupsRepository groupsRepository = new GroupsRepository();
        public GroupsListPage()
        {
            InitializeComponent();
            GroupsListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }

        protected override async void OnAppearing()
        {
            var groups = await groupsRepository.GetAll();
            GroupsListView.ItemsSource = null;
            GroupsListView.ItemsSource = groups;
            GroupsListView.IsRefreshing = false;

        }

        private void GroupsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            var groupsnet = e.Item as MGroups;
            Navigation.PushModalAsync(new GroupsDetails(groupsnet));
            ((ListView)sender).SelectedItem = null;


        }

        private async void EditTap_Tapped(object sender, EventArgs e)
        {
            //DisplayAlert("Editar", "¿Quieres guardar los cambios?", "Aceptar");
            string id = ((TappedEventArgs)e).Parameter.ToString();
            var groups = await groupsRepository.GetById(id);
            if (groups == null)
            {
                await DisplayAlert("Atención", "Registro no encontrado.", "Aceptar");
            }
            groups.Id = id;
            await Navigation.PushModalAsync(new GroupsEdit(groups));

        }

        private async void DeleteTapp_Tapped(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Eliminar", "¿Quieres eliminar el grupo de la lista?", "Aceptar", "Cancelar");
            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await groupsRepository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Atención", "El grupo fue eliminado.", "Aceptar");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Fallo eliminar grupo.", "Aceptar");
                }
            }

        }

        private async void TxtSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            string searchValue = TxtSearch.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var groups = await groupsRepository.GetAllByName(searchValue);
                GroupsListView.ItemsSource = null;
                GroupsListView.ItemsSource = groups;
            }
            else
            {
                OnAppearing();
            }

        }

        private async void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchValue = TxtSearch.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var groups = await groupsRepository.GetAllByName(searchValue);
                GroupsListView.ItemsSource = null;
                GroupsListView.ItemsSource = groups;
            }
            else
            {
                OnAppearing();
            }


        }

        private async void EditMenuItem_Clicked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var groups = await groupsRepository.GetById(id);
            if (groups == null)
            {
                await DisplayAlert("Atención", "Registro no encontrado.", "Aceptar");
            }
            groups.Id = id;
            await Navigation.PushModalAsync(new GroupsEdit(groups));

        }

        private async void DeleteMenuItem_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Eliminar", "¿Quieres eleminar el grupo de la lista?", "Aceptar", "Cancelar");
            if (response)
            {
                string id = ((MenuItem)sender).CommandParameter.ToString();
                bool isDelete = await groupsRepository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Atención", "El grupo fue eliminado.", "Aceptar");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Fallo eliminar grupo.", "Aceptar");
                }
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void BtnSignIn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new GroupsEntry());
        }
    }
}