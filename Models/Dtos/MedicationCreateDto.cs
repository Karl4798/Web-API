using System.ComponentModel.DataAnnotations;

namespace AdMedAPI.Models.Dtos
{
    public class MedicationCreateDto
    {
        [Required] public string Name { get; set; }
        [Required] public int Quantity { get; set; }
        [Required] public string TimeSchedule { get; set; }
        public string Notes { get; set; }
        [Required] public int ResidentId { get; set; }
    }
}