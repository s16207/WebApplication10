using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace WebApplication10.Models
{
    
    public class MedDbContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Prescription_Medicament> Prescription_Medicaments { get; set; }

        public MedDbContext(DbContextOptions options): base(options) { }
        protected MedDbContext() { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            
            modelBuilder.Entity<Prescription>().HasKey(x => x.IdPrescription);
           // modelBuilder.Entity<Prescription>().HasAlternateKey(x => new { x.IdDoctor, x.IdPatient });
            modelBuilder.Entity<Prescription_Medicament>().HasKey(x => new { x.IdMedicament, x.IdPrescription });


            modelBuilder.Entity<Prescription>().HasOne(x => x.Patient).WithMany(x => x.Prescriptions).HasForeignKey(x => x.IdPatient);
            modelBuilder.Entity<Prescription>().HasOne(x => x.Doctor).WithMany(x => x.Prescriptions).HasForeignKey(x => x.IdDoctor);

            SeedData(modelBuilder);
            
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor() {  IdDoctor =1, Email ="kutas@wp.pl", FirstName = "Andrzej", LastName = "Test"},
                new Doctor() { IdDoctor = 2, Email = "dupa@wp.pl", FirstName = "Leon", LastName = "Nowak"},
                new Doctor() { IdDoctor = 3, Email = "pipppp@onet.pl", FirstName = "Piotr", LastName ="Dupa"}

                );

            modelBuilder.Entity<Patient>().HasData(
              new Patient() { IdPatient = 1, FirstName = "Andrzej", LastName = "Mobobo", Birthdate = new DateTime(1990, 12, 12) },
                new Patient() { IdPatient = 2, FirstName = "Lucjan", LastName = "Krab", Birthdate = DateTime.Now }
                );

            modelBuilder.Entity<Medicament>().HasData(
                new Medicament() { IdMedicament = 1, Name = "Aspiryna", Description = "Na wszystko", Type = "Tabs" },
                new Medicament() { IdMedicament = 2, Name = "Tabcin", Description = "Na wszystko", Type = "Pils" }
                );

            modelBuilder.Entity<Prescription>().HasData(
                   new Prescription { IdPrescription = 1, Date = DateTime.Now, DueDate = DateTime.Now, IdPatient = 1, IdDoctor = 1 },
                   new Prescription { IdPrescription = 2, Date = DateTime.Now, DueDate = DateTime.Now, IdPatient = 2, IdDoctor = 2 }
 
                   );
            modelBuilder.Entity<Prescription_Medicament>().HasData(
                new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1, Details = "duzo", Dose = 5 },
                new Prescription_Medicament { IdMedicament = 2, IdPrescription = 2, Details = "malo", Dose = 7 }

                );

        }
    }
}
