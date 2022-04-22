using Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CardNumberValidationAttribute : ValidationAttribute
    {
        public CardNumberValidationAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public override bool IsValid(object value)
        {
            var numCard = (ulong)value;

            var values = numCard.ToArray();

            var sum = 0;

            for (int i = 0; i < values.Length; i++)
            {
                if (i % 2 == 0)
                {
                    sum += (values[i] * 2) > 9 ? (values[i] * 2) - 9 : values[i] * 2;
                }
                else
                {
                    sum += values[i];
                }
            }


            return (sum % 10) == 0;
        }
    }
}

