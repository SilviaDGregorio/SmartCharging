﻿using System.ComponentModel.DataAnnotations;

namespace SmartCharging.Api.DTO
{
    public class RequiredGreaterThanZeroValidation : ValidationAttribute
    {
        /// <param name="value">The float value of the selection</param>
        /// <returns>True if value is greater than zero</returns>
        public override bool IsValid(object value)
        {
            // return true if value is a non-null number > 0, otherwise return false
            return value != null && float.TryParse(value.ToString(), out float number) && number > 0;
        }
    }
}
