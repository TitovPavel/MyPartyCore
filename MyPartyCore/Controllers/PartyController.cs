using System;
using System.IO;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPartyCore.BL;
using MyPartyCore.Infrastructure;
using MyPartyCore.Models;
using MyPartyCore.ViewModels;
using MyPartyCore.Filters;

namespace MyPartyCore.Controllers
{
    public class PartyController : Controller
    {
        internal readonly IPartyService _partyService;
        private readonly IHostingEnvironment _env;
        internal readonly IMapper _mapper;


        public PartyController(IPartyService r, IHostingEnvironment env, IMapper mapper)
        {
            _partyService = r;
            _env = env;
            _mapper = mapper;
        }

        [CustomCache]
        public ActionResult Index(int id, int page = 1)
        {

            int pageSize = 3;

            IQueryable<Participant> source = _partyService.ListAttendent().Where(x => x.PartyId == id);
            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ProjectTo<PartyParticipants>(_mapper.ConfigurationProvider).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
        
            Party party = _partyService.GetPartyByID(id);

            
            PartyParticipantsViewModel partyParticipantsViewModel = new PartyParticipantsViewModel();
            partyParticipantsViewModel.PartyID = id;
            partyParticipantsViewModel.PartyTitle = party.Title;
            partyParticipantsViewModel.PartyParticipants = items;
            partyParticipantsViewModel.PageViewModel = pageViewModel;

            return View(partyParticipantsViewModel);
        }

        public ActionResult Vote(int id)
        {
            return View(new ParticipantViewModel() { PartyId = id });
        }

        public ActionResult Save(ParticipantViewModel participantViewModel, IFormFile file)
        {

            Participant participant = _mapper.Map<Participant>(participantViewModel);

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