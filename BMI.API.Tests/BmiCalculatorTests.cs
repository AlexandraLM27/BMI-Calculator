using System;
using Xunit;
using BMI.API.Services;
using BMI.API.Models;

namespace BMI.API.Tests
{
    public class BmiCalculatorTests
    {
        [Fact]
        public void Calculate_Returns_Underweight()
        {
            var result = BmiCalculator.Calculate(180, 55); // BMI ≈ 16.98
            Assert.Equal("Underweight", result.Category);
            Assert.InRange(result.Bmi, 16.9, 17.1);
        }

        [Fact]
        public void Calculate_Returns_NormalWeight()
        {
            var result = BmiCalculator.Calculate(170, 65); // BMI ≈ 22.49
            Assert.Equal("Normal weight", result.Category);
            Assert.InRange(result.Bmi, 22.4, 22.6);
        }

        [Fact]
        public void Calculate_Returns_Overweight()
        {
            var result = BmiCalculator.Calculate(165, 75); // BMI ≈ 27.55
            Assert.Equal("Overweight", result.Category);
            Assert.InRange(result.Bmi, 27.5, 27.6);
        }

        [Fact]
        public void Calculate_Returns_Obese()
        {
            var result = BmiCalculator.Calculate(160, 100); // BMI ≈ 39.06
            Assert.Equal("Obese", result.Category);
            Assert.InRange(result.Bmi, 39.0, 39.1);
        }

        [Theory]
        [InlineData(0, 70)]
        [InlineData(-160, 70)]
        [InlineData(170, 0)]
        [InlineData(170, -80)]
        public void Calculate_Throws_For_Invalid_Inputs(double heightCm, double weightKg)
        {
            var ex = Assert.Throws<ArgumentException>(() => BmiCalculator.Calculate(heightCm, weightKg));
            Assert.Equal("Height and weight must be greater than zero.", ex.Message);
        }

        [Fact]
        public void Calculate_Throws_For_NaN_Or_Infinity()
        {
            Assert.Throws<ArgumentException>(() => BmiCalculator.Calculate(double.NaN, 70));
            Assert.Throws<ArgumentException>(() => BmiCalculator.Calculate(170, double.PositiveInfinity));
        }

    }
}
