using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicModelLibrary
{
    public class Patient
    {
        public override string ToString()
        {
            string message = "";

            message += "Patient Details";
            message += $"\nPatient Id : {ID}";//Interpollation
            message += $"\nPatient name : {Name}";//Interpollation
            message += $"\nPatient Age : {Age}";
            message += $"\nPatient Phone Number : {phone}";//Interpollation
            message += "\n------------------------------";


            return message;
        }

        public int ID;
        public string Name;
        public int Age;
        public string phone;

        public Patient() { }

        public Patient(int id, string? name, int age, string? Phone)
        {
            ID = id;
            Name = name;
            Age = age;
            phone = Phone;
        }


        public void TakePatientDetails()
        {

            Console.WriteLine("Please enter the Patient's Name: ");
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


            //Console.WriteLine("Please enter the Patient's Age: ");
            //bool checkAge=false;

            //    do
            //    {
            //    try
            //    {
            //        checkAge = Int32.TryParse(Console.ReadLine(), out Age);
            //        if (!checkAge)
            //        {
            //            Console.WriteLine("Invalid entry please try again (Non Negative Numbers are accepted)");
            //        }
            //        if (Age <= 0)
            //        {
            //            throw new NonNegativeValueException("Age cannot be negative. Only Non Negative Numbers are accepted");
            //        }
            //    }
            //    catch(NonNegativeValueException nne)
            //    {
            //        Console.WriteLine(nne.Message);
            //    }

            //    } while (!checkAge);

            Console.WriteLine("Please enter the Patient's Age (In years) : ");
            while (!Int32.TryParse(Console.ReadLine(), out Age) || Age <= 0)
            {
                Console.WriteLine("Invalid entry please try again (Non Negative Numbers are accepted)");
            }


            Console.WriteLine("Please enter the Patient's Phone Numbers (10 digits in length) : ");
            do
            {
                phone = Console.ReadLine();
                bool check = phone.All(c => Char.IsDigit(c));
                if (!check)
                {
                    Console.WriteLine("Invalid entry please try again (Only alpahbets are accepted)");
                }
            }
            while (!phone.All(c => Char.IsDigit(c)));

        }
    }
}
