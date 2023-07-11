using ClinicModelLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicDALLibrary
{
    public interface IRepo<T1,T2>
    {
        T1 Get(T2 id);
        T1 Add(T1 item);

        bool Check(T2 id);
        ICollection<T1> GetAll();
        T1 Delete(int id);

        public bool CheckPIDFromAppointments(int Pid);
        public bool CheckDIDFromAppointments(int Did);

    }
}
