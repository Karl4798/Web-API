using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static AdMedAPI.Models.Resident;

namespace AdMedAPI.Models.Dtos
{

    public class ResidentCreateDto

    {

        // General information of the resident
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        [Required] public Enums.Genders Gender { get; set; }
        public string GenderString { get; set; }
        [Required] public string Allergies { get; set; }
        [Required] public DateTime DateOfBirth { get; set; }
        [Required] public string IdentityNumber { get; set; }
        [Required] public string MedicalAid { get; set; }
        public string MedicalAidNumber { get; set; }
        public string DoctorName { get; set; }
        public string HomeTelephoneNumber { get; set; }
        public string WorkTelephoneNumber { get; set; }
        [Required] public string CellTelephoneNumber { get; set; }
        [Required] public string Undertaker { get; set; }
        [Required] public string UndertakerTelephoneNumber { get; set; }
        [Required] public string PharmacyName { get; set; }
        [Required] public string PharmacyTelephoneNumber { get; set; }
        [Required] public string PharmacyFaxNumber { get; set; }
        [Required] public int PrimaryContactId { get; set; }
        [ForeignKey("PrimaryContactId")] public virtual PrimaryContactResident PrimaryContact { get; set; }
        public int MedicationId { get; set; }
        [ForeignKey("MedicationId")] public virtual Medication Medication { get; set; }

        // General information of the primary contact included in PrimaryContact

    }

}
