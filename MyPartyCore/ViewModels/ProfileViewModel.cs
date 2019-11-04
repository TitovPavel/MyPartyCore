using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.ViewModels
{
    public class ProfileViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Имя")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Пол")]
        public string Sex { get; set; }

    }
}
