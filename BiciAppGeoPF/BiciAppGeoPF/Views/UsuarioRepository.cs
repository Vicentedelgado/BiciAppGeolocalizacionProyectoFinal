using BiciAppGeoPF.Model;
using Firebase.Database;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiciAppGeoPF.Views
{
    internal class UsuarioRepository
    {
        FirebaseClient firebaseClient = new FirebaseClient
            ("https://biciappgeolocalizacion-default-rtdb.firebaseio.com");


        public async Task<bool> Save(MUsers users)

        {
            var data = await firebaseClient.Child(nameof(MUsers))
                .PostAsync(JsonConvert.SerializeObject(users));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;


        }

        public async Task<List<MUsers>> GetAll()
        {
            return (await firebaseClient.Child(nameof(MUsers)).OnceAsync<MUsers>()).Select(item => new MUsers
            {
                Name = item.Object.Name,
                Uid = item.Object.Uid,
                Email = item.Object.Email,
                LastName = item.Object.LastName,
                IdGroups = item.Object.IdGroups,
                Id = item.Key
            }).ToList();
        }

        public async Task<MUsers> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(MUsers) + "/" + id).OnceSingleAsync<MUsers>());
        }

        public async Task<bool> Update(MUsers users)
        {
            await firebaseClient.Child(nameof(MUsers) + "/" + users.Id).PutAsync(JsonConvert.SerializeObject(users));
            return true;
        }

        public async Task<bool> Delete(string id)
        {
            await firebaseClient.Child(nameof(MUsers) + "/" + id).DeleteAsync();
            return true;
        }

        public async Task<List<MUsers>> GetAllByName(string name)
        {
            return (await firebaseClient.Child(nameof(MUsers)).OnceAsync<MUsers>()).Select(item => new MUsers
            {
                Name = item.Object.Name,
                Uid = item.Object.Uid,
                Email = item.Object.Email,
                LastName = item.Object.LastName,
                IdGroups = item.Object.IdGroups,
                Id = item.Key
            }).Where(c => c.Name.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}