using System.Collections.Generic;
using AdMedAPI.Models;

namespace AdMedAPI.Repository.IRepository
{
    public interface IMedicationRepository
    {
        ICollection<Medication> GetMedications();
        Medication GetMedication(int applicationId);
        bool CreateMedication(Medication application);
        bool UpdateMedication(Medication application);
        bool DeleteMedication(Medication application);
        bool Save();
    }
}