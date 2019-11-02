using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyPartyCore.DataAnnotationValidations;

namespace MyPartyCore.ViewModels
{
    public class PartyViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Название вечеринки")]
        //[Required]
        public string Title { get; set; }
        //[DataAnnotationCustom]
        //[Required]
        [Display(Name = "Дата")]
        public DateTime? Date { get; set; }
        //[Required]
        [Display(Name = "Место проведения")]
        public string Location { get; set; }

    }
}