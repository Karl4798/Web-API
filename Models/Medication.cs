using System.ComponentModel.DataAnnotations;

namespace AdMedAPI.Models
{

    public class Medication
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string TimeSchedule { get; set; }
        public string Notes { get; set; }
        public int ResidentId { get; set; }

    }

}