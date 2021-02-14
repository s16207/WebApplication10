using WebApplication10.DTO;
using WebApplication10.Models;
using WebApplication10.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WebApplication10.Controllers
{
    /*[Route("api/[controller]")]*/
    [ApiController]
    public class MedicamentsController : ControllerBase
    {
        
        private readonly MedDbContext _context;
        private readonly IDbService _dbService;
        public MedicamentsController(MedDbContext context, IDbService dbService)
        {
            _context = context;
            _dbService = dbService;
        }
        [HttpGet]
        [Route("api/medicaments/{id}")]
        public async Task<IActionResult> GetMedAsync(int id)
        {
            var med = _dbService.GetMedWithPres(id);
            var allPresc = _context.Prescriptions;
            List<Prescription> presList = new List<Prescription>();
            var medPres = _context.Prescription_Medicaments.Where(x => x.IdMedicament == id).ToList();
            foreach (var x in medPres)
            {
                presList.Add(_context.Prescriptions.Where(x => x.IdPrescription == x.IdPrescription).FirstOrDefault());
            }
            presList.OrderBy(x => x.IdPrescription);
            var result = new MedicamentDTO()
            {
                IdMedicament = med.IdMedicament,
                Name = med.Name,
                Type = med.Type,
                Description = med.Description,
                PrescriptionDTOs = presList
          
            };
 
            return Ok(result);
        }
    }
}