using Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InnValidationAttribute : ValidationAttribute
    {
        public InnValidationAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public override bool IsValid(object value)
        {
            var inn = (ulong)value;

            var values = inn.ToArray();

            switch (values.Length)
            {
                case 10:
                    #region Юр. лица

                    var coefficientsN10 = new byte[] { 2, 4, 10, 3, 5, 9, 4, 6, 8 };

                    int sumN10 = GetSumNx(values, coefficientsN10);

                    var checkNumberN10 = (sumN10 % 11) % 10;

                    return values[^1] == checkNumberN10;
                #endregion

                case 12:
                    #region Физ. лица


                    var coefficientsN11 = new byte[] { 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };
                    var coefficientsN12 = new byte[] { 3, 7, 2, 4, 10, 3, 5, 9, 4, 6, 8 };


                    var sumN11 = GetSumNx(values, coefficientsN11);

                    var checkNumberN11 = (sumN11 % 11) % 10;

                    var sumN12 = GetSumNx(values, coefficientsN12);

                    var checkNumberN12 = (sumN12 % 11) % 10;

                    return values[^2] == checkNumberN11 && values[^1] == checkNumberN12;
                #endregion

                default:
                    return false;
            }
        }
        private int GetSumNx(byte[] values, byte[] coefficientsNx)
        {
            var sumNx = 0;

            for (int i = 0; i < coefficientsNx.Length; i++)
            {
                sumNx += coefficientsNx[i] * values[i];
            }
            return sumNx;
        }

    }
}

