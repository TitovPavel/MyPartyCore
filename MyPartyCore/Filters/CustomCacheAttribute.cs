using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using MyPartyCore.DB.BL;
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

        private IMemoryCache _cache;

        public CustomCacheAttribute(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(15));
            string key = context.HttpContext.Request.Path.Value;
            _cache.Set(key, context.Result, cacheEntryOptions);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            IActionResult cacheActionResult;
            string key = context.HttpContext.Request.Path.Value;
            if (_cache.TryGetValue(key, out cacheActionResult))
            {
                context.Result = cacheActionResult;

            }
        }
    }
}
