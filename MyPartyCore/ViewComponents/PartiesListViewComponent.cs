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
using Microsoft.Extensions.Localization;

namespace MyPartyCore.ViewComponents
{
    public class PartiesListViewComponent : ViewComponent
    {
        private readonly IPartyService _partyService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<PartiesListViewComponent> _localizer;

        public PartiesListViewComponent(IPartyService partyService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IStringLocalizer<PartiesListViewComponent> localizer)
        {
            _partyService = partyService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
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

            List<PartyViewModel> partyViews = _partyService.ListOfCurrentParties()
                .Where(x => listId.Contains(x.Id))
                .ProjectTo<PartyViewModel>(_mapper.ConfigurationProvider)
                .OrderByDescending(x => listId.FindIndex(y => x.Id == y))
                .ToList();

            ViewBag.NameListParties = _localizer["NameLastListParties"];
            return partyViews;

        }

        public List<PartyViewModel> GetTenParties()
        {
            List<PartyViewModel> partyViews = _partyService.ListOfCurrentParties()
                .OrderBy(x => x.Date)
                .Take(10)
                .ProjectTo<PartyViewModel>(_mapper.ConfigurationProvider)
                .ToList();

            ViewBag.NameListParties = _localizer["NameTenListParties"];
            return partyViews;

        }
    }
}
