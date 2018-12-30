using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CVhub.CustomDataAnnotations
{
    public class CurrentDateAttribute : ValidationAttribute
    {
        public CurrentDateAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var dt = (DateTime)value;

            if (dt >= DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
}
