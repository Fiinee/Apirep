﻿using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication2.Entities;
using WebApplication2.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.DataAccess.Models;


namespace WebApplication2.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<Role> _roles;

        public AuthorizeAttribute(params Role[] roles)
        {
            _roles = roles?? new Role[] { };
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous) return;
            var account = (Account)context.HttpContext.Items["User"];
            if (account == null || (_roles.Any() && !_roles.Contains(account.Role))) { 
                context.Result = new JsonResult(new { message = "Unauthorizated" })
                { StatusCode = StatusCodes.Status401Unauthorized };
            } 
        }
    }
}
