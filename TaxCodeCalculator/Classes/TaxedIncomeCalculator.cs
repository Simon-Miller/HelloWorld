using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaxCodeCalculator.Classes
{
    public class TaxedIncomeCalculator
    {
        const double PERSONAL_ALLOWANCE = 12500; // as of 2020
        const double HIGHER_RATE_FROM = 50001;
        const double HIGHER_RATE_TO = 150000;

        const double STANDARD_RATE_PERCENT = 0.20;
        const double HIGHER_RATE_PERCENT = 0.40;
        const double UPPER_RATE_PERCENT = 0.45;

        /// <summary>
        /// calculates the amount of tax you need to pay given your income, and tax code.
        /// </summary>
        public (double tax, double wage) IncomeAfterTax(string taxCode, double income)
        {
            double number = getNumberPartOfTaxCode(taxCode);
            string letters = getLettersPartOfTaxCode(taxCode);

            var allowance = getAllowance(number, letters);
            var tax = getTaxOnIncome(allowance, income);

            return (tax, income-tax);
        }

        private double getNumberPartOfTaxCode(string taxCode)
        {
            var rx = new Regex(@"^[0-9]{1,}");

            var numberPart = rx.Match(taxCode).Value;
            var numberValue = PERSONAL_ALLOWANCE;

            if (numberPart != null)
                numberValue = double.Parse(numberPart);

            return numberValue;
        }

        private string getLettersPartOfTaxCode(string taxCode)
        {
            var rx = new Regex(@"[A-Z]{1,2}$");

            return rx.Match(taxCode).Value;
        }

        private double getAllowance(double number, string letters)
        {
            var codeAllowance = number * 10.0;

            switch(letters)
            {
                case "L": return codeAllowance;

                // married parter gave you 10% of their allowance.
                case "M": return codeAllowance + (PERSONAL_ALLOWANCE * 0.1);

                // you've transfered 10% to your married partner.  So your number part should be less than 1250.
                case "N": return codeAllowance;

                // tax code contains other variables in calculating taxable income.
                case "T": return codeAllowance;

                default: throw new NotImplementedException();
            }
        }

        private double getTaxOnIncome(double allowance, double income)
        {
            double taxSubTotal = 0;

            if(income > allowance)
            {
                var taxableIncome = income - allowance;

            

                if(income > allowance && income < HIGHER_RATE_FROM)
                {
                    var standardRateTaxable = taxableIncome * STANDARD_RATE_PERCENT;
                    taxSubTotal += standardRateTaxable;
                }
                else
                {
                    // higher than standard rate of tax.
                    var standardRateTaxable = HIGHER_RATE_FROM - 12500;
                    var tax = standardRateTaxable * STANDARD_RATE_PERCENT;
                    taxSubTotal += tax;

                    taxableIncome -= standardRateTaxable;

                    if (income <= HIGHER_RATE_TO)
                    {
                        // rest of income falls inside the higher rate.
                        var higherRateTax = taxableIncome * HIGHER_RATE_PERCENT;
                        taxSubTotal += higherRateTax;
                    }
                    else
                    {
                        //rest of income includes bother higher rate, and upper rate.
                        var higherRateRange = HIGHER_RATE_TO - HIGHER_RATE_FROM;
                        var higherRateTaxOnly = higherRateRange * HIGHER_RATE_PERCENT;
                        taxSubTotal += higherRateTaxOnly;

                        taxableIncome -= higherRateRange;

                        var upperRateTaxOnly = taxableIncome * UPPER_RATE_PERCENT;
                        taxSubTotal += upperRateTaxOnly;
                    }
                }
            }

            return taxSubTotal;
        }
    }
}
