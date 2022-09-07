using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using App_Gps.Modelo;
using Firebase.Database;
using Firebase.Database.Query;

namespace App_Gps
{
    public partial class MainPage : ContentPage
    {
        public Locations CurrentLocation { get; set; }
        public static FirebaseClient firebaseClient = new FirebaseClient("https://biciappgeolocalizacion-default-rtdb.firebaseio.com/");
        public string lon;
        public string lat;
        public Boolean flag=false;

        public MainPage()
        {
            InitializeComponent();            
        }
        

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        async void btnLocation_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    CurrentLocation = new Locations
                    {
                        Latitude = location.Latitude.ToString(),
                        Longitude = location.Longitude.ToString()
                    };
                    lblLatitude.Text = "Latitude: " + CurrentLocation.Latitude;
                    lblLongitude.Text = "Longitude:" + CurrentLocation.Longitude;
                    lat = CurrentLocation.Latitude;
                    lon = CurrentLocation.Longitude;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Faild", fnsEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Faild", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Faild", ex.Message, "OK");
            }
            await DisplayAlert("Ubicacion", "Se cargara su ubicacion", "OK");

        }

        private void btnOK_Clicked(object sender, EventArgs e)
        {            
                Device.StartTimer(new TimeSpan(0,0,10), () =>
                {
                    DisplayAlert("time", "10 segundos", "OK");
                    if(flag==false)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
                });                        
            
        }

        private void btnStop_Clicked(object sender, EventArgs e)
        {
            flag = !flag;
        }

        async void btnFB_Clicked(object sender, EventArgs e)
        {
            try
            {
                string latitude = lblLatitude.Text;
                string longitude = lblLongitude.Text;

                await DisplayAlert("1", longitude, "OK");
                await DisplayAlert("2", latitude, "OK");

                await firebaseClient.Child("MUsers").PostAsync(new Locations() { Latitude = lat, Longitude = lon, time = DateTime.Now.ToString() });
                
            }
            catch (Exception ex)
            {
                await DisplayAlert("Faild", ex.Message, "FAILED");
            }
        }

        public void hola()
        {
            // do something every 60 seconds

            Device.BeginInvokeOnMainThread(() =>
            {
                // interact with UI elements

            });
            //return false; // runs again, or false to stop
        }

        public void update()
        {
            /*var GetItems = (await fc
                  .Child("ItemTable")
                  .OnceAsync<ItemsModel>()).Select(item => new ItemsModel
                  {
                      Description = item.Object.Description,
                      Date = item.Object.Date,
                      Key = item.Key
                  });
            var selected = Items.Where(k => k.Key == SelectedKey.Key).FirstOrDefault();*/
        }


    }

}
