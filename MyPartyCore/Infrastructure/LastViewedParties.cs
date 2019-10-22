using Microsoft.AspNetCore.Http;
using MyPartyCore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPartyCore.Infrastructure
{
    public static class LastViewedParties
    {
        public static void AddParty(this ISession session, int partyId)
        {
            List<int> lastViewedParties = session.GetParties();
                       
            lastViewedParties.Remove(partyId);
            lastViewedParties.Add(partyId);

            session.SetString("LastViewedParties", JsonConvert.SerializeObject(lastViewedParties));

        }

        public static List<int> GetParties(this ISession session)
        {
            string valueTime = session.GetString("LastViewedPartiesTime");
            session.SetString("LastViewedPartiesTime", DateTime.Now.ToString());

            if (DateTime.TryParse(valueTime, out DateTime dateTimeModified) && dateTimeModified.AddMinutes(2) < DateTime.Now)
            {
                return new List<int>();
            }

            string value = session.GetString("LastViewedParties");
          
            return value == null ? new List<int>() : JsonConvert.DeserializeObject<List<int>>(value);

            
        }
    }
}