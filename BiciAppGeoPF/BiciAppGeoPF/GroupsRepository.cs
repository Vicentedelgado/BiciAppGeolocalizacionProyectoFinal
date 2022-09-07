using BiciAppGeoPF.Model;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiciAppGeoPF
{
    public class GroupsRepository
    {
        FirebaseClient firebaseClient = new FirebaseClient
            ("https://biciappgeolocalizacion-default-rtdb.firebaseio.com");
        
        public async Task<bool> Save(MGroups groups)
        {
            var data = await firebaseClient.Child(nameof(MGroups))
                .PostAsync(JsonConvert.SerializeObject(groups));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;


        }

        public async Task<List<MGroups>> GetAll()
        {
            return (await firebaseClient.Child(nameof(MGroups)).OnceAsync<MGroups>()).Select(item => new MGroups
            {
                NameGrup = item.Object.NameGrup,
                Id = item.Key
            }).ToList();
        }

        public async Task<MGroups> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(MGroups) + "/" + id).OnceSingleAsync<MGroups>());
        }

        public async Task<bool> Update(MGroups groups)
        {
            await firebaseClient.Child(nameof(MGroups) + "/" + groups.Id).PutAsync(JsonConvert.SerializeObject(groups));
            return true;
        }

        public async Task<bool> Delete(string id)
        {
            await firebaseClient.Child(nameof(MGroups) + "/" + id).DeleteAsync();
            return true;
        }

        public async Task<List<MGroups>> GetAllByName(string name)
        {
            return (await firebaseClient.Child(nameof(MGroups)).OnceAsync<MGroups>()).Select(item => new MGroups
            {
                NameGrup = item.Object.NameGrup,
                Id = item.Key
            }).Where(c => c.NameGrup.ToLower().Contains(name.ToLower())).ToList();
        }



    }
}
