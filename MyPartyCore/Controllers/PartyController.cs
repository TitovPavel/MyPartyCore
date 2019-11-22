using System;
using System.IO;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPartyCore.DB.BL;
using MyPartyCore.Infrastructure;
using MyPartyCore.DB.Models;
using MyPartyCore.ViewModels;
using MyPartyCore.Filters;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using MyPartyCore.AuthorizationPolicy;

namespace MyPartyCore.Controllers
{
    [Authorize(Roles = "user")]
    public class PartyController : Controller
    {
        private readonly IPartyService _partyService;
        private readonly IHostingEnvironment _env;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<User> _userManager;

        public PartyController(IPartyService r, 
            IHostingEnvironment env, 
            IMapper mapper, 
            IHttpContextAccessor httpContextAccessor, 
            UserManager<User> userManager,
            IAuthorizationService authorizationService)
        {
            _partyService = r;
            _env = env;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _authorizationService = authorizationService;

        }

        public async Task<ActionResult> Index(int id, int page = 1)
        {

            Party party = _partyService.GetPartyWithOwnerByID(id);

            if (party == null)
            {
                return new NotFoundResult();
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, party, "ReadPartyOver18");

            if (authorizationResult.Succeeded)
            {
                int pageSize = 3;

                IQueryable<Participant> source = _partyService.ListAttendent().Where(x => x.PartyId == id);
                var count = source.Count();
                var items = source.Skip((page - 1) * pageSize).Take(pageSize).ProjectTo<PartyParticipants>(_mapper.ConfigurationProvider).ToList();

                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

                _httpContextAccessor.HttpContext.Session.AddParty(id);

                PartyParticipantsViewModel partyParticipantsViewModel = new PartyParticipantsViewModel();
                partyParticipantsViewModel.PartyID = id;
                partyParticipantsViewModel.PartyTitle = party.Title;
                partyParticipantsViewModel.PartyParticipants = items;
                partyParticipantsViewModel.PageViewModel = pageViewModel;

                return View(partyParticipantsViewModel);
            }
            else
            {
                return new ForbidResult();
            }
        }

        public ActionResult Vote(int id)
        {
            return View(new ParticipantViewModel() { PartyId = id, UserId = _userManager.GetUserId(User) });
        }

        public ActionResult Save(ParticipantViewModel participantViewModel, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                Participant participant = _mapper.Map<Participant>(participantViewModel);

                if (_partyService.ParticipantBelongUser(participant))
                {
                    if (file != null && file.Length > 0)
                    {
                        string _path = Path.Combine(_env.WebRootPath, "ParticipansPhoto", String.Concat(participant.Name, new FileInfo(file.FileName).Extension));
                        using (var stream = new FileStream(_path, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }

                    _partyService.Vote(participant);
                    return RedirectToAction("Index", new { id = participantViewModel.PartyId });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Участинк под данным именем зарегистрирован другим пользвателем.");
                }
            }

            return View("Vote", participantViewModel);
     
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

        [HttpGet]
        public ActionResult Add(string id)
        {
            return View(new CreatePartyViewModel() { OwnerId = id });
        }

        [HttpPost]
        public ActionResult Add(CreatePartyViewModel partyViewModel)
        {
            if (ModelState.IsValid)
            {
                Party party = _mapper.Map<Party>(partyViewModel);
                
                _partyService.AddParty(party);

                return RedirectToAction("List", "Party", new { id = partyViewModel.OwnerId });
            }
            else
            {
                return View(partyViewModel);
            }
        }

        [TypeFilter(typeof(CustomCacheAttribute))]
        [HttpGet]
        public ActionResult List(string id)
        {
            PartiesByOwnerViewModel partiesByOwnerViewModel = new PartiesByOwnerViewModel()
            {
                Id = id
            };

            partiesByOwnerViewModel.Parties = _partyService.ListOfPartiesByOwner(id).ProjectTo<PartyViewModel>(_mapper.ConfigurationProvider).ToList();
            return View(partiesByOwnerViewModel);
        }

        [HttpGet]
        [Authorize(Policy = "Over18")]
        public ActionResult AdultParties()
        {
            List<PartyViewModel> partyViews = _partyService.ListOfCurrentParties().Where(x => x.AgeLimit).ProjectTo<PartyViewModel>(_mapper.ConfigurationProvider).ToList();
            return View(partyViews);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            Party party = _partyService.GetPartyWithOwnerByID(id);

            if (party == null)
            {
                return new NotFoundResult();
            }

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, party, Operations.Update);

            if (authorizationResult.Succeeded)
            {
                EditPartyViewModel partyViewModel = _mapper.Map<EditPartyViewModel>(party);

                return View(partyViewModel);
            }
            else
            {
                return new ForbidResult();
            }
        }

        [HttpPost]
        public IActionResult Edit(EditPartyViewModel model)
        {
            if (ModelState.IsValid)
            {
                Party party = _mapper.Map<Party>(model);
                _partyService.UpdateParty(party);
                return RedirectToAction("List", new { id = _userManager.GetUserId(User) });
            }

            return View(model);
        }
    }
}