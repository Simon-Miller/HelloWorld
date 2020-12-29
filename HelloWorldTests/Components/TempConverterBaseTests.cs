using HelloWorld.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelloWorldTests.Components
{
    [TestClass]
    public class TempConverterBaseTests
    {
        [TestMethod]
        public void CanConvertCelsiusToFahrenheit()
        {
            // Arrange
            var target = new testClass();

            // Act
            var result = target.ConvertToFahrenheit(0.0M);

            // Assert
            Assert.AreEqual(32.0M, result);
        }


        private class testClass : TempConverterBase
        {
            public decimal ConvertToFahrenheit(decimal celsius)
            {
                return base.ConvertCelsiusToFahrenheit(celsius);
            }

            public decimal ConvertToCelsius(decimal fehrenheit)
            {
                return base.ConvertFahrenheitToCelsius(fehrenheit);
            }
        }
    }

}
