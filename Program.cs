namespace HospitalSystemTask_OOP
{
    internal class Program
    {
        static void Main(string[] args)
        {







        } // End of Main method


















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
            Console.WriteLine("Doctor: " + Name + ", Age: " + Age + ", Specialization: " + Specialization);
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
    }


    // Hospital class

    class Hospital
    {
        public List<Doctor> Doctors = new List<Doctor>();
        public List<Patient> Patients = new List<Patient>();
        public List<Appointment> Appointments = new List<Appointment>();

    }















}// End of namespace HospitalSystemTask_OOP
