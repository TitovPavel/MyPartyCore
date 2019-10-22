using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using MyPartyCore.BL;
using MyPartyCore.Infrastructure;
using MyPartyCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.Filters
{
    public class CustomCacheAttribute : Attribute, IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext context)
        {

            IMapper _mapper = ((Controllers.PartyController)context.Controller)._mapper;
            IPartyService partyService = ((Controllers.PartyController)context.Controller)._partyService;

            List<int> listId = context.HttpContext.Session.GetParties();

            List<PartyViewModel> partyViews = partyService.ListOfCurrentParties()
                .Where(x => listId.Contains(x.Id))
                .ProjectTo<PartyViewModel>(_mapper.ConfigurationProvider)
                .OrderByDescending(x => listId.FindIndex(y => x.Id == y))
                .ToList();

            ((Microsoft.AspNetCore.Mvc.ViewResult)context.Result).ViewData["partyViews"] = partyViews;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            int id = Convert.ToInt32(context.ActionArguments["id"]);
            context.HttpContext.Session.AddParty(id);
        }
    }
}
