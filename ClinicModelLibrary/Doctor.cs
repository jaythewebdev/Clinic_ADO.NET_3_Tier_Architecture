using static System.Reflection.Metadata.BlobBuilder;

namespace ClinicModelLibrary
{
    public class Doctor
    {
        public override string ToString()
        {
            string message = "";

            message += "Doctor Details";
            message += $"\nDoctor Id : {ID}";//Interpollation
            message += $"\nDoctor name : {Name}";//Interpollation
            message += $"\nDoctor Speciality : {Speciality}";
            message += $"\nDoctor Experience : {Experience}";//Interpollation
            message += "\n------------------------------";


            return message;
        }

        public Doctor()
        {

        }

        public Doctor(int id, string? name, string? speciality,int experience)
        {
            ID= id;
            Name = name;
            Speciality = speciality;
            Experience= experience;
        }

        public int ID;
        public string Name;
        public string Speciality;
        public int Experience;


        public void TakeDoctorDetails()
        {

            Console.WriteLine("Please enter the Doctor's Name: ");
            do
            {
                Name = Console.ReadLine();
                bool check = Name.All(c => Char.IsLetter(c) || c == ' ');
                if (!check)
                {
                    Console.WriteLine("Invalid entry please try again (Only alpahbets are accepted)");
                }
            }
            while (!Name.All(c => Char.IsLetter(c) || c == ' '));


            Console.WriteLine("Please enter the Doctor's Speciality: ");
            do
            {
                Speciality = Console.ReadLine();
                bool check = Speciality.All(c => Char.IsLetter(c) || c == ' ');
                if (!check)
                {
                    Console.WriteLine("Invalid entry please try again (Only alpahbets are accepted)");
                }
            }
            while (!Speciality.All(c => Char.IsLetter(c) || c == ' '));

            Console.WriteLine("Please enter the Experience of the Doctor (In years) : ");
            bool checkExp;
            while(!Int32.TryParse(Console.ReadLine(), out Experience) || Experience <= 0)
            {
                Console.WriteLine("Invalid entry please try again (Non Negative Numbers are accepted)");

            }

        }
    }
}