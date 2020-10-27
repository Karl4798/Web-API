using System.Collections.Generic;
using AdMedAPI.Models;

namespace AdMedAPI.Repository.IRepository
{
    public interface IResidentRepository
    {
        ICollection<Resident> GetResidents();
        Resident GetResident(int ResidentId);
        bool ResidentExists(string id);
        bool ResidentExists(int ResidentId);
        bool CreateResident(Resident Resident);
        bool UpdateResident(Resident Resident);
        bool DeleteResident(Resident Resident);
        bool Save();
    }
}