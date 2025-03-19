// Arquivo: CrudMedicine.cs
using System;
using System.Collections.Generic;
using System.Linq;
using FarmacySystem.model;
using FarmacySystem.data;

namespace FarmacySystem.controller
{
    public class CrudMedicine
    {
        public void InsertMedicine(int idM, string nameM, string descriptionM, string typeM, decimal priceM, DateTime expiration_dateM)
        {
            using (var db = new AppDbContext())
            {
                db.Medicines.Add(new Medicine 
                { 
                    Name = nameM, 
                    Description = descriptionM, 
                    Type = typeM, 
                    Price = priceM, 
                    ExpirationDate = expiration_dateM 
                });
                db.SaveChanges();
            }
        }

        public List<Medicine> ListMedicines()
        {
            using (var db = new AppDbContext())
            {
                return db.Medicines.ToList(); // Retorna a lista de objetos Medicine
            }
        }

        public void MedicinesUpdate(int id, string? Newname = null, string? Newdescription = null, string? Newtype = null, decimal? Newprice = null, DateTime? Newexpiration_date = null)
        {
            using (var db = new AppDbContext())
            {
                var medicine = db.Medicines.Find(id);
                if (medicine != null)
                {
                    medicine.Name = Newname ?? medicine.Name;
                    medicine.Description = Newdescription ?? medicine.Description;
                    medicine.Type = Newtype ?? medicine.Type;
                    medicine.Price = Newprice ?? medicine.Price;
                    medicine.ExpirationDate = Newexpiration_date ?? medicine.ExpirationDate;
                    db.SaveChanges();
                }
            }
        }

        public void MedicinesDelete(int id)
        {
            using (var db = new AppDbContext())
            {
                var medicine = db.Medicines.Find(id);
                if (medicine != null)
                {
                    db.Medicines.Remove(medicine);
                    db.SaveChanges();
                }
            }
        }
    }
}