﻿using MyPartyCore.Models;
using System.Collections.Generic;

namespace MyPartyCore.DAL
{
    interface IParticipantsRepository
    {
        List<Participant> GetAll();
        Participant GetById(int participantID);
        void Add(Participant participant);
        void Update(Participant participant);
        void Delete(Participant participant);
    }
}