using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MyPartyCore.Infrastructure
{
    //public class DateTimeModelBinder : IModelBinder
    //{
    //    public Task BindModelAsync(ModelBindingContext bindingContext)
    //    {
    //        var valueProvider = bindingContext.ValueProvider;

    //        DateTime arrivalDate = DateTime.MinValue;

    //        if (valueProvider.GetValue("ArrivalDate").FirstValue != "")
    //        {

    //            arrivalDate = (DateTime)valueProvider.GetValue("ArrivalDate").ConvertTo(typeof(DateTime));
    //        }

    //        return arrivalDate;
    //    }

    //}
}

