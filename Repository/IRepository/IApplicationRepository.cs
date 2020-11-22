using System.Collections.Generic;
using AdMedAPI.Models;

namespace AdMedAPI.Repository.IRepository
{
    // Application repository interface
    public interface IApplicationRepository
    {
        // All methods implemented in the repository
        ICollection<Application> GetApplications();
        Application GetApplication(int applicationId);
        bool ApplicationExists(string id);
        bool ApplicationExists(int applicationId);
        bool CreateApplication(Application application);
        bool UpdateApplication(Application application);
        bool DeleteApplication(Application application);
        bool Save();
    }
}