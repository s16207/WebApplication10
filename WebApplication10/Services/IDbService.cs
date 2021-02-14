using WebApplication10.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WebApplication10.Services
{
    public interface IDbService
    {
        public Medicament GetMedWithPres(int medId);
        public Patient DeletePatient(int patId);
    }
}