using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyPartyCore.DataAnnotationValidations;

namespace MyPartyCore.ViewModels
{
    public class CreatePartyViewModel
    {

        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Date")]
        public DateTime? Date { get; set; }
        [Display(Name = "Location")]
        public string Location { get; set; }
        public string OwnerId { get; set; }
        [Display(Name = "AgeLimit")]
        public bool AgeLimit { get; set; }

    }
}