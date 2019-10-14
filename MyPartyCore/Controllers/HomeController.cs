using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using MyPartyCore.BL;
using MyPartyCore.Models;
using MyPartyCore.ViewModels;

namespace MyPartyCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPartyService partyService;
        private readonly IMapper _mapper;

        public HomeController(IPartyService r, IMapper mapper)
        {
            partyService = r;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            List<PartyViewModel> partyViews = partyService.ListOfCurrentParties().ProjectTo<PartyViewModel>(_mapper.ConfigurationProvider).ToList();
            return View(partyViews);
        }
    }
}
