using BMI.API.Models;

namespace BMI.API.Services
{
    public static class BmiCalculator
    {
        /// <summary>
        /// Calculează BMI-ul și determină categoria.
        /// </summary>
        /// <param name="heightCm">Înălțimea în cm</param>
        /// <param name="weightKg">Greutatea în kg</param>
        /// <returns>Un obiect cu BMI-ul calculat și categoria</returns>
        public static BmiResponse Calculate(double heightCm, double weightKg)
        {
            if (double.IsNaN(heightCm) || double.IsNaN(weightKg) ||
                double.IsInfinity(heightCm) || double.IsInfinity(weightKg))
            {
                throw new ArgumentException("Height and weight must be numeric values.");
            }

            if (heightCm <= 0 || weightKg <= 0)
            {
                throw new ArgumentException("Height and weight must be greater than zero.");
            }

            double heightM = heightCm / 100.0;
            double bmi = weightKg / (heightM * heightM);

            string category = bmi switch
            {
                < 18.5 => "Underweight",
                >= 18.5 and < 25 => "Normal weight",
                >= 25 and < 30 => "Overweight",
                _ => "Obese"
            };

            return new BmiResponse
            {
                Bmi = Math.Round(bmi, 2),
                Category = category
            };
        }
    }
}
