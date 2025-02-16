using DBRejectionTherapyAPI.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DBRejectionTherapyAPI.Data
{
    public class MongoCRUD
    {
        private IMongoDatabase db;

        public MongoCRUD(string database)
        {
            var builder = WebApplication.CreateBuilder();
            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            var client = new MongoClient(connection);
            db = client.GetDatabase(database);
        }

        //Add Dare
        public async Task<string> AddDare (string table, Dares dare)
        {
            var collection = db.GetCollection<Dares>(table);
            await collection.InsertOneAsync(dare);
            return "";
        }

        //Show All Dares
        public async Task<List<Dares>> ShowDares (string table)
        {
            var collection = db.GetCollection<Dares>(table);
            var dares = await collection.AsQueryable().ToListAsync();
            return dares;
        }

        //Get dare by Id
        public async Task<Dares> ShowDare (string table, string id)
        {
            var collection = db.GetCollection<Dares>(table);
            var dares = await collection.FindAsync(x => x.Id == id);
            return await dares.FirstOrDefaultAsync();
        }

        //Update Dare
        public async Task<Dares> UpdateDare (string table, Dares dare)
        {
            var collection = db.GetCollection<Dares>(table);
            var updateDefinition = new List<UpdateDefinition<Dares>>();

            if (!string.IsNullOrEmpty(dare.Category))
            {
                updateDefinition.Add(Builders<Dares>.Update.Set(x => x.Category, dare.Category));
            }

            if (!string.IsNullOrEmpty(dare.Difficulty))
            {
                updateDefinition.Add(Builders<Dares>.Update.Set(x => x.Difficulty, dare.Difficulty));
            }

            if (!string.IsNullOrEmpty(dare.Dare))
            {
                updateDefinition.Add(Builders<Dares>.Update.Set(x => x.Dare, dare.Dare));
            }

            if (!string.IsNullOrEmpty(dare.Name))
            {
                updateDefinition.Add(Builders<Dares>.Update.Set(x => x.Name, dare.Name));
            }

            if (updateDefinition.Any())
            {
                var combinedUpdate = Builders<Dares>.Update.Combine(updateDefinition);

                await collection.UpdateOneAsync(x => x.Id == dare.Id, combinedUpdate);
            }

            return dare;
        }
       

        //Delete Dare
        public async Task<string> DeleteDare (string table, string id)
        {
            var collection = db.GetCollection<Dares>(table);
            var dare = await collection.DeleteOneAsync(x => x.Id == id);
            return "";
        }







        //Add User
        public async Task<string> AddUser(string table, Users user)
        {
            var collection = db.GetCollection<Users>(table);
            await collection.InsertOneAsync(user);
            return "";
        }

        //Show All Users
        public async Task<List<Users>> ShowUsers(string table)
        {
            var collection = db.GetCollection<Users>(table);
            var users = await collection.AsQueryable().ToListAsync();
            return users;
        }

        //Get user by Id
        public async Task<Users> ShowUser(string table, string id)
        {
            var collection = db.GetCollection<Users>(table);
            var users = await collection.FindAsync(x => x.Id == id);
            return await users.FirstOrDefaultAsync();
        }

        //Update User
        public async Task<Users> UpdateUser(string table, Users user)
        {
            var collection = db.GetCollection<Users>(table);
            var updateDefinition = new List<UpdateDefinition<Users>>();

            if (!string.IsNullOrEmpty(user.Username))
            {
                updateDefinition.Add(Builders<Users>.Update.Set(x => x.Username, user.Username));
            }

            if (user.CompletedDares != null && user.CompletedDares.Any())
            {
                updateDefinition.Add(Builders<Users>.Update.PushEach(x => x.CompletedDares, user.CompletedDares));
            }

            if (updateDefinition.Any())
            {
                var combinedUpdate = Builders<Users>.Update.Combine(updateDefinition);
                await collection.UpdateOneAsync(x => x.Id == user.Id, combinedUpdate);
            }

            return user;
        }



        //Delete User
        public async Task<string> DeleteUser(string table, string id)
        {
            var collection = db.GetCollection<Users>(table);
            await collection.DeleteOneAsync(x => x.Id == id);
            return "";
        }
    }
}
