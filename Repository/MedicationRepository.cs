using System.Collections.Generic;
using System.Linq;
using AdMedAPI.Data;
using AdMedAPI.Models;
using AdMedAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace AdMedAPI.Repository
{
    public class MedicationRepository : IMedicationRepository
    {
        // Injected dependancy for the application database context
        private readonly ApplicationDbContext _db;

        // Constructor
        public MedicationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public ICollection<Medication> GetMedications()
        {
            return _db.Medications.Include(a => a.Resident).OrderBy(a => a.Id).ToList();
        }

        public Medication GetMedication(int medicationId)
        {
            return _db.Medications.Include(a => a.Resident).FirstOrDefault(a => a.Id == medicationId);
        }

        public bool CreateMedication(Medication medication)
        {
            _db.Medications.Add(medication);
            return Save();
        }

        public bool UpdateMedication(Medication medication)
        {
            _db.Medications.Update(medication);
            return Save();
        }

        public bool DeleteMedication(Medication medication)
        {
            _db.Medications.Remove(medication);
            return Save();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }
    }
}