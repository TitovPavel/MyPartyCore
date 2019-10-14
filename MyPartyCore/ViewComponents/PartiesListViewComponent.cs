using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using MyPartyCore.BL;
using MyPartyCore.Infrastructure;
using MyPartyCore.Models;
using MyPartyCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.ViewComponents
{
    public class PartiesListViewComponent : ViewComponent
    {
        private readonly IPartyService partyService;
        private readonly IMapper _mapper;

        public PartiesListViewComponent(IPartyService r, IMapper mapper)
        {
            partyService = r;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke(bool lastViewedParties)
        {
            List<PartyViewModel> partyViews;

            if (lastViewedParties)
            {
                partyViews = GetLastViewedParties();
            }
            else
            {
                partyViews = GetTenParties();
            }

            return View(partyViews);
        }

        public List<PartyViewModel> GetLastViewedParties()
        {
            List<int> listId = HttpContext.Session.GetParties();

            List<PartyViewModel> partyViews = partyService.ListOfCurrentParties()
                .Where(x => listId.Contains(x.Id))
                .ProjectTo<PartyViewModel>(_mapper.ConfigurationProvider)
                .OrderByDescending(x => listId.FindIndex(y => x.Id == y))
                .ToList();

            ViewBag.NameListParties = "5 последних просмотренных вечеринок вечеринок:";
            return partyViews;

        }

        public List<PartyViewModel> GetTenParties()
        {
            List<PartyViewModel> partyViews = partyService.ListOfCurrentParties()
                .OrderBy(x => x.Date)
                .Take(10)
                .ProjectTo<PartyViewModel>(_mapper.ConfigurationProvider)
                .ToList();

            ViewBag.NameListParties = "10 ближайших вечеринок:";
            return partyViews;

        }
    }
}
