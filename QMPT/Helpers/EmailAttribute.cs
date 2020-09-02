using System;
using System.ComponentModel.DataAnnotations;

namespace QMPT.Helpers
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EmailAttribute : ValidationAttribute
    {
        private static EmailAddressAttribute addressAttribute = new EmailAddressAttribute();

        public override bool IsValid(object value)
        {
            if (value.ToString() == string.Empty)
                return true;
            else return addressAttribute.IsValid(value);
        }
    }
}
