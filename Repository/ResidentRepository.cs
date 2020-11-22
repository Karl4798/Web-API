using System.Collections.Generic;
using System.Linq;
using AdMedAPI.Data;
using AdMedAPI.Models;
using AdMedAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AdMedAPI.Repository
{
    public class ResidentRepository : IResidentRepository
    {
        // Injected dependancy for the application database context
        private readonly ApplicationDbContext _db;

        // Constructor
        public ResidentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public ICollection<Resident> GetResidents()
        {
            return _db.Residents.Include(a => a.PrimaryContact).OrderBy(a => a.Id).ToList();
        }

        public Resident GetResident(int ResidentId)
        {
            return _db.Residents.Include(a => a.PrimaryContact).FirstOrDefault(a => a.Id == ResidentId);
        }

        public bool ResidentExists(string identityNumber)
        {
            bool value = _db.Residents.Any(a => a.IdentityNumber.ToLower().Trim() == identityNumber.ToLower());
            return value;
        }

        public bool ResidentExists(int ResidentId)
        {
            return _db.Residents.Any(a => a.Id == ResidentId);
        }

        public bool CreateResident(Resident Resident)
        {
            Resident.GenderString = Resident.Gender.ToString();
            _db.Residents.Add(Resident);
            return Save();
        }

        public bool UpdateResident(Resident Resident)
        {
            Resident.GenderString = Resident.Gender.ToString();
            _db.Residents.Update(Resident);
            return Save();
        }

        public bool DeleteResident(Resident Resident)
        {
            _db.Residents.Remove(Resident);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}