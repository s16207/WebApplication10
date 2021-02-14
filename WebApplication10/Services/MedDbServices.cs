using WebApplication10.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WebApplication10.Services
{
    public class MedDbServices : IDbService
    {
        private MedDbContext _context;
        public MedDbServices(MedDbContext context)
        {
            _context = context;
        }
        public Medicament GetMedWithPres(int medId)
        {
            var med = _context.Medicaments
                .Include(x => x.Prescription_Medicaments)
                .ThenInclude(x => x.Prescription)
                .FirstOrDefault(x => x.IdMedicament == medId);
            return med;
        }
        public Patient DeletePatient(int patId)
        {
            throw new NotImplementedException();
        }

}