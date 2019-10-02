using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using MyPartyCore.Models;
using Newtonsoft.Json;

namespace MyPartyCore.DAL
{
    public class JSONPartyRepository : IPartyRepository
    {

        string _path;
        private IHostingEnvironment _env;

        List<Party> parties;

        public JSONPartyRepository(IHostingEnvironment env)
        {
            _env = env;
            _path = Path.Combine(_env.WebRootPath, "Parties.json");
        }

        public void Delete(Party party)
        {
            parties.RemoveAll(x => x.Id == party.Id);
            Save();
        }

        public Party GetById(int partyID)
        {
            if (parties == null)
                parties = GetAll();

            Party party = parties.FirstOrDefault(x => x.Id == partyID);
            return party;

        }

        public List<Party> GetAll()
        {
            if (!File.Exists(_path))
                return new List<Party>();

            if (parties == null)
            {
                using (StreamReader file = new StreamReader(_path))
                {
                    String participantsString = file.ReadToEnd();
                    parties = JsonConvert.DeserializeObject(participantsString, typeof(List<Party>)) as List<Party>;
                }
            }
            return parties;
        }

        public void Save()
        {
            if (!File.Exists(_path))
                return;

            using (StreamWriter fs = new StreamWriter(_path))
            {
                fs.Write(JsonConvert.SerializeObject(parties));
            }
        }

        public void Add(Party party)
        {
            if (parties == null)
            {
                parties = GetAll();
            }
            parties.Add(party);
            Save();
        }

        public void Update(Party party)
        {
            if (parties == null)
            {
                parties = GetAll();
            }
            Delete(party);
            Add(party);
        }
    }
}