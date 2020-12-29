using System;
using System.ComponentModel.DataAnnotations;

namespace TaxCodeCalculator.Classes
{
    public class TaxCodeViewModel
    {
        public event Action<string, string> CodeChanged;

        [Required]
        [RegularExpression(@"^[0-9]{4,10}[LMNT]$", ErrorMessage = "code should be 4 or more digits followed by an L.")]
        public string Code 
        {
            get => this.code;
            set
            {
                var oldValue = this.code;
                this.code = value;
                this.CodeChanged?.Invoke(oldValue, value);
            }
        } 
        private string code = "1000L";

        public event Action<double, double> IncomeChanged;

        [Required]
        [RegularExpression(@"^[0-9]{0,}.{0,1}[0-9]{0,2}$", ErrorMessage = "Income accepts any number with up to 2 decimal places.")]
        public double Income 
        {
            get => this.income;
            set
            {
                var oldvalue = this.income;
                this.income = value;

                this.IncomeChanged?.Invoke(oldvalue, value);
            }
        }
        private double income = 0;
    }
}
