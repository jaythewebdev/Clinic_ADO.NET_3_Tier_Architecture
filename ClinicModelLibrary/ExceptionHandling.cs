using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicModelLibrary
{
    public class NonNegativeValueException : Exception
    {
        public string message { get; set; }
        public NonNegativeValueException()
        {
            message = "Value Cnnot be Negative .Only Non Negative Numbers are Accepted";
        }
        public NonNegativeValueException(string message)
        {
            this.message = message;
        }
        public override string Message => message;
    }

    public class InvalidEnteredID : Exception
    {
        string message;
        public InvalidEnteredID()
        {

            message = "Kindly check with the appointment details and book appointements accordingly (Enter ID's and Slot Properly)";
        }
        public InvalidEnteredID(string message)
        {
            this.message = message;
        }
        public override string Message
        {
            get { return message; }
        }
    }
    public class DuplicateValueException : Exception
    {
        public string message { get; set; }
        public DuplicateValueException()
        {
            message = "Kindly check with the appointment details and book appointements accordingly";
        }
        public DuplicateValueException(string message)
        {
            this.message = message;
        }
        public override string Message => message;
    }
    public class InvalidRemoveException : Exception
    {
        public string message { get; set; }
        public InvalidRemoveException()
        {
            message="Cannot be removed as He/She has an appointments pending";
        }
        public InvalidRemoveException(string message)
        {
            this.message = message;
        }
        public override string Message => message;
    }
}
