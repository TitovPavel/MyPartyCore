using Microsoft.AspNetCore.Authorization;
using MyPartyCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyPartyCore.AuthorizationPolicy
{
    public class MinimumAgePartyHandler : AuthorizationHandler<MinimumAgeRequirement, Party>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            MinimumAgeRequirement requirement,
            Party party)
        {

                if (context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
                {
                    var dateOfBirth = Convert.ToDateTime(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value);
                    if (!party.AgeLimit || requirement.MinimumAge <= DateTime.Today.Year - dateOfBirth.Year)
                    {
                        context.Succeed(requirement);
                    }
                }

            return Task.FromResult(0);
        }
    }
}
