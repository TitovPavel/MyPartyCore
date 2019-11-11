using Microsoft.AspNetCore.Hosting;
using MyPartyCore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.DAL
{
    public class UserRepository
    {
        string _path;
        private IHostingEnvironment _env;

        List<User> users;

        public UserRepository(IHostingEnvironment env)
        {
            _env = env;
            _path = Path.Combine(_env.WebRootPath, "Users.json");
        }

        public void Delete(User user)
        {
            users.RemoveAll(x => x.Login == user.Login);
            Save();
        }

        public User GetById(string userLogin)
        {
            if (users == null)
                users = GetAll();

            User user = users.FirstOrDefault(x => x.Login == userLogin);
            return user;

        }      

        public User GetBySessionID(string SessionID)
        {
            if (users == null)
                users = GetAll();

            User user = users.FirstOrDefault(x => x.SessionID == SessionID);
            return user;

        }

        public List<User> GetAll()
        {
            if (!File.Exists(_path))
                return new List<User>();

            if (users == null)
            {
                using (StreamReader file = new StreamReader(_path))
                {
                    String participantsString = file.ReadToEnd();
                    users = JsonConvert.DeserializeObject(participantsString, typeof(List<User>)) as List<User>;
                }
            }

            if (users == null)
            {
                users = new List<User>();
            }
            
            return users;
        }

        public void Save()
        {
            if (!File.Exists(_path))
                return;

            using (StreamWriter fs = new StreamWriter(_path))
            {
                fs.Write(JsonConvert.SerializeObject(users));
            }
        }

        public void Add(User user)
        {
            if (users == null)
            {
                users = GetAll();
            }
            users.Add(user);
            Save();
        }

        public void Update(User user)
        {
            if (users == null)
            {
                users = GetAll();
            }
            Delete(user);
            Add(user);
        }
    }
}
