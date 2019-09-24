using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPartyCore.ViewModels
{
    public class ParticipantViewModel
    {
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Приду")]
        public bool Attend { get; set; }
        [Display(Name = "Причина отсутствия")]
        public string Reason { get; set; }
        [Display(Name = "Время прибытия")]
        [DataType(DataType.Time)]
        public DateTime ArrivalDate { get; set; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }
        public int Id { get; set; }
        public int PartyId { get; set; }
    }
}