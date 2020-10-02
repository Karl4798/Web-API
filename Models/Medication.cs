using System.ComponentModel.DataAnnotations;

namespace AdMedAPI.Models
{

    public class Medication
    {

        [Key]
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public int Quantity { get; set; }
        [Required] public string TimeSchedule { get; set; }
        [Required] public string Notes { get; set; }
        [Required] public int ResidentId { get; set; }

    }

}