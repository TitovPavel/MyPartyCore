using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using MyPartyCore.DB.BL;
using MyPartyCore.Infrastructure;
using MyPartyCore.ViewModels;
using System.Collections.Generic;
using System.Linq;
using MyPartyCore.Filters;
using Microsoft.AspNetCore.Http;

namespace MyPartyCore.ViewComponents
{
    public class PartiesListViewComponent : ViewComponent
    {
        private readonly IPartyService partyService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PartiesListViewComponent(IPartyService r, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            partyService = r;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
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
            List<int> listId = _httpContextAccessor.HttpContext.Session.GetParties();

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
