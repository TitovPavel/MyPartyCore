using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPartyCore.BL;
using MyPartyCore.Infrastructure;
using MyPartyCore.Models;
using MyPartyCore.ViewModels;

namespace MyPartyCore.Controllers
{
    public class PartyController : Controller
    {
        private IPartyService _partyService;
        private IHostingEnvironment _env;


        public PartyController(IPartyService r, IHostingEnvironment env)
        {
            _partyService = r;
            _env = env;
            
        }

        // GET: Party
        public ActionResult Index(int id)
        {

            HttpContext.Session.AddParty(id);

            Party party = _partyService.GetPartyByID(id);

            
            PartyParticipantsViewModel partyParticipantsViewModel = new PartyParticipantsViewModel();
            partyParticipantsViewModel.PartyID = id;
            partyParticipantsViewModel.PartyTitle = party.Title;
            partyParticipantsViewModel.PartyParticipants = _partyService.ListAttendent().Where(x => x.PartyId == id).Select(x => new PartyParticipants { Id = x.Id, Name = x.Name, ArrivalDate = x.ArrivalDate }).ToList();

            return View(partyParticipantsViewModel);
        }

        public ActionResult Vote(int id)
        {
            return View(new ParticipantViewModel() { PartyId = id });
        }

        public ActionResult Save(ParticipantViewModel participantViewModel, IFormFile file)
        {

            Participant participant = new Participant
            {
                Id = participantViewModel.Id,
                ArrivalDate = participantViewModel.ArrivalDate,
                Attend = participantViewModel.Attend,
                Name = participantViewModel.Name,
                Email = participantViewModel.Email,
                PartyId = participantViewModel.PartyId,
                Reason = participantViewModel.Reason??"",
            };

            if (file != null && file.Length > 0)
            {
                string _path = Path.Combine(_env.WebRootPath, "ParticipansPhoto", String.Concat(participant.Name, new FileInfo(file.FileName).Extension));
                using (var stream = new FileStream(_path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }

            _partyService.Vote(participant);
            return RedirectToAction("Index", new {id = participantViewModel.PartyId});
        }

        public ActionResult GetImage(string userName)
        {
            string file_name = String.Concat(userName, ".jpg");
            string path = Path.Combine(_env.WebRootPath, "ParticipansPhoto", file_name);
            if (System.IO.File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                string file_type = "image/jpg";
                return File(fs, file_type, file_name);
            }
            else
            {
                return null;
            }
        }
    }
}