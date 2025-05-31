using System.ComponentModel.DataAnnotations;

namespace BMI.UI.Models
{
    public class BmiRequest
    {
        [Required]
        [Range(30, 300, ErrorMessage = "Height must be between 30cm and 300cm.")]
        public double? HeightCm { get; set; }

        [Required]
        [Range(10, 500, ErrorMessage = "Weight must be between 10kg and 500kg.")]
        public double? WeightKg { get; set; }
    }
}
