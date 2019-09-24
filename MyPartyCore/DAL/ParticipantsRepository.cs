using Microsoft.AspNetCore.Hosting;
using MyPartyCore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MyPartyCore.DAL
{
    class ParticipantsRepository : IParticipantsRepository
    {

        string _path;
        private IHostingEnvironment _env;

         List<Participant> participants;

        public ParticipantsRepository(IHostingEnvironment env)
        {
            _env = env;
            _path = Path.Combine(_env.WebRootPath, "Participants.json");
        }

        public List<Participant> List()
        {
            if (!File.Exists(_path))
                return new List<Participant>();

            if (participants == null)
            {
                using (StreamReader file = new StreamReader(_path))
                {
                    String participantsString = file.ReadToEnd();
                    participants = JsonConvert.DeserializeObject(participantsString, typeof(List<Participant>)) as List<Participant>;
                }
            }
            return participants;
        }

        public void Save(List<Participant> p)
        {
            if (!File.Exists(_path))
                return;

            using (StreamWriter fs = new StreamWriter(_path))
            {
                fs.Write(JsonConvert.SerializeObject(p));
            }

        }

        public void Delete(string name)
        {
            participants.RemoveAll(x => x.Name == name);
            Save(participants);
        }
    }
}
