using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPartyCore.BL;
using MyPartyCore.Models;
using MyPartyCore.ViewModels;

namespace MyPartyCore.Controllers
{
    public class HomeController : Controller
    {
        IPartyService partyService;

        public HomeController(IPartyService r)
        {
            partyService = r;
        }

        public ActionResult Index()
        {
            List<PartyViewModel> partyViews = partyService.ListOfCurrentParties().Select(_ => new PartyViewModel { Id = _.Id, Title = _.Title, Location = _.Location, Date = _.Date }).ToList();
            return View(partyViews);
        }
    }
}
