using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using MyPartyCore.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.AuthorizationPolicy
{
    public class PartyAuthorizationCrudHandler : AuthorizationHandler<OperationAuthorizationRequirement, Party>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       OperationAuthorizationRequirement requirement,
                                                       Party party)
        {
            if (context.User.Identity?.Name == party.Owner.UserName &&
            requirement.Name == Operations.Update.Name)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class SameAuthorRequirement : IAuthorizationRequirement { }
}
