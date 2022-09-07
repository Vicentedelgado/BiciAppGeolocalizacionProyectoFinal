using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using App_Gps.Modelo;
using Firebase.Database.Query;

namespace App_Gps.Servicios
{
    public class ConexionFB
    {
        public static FirebaseClient firebaseClient = new FirebaseClient("https://migps-3b10e-default-rtdb.firebaseio.com/");
        /*await fc.Child("ItemTable").PostAsync(new ItemsModel() { Description = InputDescription, Date = DateTime.Now.ToString()});
        public static async Task<bool> AgregarenBolsa()
        {            

            try
            {
                string longitud = "si";
                string latitude = "no";

                
                var response = await firebaseClient.Child("Geo").PostAsync(new Locations() {Latitude=latitude, Longitude=longitud, time=DateTime.Now.ToString()})
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string t = ex.Message;
                return false;
            }
        }*/
    }
}
