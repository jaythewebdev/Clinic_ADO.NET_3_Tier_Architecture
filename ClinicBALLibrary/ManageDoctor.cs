using ClinicDALLibrary;
using ClinicModelLibrary;

namespace ClinicBALLibrary
{
    public class ManageDoctor
    {
        private readonly IRepo<Doctor, int> _repo;

        public ManageDoctor()
        {

        }
        public ManageDoctor(IRepo<Doctor, int> repo)
        {
            _repo = repo;
        }
        public Doctor Add(Doctor doctor)
        {
            return _repo.Add(doctor);
        }
        public ICollection<Doctor> GetAll()
        {
            return _repo.GetAll();
        }
        public Doctor Get(int id)
        {
            return _repo.Get(id);
        }
        public bool Check(int id)
        {
            return _repo.Check(id);
        }
        public Doctor Delete(int id)
        {
            return _repo.Delete(id);
        }
    }
    public class ManagePatient
    {
        private readonly IRepo<Patient, int> _repo;

        public ManagePatient()
        {

        }
        public ManagePatient(IRepo<Patient, int> repo)
        {
            _repo = repo;
        }
        public Patient Add(Patient patient)
        {
            return _repo.Add(patient);
        }
        public ICollection<Patient> GetAll()
        {
            return _repo.GetAll();
        }
        public Patient Get(int id)
        {
            return _repo.Get(id);
        }
        public bool Check(int id)
        {
            return _repo.Check(id);
        }
        public Patient Delete(int id)
        {
            return _repo.Delete(id);
        }

    }

    public class ManageAppointment
    {
        private readonly IRepo<Appointments, int> _repo;

        public ManageAppointment()
        {

        }
        public ManageAppointment(IRepo<Appointments, int> repo)
        {
            _repo = repo;
        }
        public Appointments Add(Appointments appointment)
        {
            return _repo.Add(appointment);
        }
        public ICollection<Appointments> GetAll()
        {
            return _repo.GetAll();
        }
        public bool Check(int id)
        {
            return _repo.Check(id);
        }
        public bool CheckDIDFromAppointments(int Did)
        {
            return _repo.CheckDIDFromAppointments(Did);
        }
        public bool CheckPIDFromAppointments(int Pid)
        {
            return _repo.CheckPIDFromAppointments(Pid);
        }
        public Appointments Delete(int id)
        {
            return _repo.Delete(id);
        }

    }
}