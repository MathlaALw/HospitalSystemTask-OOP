using System.Runtime.CompilerServices;

namespace HospitalSystemTask_OOP
{
    public class Program
    {

        // Create a new Hospital object to manage the system
        static Hospital hospital = new Hospital();

        // save appointments to a file
        static string appointmentsFilePath = "appointments.txt";
        static string doctorsFilePath = "doctors.txt";
        static string patientsFilePath = "patients.txt";


        static void Main(string[] args)
        {

            
            LoadDoctors();
            LoadPatients();
            LoadAppointments();
            Console.Clear();
            while (true)
            {
                
                
                Console.WriteLine("Hospital System Menu");
                Console.WriteLine("1. Add Doctor");
                Console.WriteLine("2. Add Patient");
                Console.WriteLine("3. Book Appointment");
                Console.WriteLine("4. Show Appointments");
                Console.WriteLine("5. Search for Appointment by Patient ID and Date");
                Console.WriteLine("6. Add New Appointment Time for Doctor");
                Console.WriteLine("7. View All Appointment Times for Doctor");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": AddDoctor(); break;
                    case "2": AddPatient(); break;
                    case "3": BookAppointment(); break;
                    case "4": ShowAppointments(); break;
                    case "5": SearchAppointments(); break;
                    case "6": AddNewAppointmentTimeforDoctor(); break;
                    case "7": ViewAllAppointmentsTimesForDoctor(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid option. Try again."); break;
                }
            }



        } // End of Main method



        // Method to add a Doctor
        public static void AddDoctor()
        {
            Console.Clear();
            Console.WriteLine("Add New Doctor");
            Doctor doc = new Doctor();
            Console.Write("Doctor ID: ");
            doc.Id = int.Parse(Console.ReadLine());
            Console.Write("Name: ");
            doc.Name = Console.ReadLine();
            Console.Write("Age: ");
            doc.Age = int.Parse(Console.ReadLine());
            Console.Write("Specialization: ");
            doc.Specialization = Console.ReadLine();
            Console.WriteLine("Enter Number of Available Times: ");
            int count = int.Parse(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                Console.Write("Enter time (yyyy-MM-dd HH:mm): ");
                DateTime dt;
                while (!DateTime.TryParse(Console.ReadLine(), out dt))
                {
                    Console.Write("Invalid format. Try again (yyyy-MM-dd HH:mm): ");
                }
                doc.AvailableAppointments.Add(dt);
            }

            hospital.Doctors.Add(doc);
            // Save the doctor to the file
            SaveDoctors();
            Console.WriteLine("Doctor added.\n");
        }


        public static void AddPatient()
        {
            Console.Clear();
            Console.WriteLine("Add New Patient");
            Patient pat = new Patient();
            Console.Write("Patient ID: ");
            pat.Id = int.Parse(Console.ReadLine());
            do
            {
                Console.Write("Patient ID : ");
                string idInput = Console.ReadLine();
                if (!int.TryParse(idInput, out pat.Id) || pat.Id <= 0)
                {
                    Console.WriteLine("Invalid ID. Please enter a positive integer.");
                }
            } while (pat.Id <= 0);
            do
            {
                Console.Write("Name: ");
                pat.Name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(pat.Name))
                {
                    Console.WriteLine("Invalid name. Please enter a non-empty name.");
                }
            } while (string.IsNullOrWhiteSpace(pat.Name));



            do
            {
                Console.Write("Age: ");
                string ageInput = Console.ReadLine();
                if (!int.TryParse(ageInput, out pat.Age) || pat.Age <= 0)
                {
                    Console.WriteLine("Invalid age. Please enter a positive integer.");
                }
            } while (pat.Age <= 0);


            do
            {
                Console.Write("Phone number : ");
                pat.PhoneNumber = Console.ReadLine();
                if (pat.PhoneNumber.Length != 8 || !(pat.PhoneNumber.StartsWith("9") || pat.PhoneNumber.StartsWith("7")) || !long.TryParse(pat.PhoneNumber, out _))
                {
                    Console.WriteLine("Invalid phone number. Please try again ( should be 8 digits , start with 9 or 7) .");
                }
            } while (pat.PhoneNumber.Length != 8 || !(pat.PhoneNumber.StartsWith("9") || pat.PhoneNumber.StartsWith("7")) || !long.TryParse(pat.PhoneNumber, out _));



