using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClinicModelLibrary
{
    public class Slot
    {
        public override string ToString()
        {
            string message = "";

            message += "Slot Details";
            message += $"\nSlot  : {SlotID}";//Interpollation
            message += $"\nTimings : {Timing}";//Interpollation

            return message;
        }
        public int SlotID { get; set; }
        public string Timing { get; set; }
        public Slot() { }
    }
}
