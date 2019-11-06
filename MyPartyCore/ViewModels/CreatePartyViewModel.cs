﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyPartyCore.DataAnnotationValidations;

namespace MyPartyCore.ViewModels
{
    public class CreatePartyViewModel
    {
        [Display(Name = "Название вечеринки")]
        public string Title { get; set; }
       
        [Display(Name = "Дата")]
        public DateTime? Date { get; set; }
        
        [Display(Name = "Место проведения")]
        public string Location { get; set; }
        public string OwnerId { get; set; }

    }
}