using Microsoft.AspNetCore.Razor.TagHelpers;
using MyPartyCore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPartyCore.Helpers
{
    public class PartyTagHelper:TagHelper
    {

        public PartyViewModel PartyView { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", String.Concat("/", ControllerName, "/", ActionName, "/", PartyView.Id));
            output.Attributes.SetAttribute("data-toggle", "tooltip");
            output.Attributes.SetAttribute("title", String.Concat("Дата: ", PartyView.Date?.ToShortDateString(), ", место: ", PartyView.Location));
            output.Content.SetHtmlContent(PartyView.Title);

        }
    }
}