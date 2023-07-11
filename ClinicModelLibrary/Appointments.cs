using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicModelLibrary
{
    public class Appointments
    {
        public override string ToString()
        {
            string message = "";

            message += "Appointemnt Details";
            message += $"\nAppointment Id : {AID}";
            message += $"\nDoctor Id : {DID}";
            message += $"\nPatient Id : {PID}";
            message += $"\nSlot Id : {SID}";
            message += "\n------------------------------";

            return message;
        }

        public int AID;
        public int DID;
        public int PID;
        public int SID;

        public Appointments() { }

        public Appointments(int aid,int pid,int did,int sid)
        {
            AID = aid;
            DID = did;
            PID = pid;
            SID = sid;
        }

        public void TakeAppointmentDetails()
        {

            Console.WriteLine("Please enter your Patient ID: ");
            bool checkPID;
            do
            {
                checkPID = Int32.TryParse(Console.ReadLine(), out PID);
                if (!checkPID)
                {
                    Console.WriteLine("Invalid entry please try again (Non Negative Numbers are accepted)");
                }
            } while (!checkPID);
            if (PID <= 0)
            {
                throw new NonNegativeValueException("Patient ID cannot be negative. Only Non Negative Numbers are accepted");
            }

            Console.WriteLine("Please enter the Doctor ID you want to book an appointment with: ");
            bool checkDID;
            do
            {
                checkDID = Int32.TryParse(Console.ReadLine(), out DID);
                if (!checkDID)
                {
                    Console.WriteLine("Invalid entry please try again (Non Negative Numbers are accepted)");
                }
            } while (!checkDID);
            if (DID <= 0)
            {
                throw new NonNegativeValueException("Doctor ID cannot be negative. Only Non Negative Numbers are accepted");
            }
            Console.WriteLine("Please enter the Slot you want to book an appointment with (Between 1 - 5 ): ");
            bool checkSID;
            do
            {
                checkSID = Int32.TryParse(Console.ReadLine(), out SID);
                if (!checkSID)
                {
                    Console.WriteLine("Invalid entry please try again (Non Negative Numbers are accepted)");
                }
            } while (!checkSID);
            if (SID <= 0)
            {
                throw new NonNegativeValueException("Doctor ID cannot be negative. Only Non Negative Numbers are accepted");
            }
        }
    }
}
