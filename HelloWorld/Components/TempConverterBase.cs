using Microsoft.AspNetCore.Components;
using System;

namespace HelloWorld.Components
{
    /// <summary>
    /// a very unit-testable base class.  
    /// Not pure functions
    /// </summary>
    public abstract class TempConverterBase : ComponentBase
    {
        /// <summary>
        /// get called back when temperature changes, and receive the new temperature as both degrees Censius and Fahrenheit.
        /// </summary>
        [Parameter]
        public Action<TemperatureData> TemperatureChanged { get; set; }


        protected decimal ConvertCelsiusToFahrenheit(decimal celsius)
        {
            // was ((x /5) * 9) + 32.  Is this right?
            return (celsius * 1.8M) + 32.0M;
        }

        protected decimal ConvertFahrenheitToCelsius(decimal fahrenheit)
        {
            return (fahrenheit - 32.0M) / 1.8M;
        }

        /// <summary>
        /// fires the <see cref="TemperatureChanged"/> callback providing it with <paramref name="celsius"/> and <paramref name="fahrenheit"/>
        /// </summary>
        /// <param name="celsius">must match the equivalent <paramref name="fahrenheit"/>.</param>
        /// <param name="fahrenheit">must match the equivalent <paramref name="celsius"/>.</param>
        protected void FireTemperatureChangedCallback(decimal celsius, decimal fahrenheit)
        {
            this.TemperatureChanged?.Invoke(new TemperatureData { Celcius = celsius, Fahrenheit = fahrenheit });
        }
    }

    /// <summary>
    /// C#9 feature.  Better than a Tuple in this case.  A little more formal.
    /// </summary>
    public class TemperatureData
    {
        public decimal Celcius { get; set; }
        public decimal Fahrenheit { get; set; }
    }
}
