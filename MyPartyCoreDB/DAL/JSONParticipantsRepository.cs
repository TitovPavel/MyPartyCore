using Microsoft.AspNetCore.Hosting;
using MyPartyCore.DB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyPartyCore.DB.DAL
{
    class JSONParticipantsRepository : IParticipantsRepository
    {
        private readonly string _path;
        private readonly IHostingEnvironment _env;

        private List<Participant> participants;

        public JSONParticipantsRepository(IHostingEnvironment env)
        {
            _env = env;
            _path = Path.Combine(_env.WebRootPath, "Participants.json");
        }

        public void Save()
        {
            if (!File.Exists(_path))
                return;

            using (StreamWriter fs = new StreamWriter(_path))
            {
                fs.Write(JsonConvert.SerializeObject(participants));
            }

        }

        public List<Participant> GetAll()
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

        public Participant GetById(int participantID)
        {
            if (participants == null)
                participants = GetAll();

            Participant party = participants.FirstOrDefault(x => x.Id == participantID);
            return party;
        }

        public void Add(Participant participant)
        {
            if(participants==null)
            {
                participants = GetAll();
            }
            participants.Add(participant);
            Save();
        }

        public void Update(Participant participant)
        {
            if (participants == null)
            {
                participants = GetAll();
            }
            Delete(participant);
            Add(participant);
        }

        public void Delete(Participant participant)
        {
            if (participants == null)
            {
                participants = GetAll();
            }
            participants.RemoveAll(x => x.Name == participant.Name);
            Save();
        }
    }
}
