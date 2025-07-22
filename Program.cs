using System.Runtime.CompilerServices;

namespace HospitalSystemTask_OOP
{
    public class Program
    {

        // Create a new Hospital object to manage the system
        static Hospital hospital = new Hospital();
        static void Main(string[] args)
        {

           
            while (true)
            {
                

                Console.WriteLine("Hospital System Menu");
                Console.WriteLine("1. Add Doctor");
                Console.WriteLine("2. Add Patient");
                Console.WriteLine("3. Book Appointment");
                Console.WriteLine("4. Show Appointments");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": AddDoctor(); break;
                    case "2": AddPatient(); break;
                    case "3": BookAppointment(); break;
                    case "4": ShowAppointments(); break;
                    case "5": return;
                    default: Console.WriteLine("Invalid option. Try again."); break;
                }
            }



        } // End of Main method



        // Method to add a Doctor
        public static void AddDoctor()
        {
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
            Console.WriteLine("Doctor added.\n");
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
            Console.WriteLine("Doctor ID  "+Id + " ,  Doctor: " + Name +  ", Specialization: " + Specialization);
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
