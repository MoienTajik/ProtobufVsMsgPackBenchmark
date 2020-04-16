using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using ProtobufVsMsgPack.Models;

namespace ProtobufVsMsgPack.Infrastructure.Data
{
    public static class UsersRepository
    {
        public static List<User> GetUsers()
        {
            string runningLocationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string usersJsonPath = Path.Combine(runningLocationPath, "users.json");
            string usersJson = File.ReadAllText(usersJsonPath);

            var users = JsonConvert.DeserializeObject<List<User>>(usersJson);

            return users;
        }
    }
}