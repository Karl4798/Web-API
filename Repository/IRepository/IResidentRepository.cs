using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdMedAPI.Models;

namespace AdMedAPI.Repository.IRepository
{

    public interface IResidentRepository
    {

        ICollection<Resident> GetResidents();
        Resident GetResident(int ResidentId);
        bool ResidentExists(String id);
        bool ResidentExists(int ResidentId);
        bool CreateResident(Resident Resident);
        bool UpdateResident(Resident Resident);
        bool DeleteResident(Resident Resident);
        bool Save();

    }

}