            hospital.Patients.Add(pat);
            // Save the patient to the file
            SavePatients();
            Console.WriteLine("Patient added.\n");
        }



        public static void BookAppointment()
        {
            Console.Clear();
            Console.WriteLine("Book Appointment");
            // view all doctors
            Console.WriteLine("Available Doctors:");
            foreach (var doctor in hospital.Doctors)
            {
                doctor.DisplayInfo();
                // view available times for each doctor


                Console.WriteLine("Available Times: " + string.Join(", ", doctor.AvailableAppointments));
            }
            Console.WriteLine("Choose a doctor to book an appointment.\n");
            Console.Write("Doctor ID: ");
            int docId = int.Parse(Console.ReadLine());
            Doctor doc = hospital.Doctors.Find(d => d.Id == docId);
            if (doc == null)
            {
                Console.WriteLine("Doctor not found.\n");
                return;
            }
            else if (doc.AvailableAppointments.Count == 0)
            {
                Console.WriteLine("Doctor has no available appointments.\n");
                return;
            }



            Console.Write("Patient ID: ");
            int patId = int.Parse(Console.ReadLine());
            Patient pat = hospital.Patients.Find(p => p.Id == patId);

            Console.Write("Appointment time (yyyy-MM-dd HH:mm): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            if (!doc.AvailableAppointments.Contains(date))
            {
                Console.WriteLine("Doctor not available at this time.\n");
                return;
            }

            bool booked = hospital.Appointments.Exists(a => a.Doctor.Id == docId && a.AppointmentDate == date);
            if (booked)
            {
                Console.WriteLine("Time already booked.\n");
                return;
            }

            Appointment appt = new Appointment();
            appt.AppointmentId = hospital.Appointments.Count + 1;
            appt.Doctor = doc;
            appt.Patient = pat;
            appt.AppointmentDate = date;
            appt.Status = "Booked";
            // Remove the booked time from doctor available times
            doc.AvailableAppointments.Remove(date);
            // Save the updated doctor to the file
            SaveDoctors();

            hospital.Appointments.Add(appt);
            // Save the appointment to the file
            SaveAppointments();
            Console.WriteLine("Appointment booked.\n");
        }

        public static void ShowAppointments()
        {
            Console.Clear();
            Console.WriteLine("Show Appointments");
            if (hospital.Appointments.Count == 0)
            {
                Console.WriteLine("No appointments found.\n");
                return;
            }

            Console.WriteLine("List of Appointments:");
            
            for (int i = 0; i < hospital.Appointments.Count; i++)
            {
                //var a = hospital.Appointments[i];
                Console.WriteLine($"Appointment {i + 1}: Doctor {hospital.Appointments[i].Doctor.Name}, Patient {hospital.Appointments[i].Patient.Name}, Date: {hospital.Appointments[i].AppointmentDate:yyyy-MM-dd HH:mm}, Status: {hospital.Appointments[i].Status}");
            }
            Console.WriteLine();

        }

        // Method to search for an appointment by patient name or date
        public static void SearchAppointments()
        {
            Console.Clear();
            Console.WriteLine("Search Appointments:");
            Console.WriteLine("1. By Patient Name");
            Console.WriteLine("2. By Date");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Enter patient name to search: ");
                string name = Console.ReadLine().ToLower();
                var results = hospital.Appointments.Where(a => a.Patient.Name.ToLower().Contains(name));
                foreach (var a in results)
                {
                    Console.WriteLine($"Appointment {a.AppointmentId}: Doctor {a.Doctor.Name}, Patient {a.Patient.Name}, Date: {a.AppointmentDate}, Status: {a.Status}");
                }
            }
            else if (choice == "2")
            {
                Console.Write("Enter date (yyyy-MM-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime searchDate))
                {
                    var results = hospital.Appointments.Where(a => a.AppointmentDate.Date == searchDate.Date);
                    foreach (var a in results)
                    {
                        Console.WriteLine($"Appointment {a.AppointmentId}: Doctor {a.Doctor.Name}, Patient {a.Patient.Name}, Date: {a.AppointmentDate}, Status: {a.Status}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid date format.");
                }
            }
            else
            {
                Console.WriteLine("Invalid option.");
            }
            Console.WriteLine();
        }

        // Method to add a new appointment time for a doctor
        public static void AddNewAppointmentTimeforDoctor()
        {
            Console.Clear();
            Console.WriteLine("Add New Appointment Time for Doctor");
            // view all doctors
            Console.WriteLine("Available Doctors:");
            foreach (var doctor in hospital.Doctors)
            {
                doctor.DisplayInfo();
                
            }
            Console.Write("Enter Doctor ID : ");
            int docId = int.Parse(Console.ReadLine());
            Doctor doc = hospital.Doctors.Find(d => d.Id == docId);
            if (doc == null)
            {
                Console.WriteLine("Doctor not found.\n");
                return;
            }
            
            Console.Write("Enter new appointment time (yyyy-MM-dd HH:mm): ");
            DateTime newTime;
            while (!DateTime.TryParse(Console.ReadLine(), out newTime))
            {
                Console.Write("Invalid format. Try again (yyyy-MM-dd HH:mm): ");
            }
            if (doc.AvailableAppointments.Contains(newTime))
            {
                Console.WriteLine("This time is already available.\n");
                return;
            }
            doc.AvailableAppointments.Add(newTime);
            // Save the updated doctor to the file
            SaveDoctors();
            Console.WriteLine("New appointment time added for doctor.\n");
        }


        // Method to view all appointment times for a doctor
        public static void ViewAllAppointmentsTimesForDoctor()
        {
            Console.Clear();
            Console.WriteLine("View All Appointment Times for Doctor");
            Console.Write("Doctor ID: ");
            int docId = int.Parse(Console.ReadLine());
            Doctor doc = hospital.Doctors.Find(d => d.Id == docId);
            if (doc == null)
            {
                Console.WriteLine("Doctor not found.\n");
                return;
            }
            if (doc.AvailableAppointments.Count == 0)
            {
                Console.WriteLine("No available appointment times for this doctor.\n");
                return;
            }
            Console.WriteLine("Available Appointment Times:");
            foreach (var time in doc.AvailableAppointments)
            {
                Console.WriteLine(time.ToString("yyyy-MM-dd HH:mm"));
            }
            if (hospital.Appointments.Count > 0)
            {
                Console.WriteLine("Booked Appointment Times:");
                foreach (var appt in hospital.Appointments.Where(a => a.Doctor.Id == docId))
                {
                    Console.WriteLine($"{appt.AppointmentDate:yyyy-MM-dd HH:mm} - {appt.Status}");
                }
            }
            else
            {
                Console.WriteLine("No booked appointments for this doctor.");
            }
            Console.WriteLine();
        }


        // Method to save appointments to a file
        public static void SaveAppointments()
        {
            using (StreamWriter writer = new StreamWriter(appointmentsFilePath))
            {
                foreach (var a in hospital.Appointments)
                {
                    writer.WriteLine($"{a.AppointmentId}|{a.Doctor.Id}|{a.Doctor.Name}|{a.Patient.Id}|{a.Patient.Name}|{a.AppointmentDate}|{a.Status}");
                }
            }
            Console.WriteLine("Appointments saved to file.");
        }



        // Method to load appointments from a file
        public static void LoadAppointments()
        {
            if (!File.Exists(appointmentsFilePath))
            {
                Console.WriteLine("No appointments file found.");
                return;
            }
            using (StreamReader reader = new StreamReader(appointmentsFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split('|');
                    if (parts.Length < 7) continue; // Skip invalid lines
                    Appointment appt = new Appointment
                    {
                        AppointmentId = int.Parse(parts[0]),
                        Doctor = hospital.Doctors.Find(d => d.Id == int.Parse(parts[1])),
                        Patient = hospital.Patients.Find(p => p.Id == int.Parse(parts[3])),
                        AppointmentDate = DateTime.Parse(parts[5]),
                        Status = parts[6]
                    };
                    hospital.Appointments.Add(appt);
                }
            }
            Console.WriteLine("Appointments loaded from file.");

            //if (!File.Exists(appointmentsFilePath))
            //{
            //    Console.WriteLine("No appointments file found.");
            //    return;
            //}

            //using (StreamReader reader = new StreamReader(appointmentsFilePath))
            //{
            //    string line;
            //    while ((line = reader.ReadLine()) != null)
            //    {
            //        var parts = line.Split('|');
            //        if (parts.Length < 7) continue; // Skip invalid lines

            //        // Try to find doctor and patient by ID
            //        int docId = int.Parse(parts[1]);
            //        int patId = int.Parse(parts[3]);

            //        Doctor doc = hospital.Doctors.Find(d => d.Id == docId);
            //        Patient pat = hospital.Patients.Find(p => p.Id == patId);

            //        // If either doctor or patient not found, skip this appointment
            //        if (doc == null || pat == null) continue;

            //        Appointment appt = new Appointment
            //        {
            //            AppointmentId = int.Parse(parts[0]),
            //            Doctor = doc,
            //            Patient = pat,
            //            AppointmentDate = DateTime.Parse(parts[5]),
            //            Status = parts[6]
            //        };
            //        hospital.Appointments.Add(appt);
            //    }
            //}

            //Console.WriteLine("Appointments loaded from file.");
        }

        // Method to save doctors to a file
        public static void SaveDoctors()
        {
            using (StreamWriter writer = new StreamWriter(doctorsFilePath))
            {
                foreach (var doctor in hospital.Doctors)
                {
                    writer.WriteLine($"{doctor.Id}|{doctor.Name}|{doctor.Age}|{doctor.Specialization}|{string.Join(",", doctor.AvailableAppointments)}");
                }
            }
            Console.WriteLine("Doctors saved to file.");
        }

        // Method to save pati from a file
        public static void SavePatients()
        {
            using (StreamWriter writer = new StreamWriter(patientsFilePath))
            {
                foreach (var patient in hospital.Patients)
                {
                    writer.WriteLine($"{patient.Id}|{patient.Name}|{patient.Age}|{patient.PhoneNumber}");
                }
            }
            Console.WriteLine("Patients saved to file.");
        }


        // Method to load doctors from a file
        public static void LoadDoctors()
        {
            if (!File.Exists(doctorsFilePath))
            {
                Console.WriteLine("No doctors file found.");
                return;
            }
            using (StreamReader reader = new StreamReader(doctorsFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split('|');
                    if (parts.Length < 5) continue; // Skip invalid lines
                    Doctor doc = new Doctor
                    {
                        Id = int.Parse(parts[0]),
                        Name = parts[1],
                        Age = int.Parse(parts[2]),
                        Specialization = parts[3],
                        AvailableAppointments = new List<DateTime>(Array.ConvertAll(parts[4].Split(','), DateTime.Parse))
                    };
                    hospital.Doctors.Add(doc);
                }
            }
            Console.WriteLine("Doctors loaded from file.");
        }

        // Method to load patients from a file
        public static void LoadPatients()
        {
            if (!File.Exists(patientsFilePath))
            {
                Console.WriteLine("No patients file found.");
                return;
            }
            using (StreamReader reader = new StreamReader(patientsFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split('|');
                    if (parts.Length < 4) continue; // Skip invalid lines
                    Patient pat = new Patient
                    {
                        Id = int.Parse(parts[0]),
                        Name = parts[1],
                        Age = int.Parse(parts[2]),
                        PhoneNumber = parts[3]
                    };
                    hospital.Patients.Add(pat);
                }
            }
            Console.WriteLine("Patients loaded from file.");
        }






    }// End of Program class



    // Classes

    // Person class
    class Person
    {
        public int Id;
        public string Name;
        public int Age;
        
        public virtual void DisplayInfo()
        {
            Console.WriteLine("Name: " + Name + ", Age: " + Age);
        }
    }

    // Doctor class

    class Doctor : Person
    {
        public string Specialization;
        public List<DateTime> AvailableAppointments = new List<DateTime>();

        public override void DisplayInfo()
        {
            Console.WriteLine("Doctor ID  " + Id + " ,  Doctor: " + Name +  ", Specialization: " + Specialization);
        }
    }

    // Patient class

    class Patient : Person
    {
        public string PhoneNumber;

        public override void DisplayInfo()
        {
            Console.WriteLine("Patient: " + Name + ", Age: " + Age + ", Phone: " + PhoneNumber);
        }
    }


    // Appointment class
    class Appointment
    {
        public int AppointmentId;
        public Doctor Doctor;
        public Patient Patient;
        public DateTime AppointmentDate;
        public String Status;
    }


    // Hospital class

    class Hospital
    {
        public List<Doctor> Doctors = new List<Doctor>();
        public List<Patient> Patients = new List<Patient>();
        public List<Appointment> Appointments = new List<Appointment>();



    }















}// End of namespace HospitalSystemTask_OOP
