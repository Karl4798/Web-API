using System.Collections.Generic;
using AdMedAPI.Models;

namespace AdMedAPI.Repository.IRepository
{
    // Medication repository interface
    public interface IMedicationRepository
    {
        // All methods implemented in the repository
        ICollection<Medication> GetMedications();
        Medication GetMedication(int applicationId);
        bool CreateMedication(Medication application);
        bool UpdateMedication(Medication application);
        bool DeleteMedication(Medication application);
        bool Save();
    }
}