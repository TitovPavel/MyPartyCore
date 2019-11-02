using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyPartyCore.DataAnnotationValidations
{
    public class DataAnnotationCustomAttribute : ValidationAttribute
    {
        public DataAnnotationCustomAttribute()
        {
            ErrorMessage = "Дата не может быть меньше текущей даты.";
        }

        public override bool IsValid(object value)
        {

            string dateString = value.ToString();

            if (DateTime.TryParse(dateString, out DateTime date) && date > DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
