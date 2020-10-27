using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace AdMedAPI.Models
{
    public class Application
    {
        // General information of the applicant
        [Key] public int Id { get; set; }
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
        [ForeignKey("PrimaryContactId")] public virtual PrimaryContactApplication PrimaryContact { get; set; }
        // General information of the primary contact included in PrimaryContact
    }
}