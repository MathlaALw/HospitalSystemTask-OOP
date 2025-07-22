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

   
















}// End of namespace HospitalSystemTask_OOP
