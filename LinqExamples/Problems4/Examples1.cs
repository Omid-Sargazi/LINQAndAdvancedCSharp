using LinqExamples.Problem1;
using LinqExamples.Problems3;

namespace LinqExamples.Problems4
{
    public class LinqOfExamples
    {
        public static void Execute()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "لپ‌تاپ", Category = "الکترونیکی", Price = 15000000, StockQuantity = 10, MinimumStockLevel = 5 },
                new Product { Id = 2, Name = "ماوس", Category = "الکترونیکی", Price = 500000, StockQuantity = 50, MinimumStockLevel = 20 },
                new Product { Id = 3, Name = "کیبورد", Category = "الکترونیکی", Price = 800000, StockQuantity = 30, MinimumStockLevel = 15 },
                new Product { Id = 4, Name = "مانیتور", Category = "الکترونیکی", Price = 8000000, StockQuantity = 8, MinimumStockLevel = 10 },
                new Product { Id = 5, Name = "هندزفری", Category = "الکترونیکی", Price = 1200000, StockQuantity = 25, MinimumStockLevel = 10 },
                new Product { Id = 6, Name = "کابل HDMI", Category = "الکترونیکی", Price = 300000, StockQuantity = 100, MinimumStockLevel = 50 }
            };

            var sales = new List<Sale>
            {
                new Sale { Id = 1, ProductId = 1, SaleDate = DateTime.Now.AddDays(-10), Quantity = 2, TotalAmount = 30000000 },
                new Sale { Id = 2, ProductId = 2, SaleDate = DateTime.Now.AddDays(-8), Quantity = 5, TotalAmount = 2500000 },
                new Sale { Id = 3, ProductId = 3, SaleDate = DateTime.Now.AddDays(-5), Quantity = 3, TotalAmount = 2400000 },
                new Sale { Id = 4, ProductId = 1, SaleDate = DateTime.Now.AddDays(-3), Quantity = 1, TotalAmount = 15000000 },
                new Sale { Id = 5, ProductId = 5, SaleDate = DateTime.Now.AddDays(-1), Quantity = 4, TotalAmount = 4800000 },
                new Sale { Id = 6, ProductId = 2, SaleDate = DateTime.Now.AddDays(-1), Quantity = 8, TotalAmount = 4000000 }
            };


            // var StockLessThanMinimumStock = products.Where(p => p.StockQuantity < p.MinimumStockLevel)
            // .Select(p => new { Name = p.Name, StockQty = p.StockQuantity, MinQty = p.MinimumStockLevel, Shortage = p.MinimumStockLevel - p.StockQuantity });

            // var highSellingProducts = sales.GroupBy(s => s.ProductId).Where(g => g.Sum(s => s.Quantity) > 5).Select(p => p.Key);
            // var lowStockProducts = products.Where(p => p.StockQuantity < 15).Select(p => p.Id);

            // var needReorder = products.Where(p => p.StockQuantity < p.MinimumStockLevel).Select(p => new
            // {
            //     p.Name,
            //     p.StockQuantity,
            //     p.MinimumStockLevel,
            //     Shortage = p.MinimumStockLevel - p.StockQuantity
            // });
            // // اتحاد: محصولاتی که یا پرفروش هستند یا موجودی کم دارند
            // var highSellingOrLowStock = highSellingProducts.Union(lowStockProducts).Join(products,id=>id,p=>p.Id,(id,p)=>p.Name);


            var needReorder = products.Where(p => p.StockQuantity < p.MinimumStockLevel).Select(p => new
            {
                p.Name,
                p.StockQuantity,
                p.MinimumStockLevel,
                Shortage = p.MinimumStockLevel - p.StockQuantity
            });

            var highSellingProductIds = sales.GroupBy(s => s.ProductId)
            .Where(g => g.Sum(s => s.Quantity) >= 5)
            .Select(s => s.Key);

            var lowStockProductIds = products.Where(p => p.StockQuantity < 20).Select(p => p.Id);

            var highSellingOrLowStock = highSellingProductIds.Union(lowStockProductIds).Join(products, id => id, p => p.Id, (id, p) => p.Name).Distinct();

            var highSellingAndLowStock = highSellingProductIds.Intersect(lowStockProductIds).Join(products, id => id, p => p.Id, (id, p) => new
            {
                p.Name,
                p.StockQuantity,
                TotalSold = sales.Where(s => s.ProductId == id).Sum(s => s.Quantity)
            });


            var patients = new List<Patient>
{
    new Patient { Id = 1, Name = "فاطمه احمدی", Age = 35, Gender = "Female", BloodType = "A+", RegistrationDate = new DateTime(2023, 2, 10) },
    new Patient { Id = 2, Name = "علی محمدی", Age = 28, Gender = "Male", BloodType = "O+", RegistrationDate = new DateTime(2023, 5, 15) },
    new Patient { Id = 3, Name = "سارا کریمی", Age = 42, Gender = "Female", BloodType = "B+", RegistrationDate = new DateTime(2022, 11, 20) },
    new Patient { Id = 4, Name = "رضا حسینی", Age = 65, Gender = "Male", BloodType = "AB+", RegistrationDate = new DateTime(2024, 1, 5) },
    new Patient { Id = 5, Name = "نازنین جعفری", Age = 19, Gender = "Female", BloodType = "A-", RegistrationDate = new DateTime(2023, 8, 30) }
};

        var doctors = new List<Doctor>
        {
            new Doctor { Id = 1, Name = "دکتر شریفی", Specialization = "قلب", ConsultationFee = 500000 },
            new Doctor { Id = 2, Name = "دکتر امیری", Specialization = "اعصاب", ConsultationFee = 450000 },
            new Doctor { Id = 3, Name = "دکتر محمودی", Specialization = "گوارش", ConsultationFee = 400000 },
            new Doctor { Id = 4, Name = "دکتر علیزاده", Specialization = "قلب", ConsultationFee = 550000 }
        };

        var appointments = new List<Appointment>
        {
            new Appointment { Id = 1, PatientId = 1, DoctorId = 1, AppointmentDate = new DateTime(2024, 1, 10), Status = "Completed", Diagnosis = "فشار خون", FeePaid = 500000 },
            new Appointment { Id = 2, PatientId = 1, DoctorId = 1, AppointmentDate = new DateTime(2024, 1, 25), Status = "Scheduled", Diagnosis = "", FeePaid = 0 },
            new Appointment { Id = 3, PatientId = 2, DoctorId = 2, AppointmentDate = new DateTime(2024, 1, 12), Status = "Completed", Diagnosis = "میگرن", FeePaid = 450000 },
            new Appointment { Id = 4, PatientId = 3, DoctorId = 1, AppointmentDate = new DateTime(2024, 1, 15), Status = "Completed", Diagnosis = "آریتمی", FeePaid = 500000 },
            new Appointment { Id = 5, PatientId = 4, DoctorId = 4, AppointmentDate = new DateTime(2024, 1, 18), Status = "Completed", Diagnosis = "نارسایی قلبی", FeePaid = 550000 },
            new Appointment { Id = 6, PatientId = 5, DoctorId = 3, AppointmentDate = new DateTime(2024, 1, 20), Status = "Cancelled", Diagnosis = "", FeePaid = 0 },
            new Appointment { Id = 7, PatientId = 2, DoctorId = 2, AppointmentDate = new DateTime(2024, 1, 28), Status = "Completed", Diagnosis = "استرس", FeePaid = 450000 }
        };

            var prescriptions = new List<Prescription>
        {
            new Prescription { Id = 1, AppointmentId = 1, MedicineName = "Losartan", Dosage = "50mg", DurationDays = 30, MedicineCost = 200000 },
            new Prescription { Id = 2, AppointmentId = 1, MedicineName = "Aspirin", Dosage = "80mg", DurationDays = 90, MedicineCost = 50000 },
            new Prescription { Id = 3, AppointmentId = 3, MedicineName = "Sumatriptan", Dosage = "50mg", DurationDays = 15, MedicineCost = 300000 },
            new Prescription { Id = 4, AppointmentId = 4, MedicineName = "Metoprolol", Dosage = "25mg", DurationDays = 60, MedicineCost = 150000 },
            new Prescription { Id = 5, AppointmentId = 5, MedicineName = "Digoxin", Dosage = "0.25mg", DurationDays = 90, MedicineCost = 250000 },
            new Prescription { Id = 6, AppointmentId = 7, MedicineName = "Alprazolam", Dosage = "0.5mg", DurationDays = 30, MedicineCost = 180000 }
        };


            var doctorPerformance = doctors.Select(doctor => new
            {
                Doctor = doctor.Name,
                Specialization = doctor.Specialization,
                CompletedAppointments = appointments.Count(a => a.DoctorId == doctor.Id && a.Status == "Completed"),
                ConsultationIncome = appointments.Where(a => a.DoctorId == doctor.Id && a.Status == "Completed").Sum(a => a.FeePaid),
                MedicineIncome = appointments.Where(a => a.DoctorId == doctor.Id && a.Status == "Completed").Join(prescriptions, a => a.Id, p => p.AppointmentId, (a, p) => p.MedicineCost).Sum(),
                AverageTreatmentCost = appointments.Where(a => a.DoctorId == doctor.Id && a.Status == "Completed").Select(a => a.FeePaid + prescriptions.Where(p => p.AppointmentId == a.Id).Sum(p => p.MedicineCost)).DefaultIfEmpty(0).Average()
            }).Where(d => d.CompletedAppointments > 0).OrderByDescending(d => d.ConsultationIncome + d.MedicineIncome);

            var patientAnalysis = patients.Select(patient => new
            {
                Patient = patient.Name,
                AgeGroup = patient.Age < 30 ? "Young" : patient.Age <= 50 ? "Old" : "eldest",
                TotalAppointments = appointments.Count(a => a.PatientId == patient.Id),
                TotalCost = appointments.Where(a => a.PatientId == patient.Id && a.Status == "Completed").Sum(a => a.FeePaid + prescriptions.Where(p => p.AppointmentId == a.Id).Sum(p => p.MedicineCost))
            }).OrderByDescending(p => p.TotalCost);

            var medicinePopular = prescriptions.GroupBy(p => p.MedicineName).Select(g => new
            {
                MedicineName = g.Key,
                PrescriptionCount = g.Count(),
                TotalPatients = g.Select(p => p.AppointmentId).Distinct().Count(),
                TotalCost = g.Sum(p => p.MedicineCost),
                AveDuration = g.Average(p => p.DurationDays),
                PrescribingDoctors = g.Select(p => p.AppointmentId).Join(appointments,
appointmentId => appointmentId, appointment => appointment.Id, (appointmentId, appointment) => appointment.DoctorId).Distinct()
.Join(doctors, doctorId => doctorId, doctor => doctor.Id, (doctorId, doctor) => doctor.Name).Distinct().ToList()
            }).OrderByDescending(m => m.PrescriptionCount).ThenByDescending(m => m.TotalCost);

            
            


        }

    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int MinimumStockLevel { get; set; }
    }

    public class Sale
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime SaleDate { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
    }


    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; } // "Male", "Female"
        public string BloodType { get; set; }
        public DateTime RegistrationDate { get; set; }
    }

    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }
        public decimal ConsultationFee { get; set; }
    }


    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } // "Scheduled", "Completed", "Cancelled", "NoShow"
        public string Diagnosis { get; set; }
        public decimal FeePaid { get; set; }
    }
    public class Prescription
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public string MedicineName { get; set; }
        public string Dosage { get; set; }
        public int DurationDays { get; set; }
        public decimal MedicineCost { get; set; }
    }



}