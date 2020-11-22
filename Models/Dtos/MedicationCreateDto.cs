using System.ComponentModel.DataAnnotations;

namespace AdMedAPI.Models.Dtos
{
    // MedicationCreate Dto
    public class MedicationCreateDto
    {
        // Medication information
        [Required] public string Name { get; set; }
        [Required] public int Quantity { get; set; }
        [Required] public string TimeSchedule { get; set; }
        public string Notes { get; set; }
        [Required] public int ResidentId { get; set; }
    }
}