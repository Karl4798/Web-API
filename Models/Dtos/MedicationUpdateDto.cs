using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdMedAPI.Models.Dtos
{
    public class MedicationUpdateDto
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public int Quantity { get; set; }
        [Required] public string TimeSchedule { get; set; }
        public string Notes { get; set; }
        [Required] public int ResidentId { get; set; }
        [ForeignKey("ResidentId")] public virtual Resident Resident { get; set; }
    }
}