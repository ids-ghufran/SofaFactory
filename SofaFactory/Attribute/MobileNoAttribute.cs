using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SofaFactory.Attribute
{
    public class MobileNoAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }
            string mobileNumber = value.ToString();

            // Regular expression to validate Indian mobile number without country code
            var regex = new Regex(@"^[6-9]\d{9}$");

            return regex.IsMatch(mobileNumber);
        }
    }
}

