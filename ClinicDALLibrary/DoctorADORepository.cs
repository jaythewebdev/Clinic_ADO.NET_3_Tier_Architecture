using ClinicDALLibrary;
using ClinicModelLibrary;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;

namespace ClinicDALLibrary
{
    public class DoctorADORepository : IRepo<Doctor,int>
    {
        SqlConnection conn;
        public DoctorADORepository() {
            conn = new SqlConnection("Data Source=LAPTOP-1J0KOR9F\\SQLSERVER2023JAI;Integrated Security=true;Initial Catalog=Clinic");
        }

        public Doctor Add(Doctor doctor)
        {
            SqlCommand cmdInsert = new SqlCommand("proc_InserDoctortUsingSp", conn);
            cmdInsert.CommandType = CommandType.StoredProcedure;
            cmdInsert.Parameters.AddWithValue("@uname", doctor.Name);
            cmdInsert.Parameters.AddWithValue("@uspeciality", doctor.Speciality);
            cmdInsert.Parameters.AddWithValue("@uexperinece", doctor.Experience);
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            int result = cmdInsert.ExecuteNonQuery();
            conn.Close();
            if (result > 0)
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                SqlCommand cmdGetId = new SqlCommand("proc_FetchLatestDocID", conn);
                doctor.ID = Convert.ToInt32(cmdGetId.ExecuteScalar().ToString());
                conn.Close();
                return doctor;
            }
            return null;
        }

        public Doctor Delete(int id)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("proc_FetchAndDisplaySpecificDoctorsUsingSP", conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.AddWithValue("@Did", id);

            DataSet ds = new DataSet();
            adapter.Fill(ds);
            Doctor doctor = new Doctor(Convert.ToInt32(ds.Tables[0].Rows[0][0]), Convert.ToString(ds.Tables[0].Rows[0][1]),
            Convert.ToString(ds.Tables[0].Rows[0][2]), Convert.ToInt32(ds.Tables[0].Rows[0][3]));

            SqlCommand cmdDelete = new SqlCommand("proc_DeleteDoctorUsingSp", conn);
            cmdDelete.CommandType = CommandType.StoredProcedure;
            cmdDelete.Parameters.AddWithValue("@Did", id);
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            int result = cmdDelete.ExecuteNonQuery();
            conn.Close();
            if (result > 0)
            {
                return doctor;
            }
            return null;
        }

        public ICollection<Doctor> GetAll()
        {
            List<Doctor> doctors = new List<Doctor>();
            SqlCommand cmdSelect = new SqlCommand("proc_FetchAndDisplayDoctorsUsingSP", conn);
            cmdSelect.CommandType = CommandType.StoredProcedure;
            Doctor doctor;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlDataReader reader = cmdSelect.ExecuteReader();
            if (!reader.HasRows)
                return null;
            while (reader.Read())
            {
                doctor = new Doctor();
                doctor.ID = Convert.ToInt32(reader[0].ToString());
                doctor.Name = reader[1].ToString();
                doctor.Speciality = reader[2].ToString();
                doctor.Experience = Convert.ToInt32(reader[3].ToString());
                doctors.Add(doctor);
            }
            conn.Close();
            return doctors;
        }

        public Doctor Get(int id)
        {
            var doctors = GetAll();
            if (doctors == null)
                return null;
            foreach (var item in doctors)
            {
                if (item.ID == id)
                {
                    return item;
                }
            }
            return null;
        }

        public bool Check(int Did)
        {
            SqlCommand cmdCount = new SqlCommand("proc_CheckDocIDSP", conn);
            cmdCount.CommandType = CommandType.StoredProcedure;
            cmdCount.Parameters.AddWithValue("@Did", Did);
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            if ((int)cmdCount.ExecuteScalar() > 0)
                return true;
            conn.Close();
            return false;
        }

        bool IRepo<Doctor, int>.CheckPIDFromAppointments(int Pid)
        {
            throw new NotImplementedException();
        }

        bool IRepo<Doctor, int>.CheckDIDFromAppointments(int Did)
        {
            throw new NotImplementedException();
        }
    }
    }
public class PatientADORepository : IRepo<Patient, int>
{
    SqlConnection conn;
    public PatientADORepository()
    {
        conn = new SqlConnection("Data Source=LAPTOP-1J0KOR9F\\SQLSERVER2023JAI;Integrated Security=true;Initial Catalog=Clinic");
    }

    public Patient Add(Patient patient)
    {
        SqlCommand cmdInsert = new SqlCommand("proc_InserPatientUsingSp", conn);
        cmdInsert.CommandType = CommandType.StoredProcedure;
        cmdInsert.Parameters.AddWithValue("@uname", patient.Name);
        cmdInsert.Parameters.AddWithValue("@uage", patient.Age);
        cmdInsert.Parameters.AddWithValue("@uphone", patient.phone);
        if (conn.State == ConnectionState.Open)
            conn.Close();
        conn.Open();
        int result = cmdInsert.ExecuteNonQuery();
        conn.Close();
        if (result > 0)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            SqlCommand cmdGetId = new SqlCommand("proc_FetchLatestPatientID", conn);
            patient.ID = Convert.ToInt32(cmdGetId.ExecuteScalar().ToString());
            conn.Close();
            return patient;
        }
        return null;
    }

    public Patient Delete(int id)
    {
        SqlDataAdapter adapter = new SqlDataAdapter("proc_FetchAndDisplaySpecificPatientUsingSP", conn);
        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        adapter.SelectCommand.Parameters.AddWithValue("@Pid", id);

        DataSet ds = new DataSet();
        adapter.Fill(ds);
        Patient patient = new Patient(Convert.ToInt32(ds.Tables[0].Rows[0][0]), Convert.ToString(ds.Tables[0].Rows[0][1]),
        Convert.ToInt32(ds.Tables[0].Rows[0][2]), Convert.ToString(ds.Tables[0].Rows[0][3]));

        SqlCommand cmdDelete = new SqlCommand("proc_DeletePatientUsingSp", conn);
        cmdDelete.CommandType = CommandType.StoredProcedure;
        cmdDelete.Parameters.AddWithValue("@Pid", id);
        if (conn.State == ConnectionState.Open)
            conn.Close();
        conn.Open();
        int result = cmdDelete.ExecuteNonQuery();
        conn.Close();
        if (result > 0)
        {
            return patient;
        }
        return null;
    }

    public Patient Get(int id)
    {
        var patients = GetAll();
        if (patients == null)
            return null;
        foreach (var item in patients)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }

    public bool Check(int Pid)
    {
        SqlCommand cmdCount = new SqlCommand("proc_CheckPatientIDSP", conn);
        cmdCount.CommandType = CommandType.StoredProcedure;
        cmdCount.Parameters.AddWithValue("@Pid", Pid);
        if (conn.State == ConnectionState.Open)
            conn.Close();
        conn.Open();
        if ((int)cmdCount.ExecuteScalar() > 0)
            return true;
        return false;
    }

    public ICollection<Patient> GetAll()
    {
        List<Patient> patients = new List<Patient>();
        SqlCommand cmdSelect = new SqlCommand("proc_FetchAndDisplayPatientsUsingSP", conn);
        cmdSelect.CommandType = CommandType.StoredProcedure;
        if (conn.State == ConnectionState.Open)
            conn.Close();
        conn.Open();
        Patient patient;
        SqlDataReader reader = cmdSelect.ExecuteReader();
        if (!reader.HasRows)
            return null;
        while (reader.Read())
        {
            patient = new Patient();
            patient.ID = Convert.ToInt32(reader[0].ToString());
            patient.Name = reader[1].ToString();
            patient.Age = Convert.ToInt32(reader[2].ToString());
            patient.phone = reader[3].ToString();
            patients.Add(patient);
        }
        conn.Close();
        return patients;
    }

    bool IRepo<Patient, int>.CheckPIDFromAppointments(int Pid)
    {
        throw new NotImplementedException();
    }

    bool IRepo<Patient, int>.CheckDIDFromAppointments(int Did)
    {
        throw new NotImplementedException();
    }
}

public class AppointmentADORepository : IRepo<Appointments, int>
{
    SqlConnection conn;
    public AppointmentADORepository()
    {
        conn = new SqlConnection("Data Source=LAPTOP-1J0KOR9F\\SQLSERVER2023JAI;Integrated Security=true;Initial Catalog=Clinic");
    }

    public Appointments Add(Appointments appointments)
    {
        SqlCommand cmdInsert = new SqlCommand("proc_InserAppointmentstUsingSp", conn);
        cmdInsert.CommandType = CommandType.StoredProcedure;

        cmdInsert.Parameters.AddWithValue("@Pid", appointments.PID);
        cmdInsert.Parameters.AddWithValue("@Did", appointments.DID);
        cmdInsert.Parameters.AddWithValue("@Sid", appointments.SID);
        if (conn.State == ConnectionState.Open)
            conn.Close();
        conn.Open();
        int result = cmdInsert.ExecuteNonQuery();
        conn.Close();
        if (result > 0)
        {
            conn.Open();
            SqlCommand cmdGetId = new SqlCommand("proc_FetchLatestAppointmentID", conn);
            appointments.AID = Convert.ToInt32(cmdGetId.ExecuteScalar().ToString());
            conn.Close();
            return appointments;
        }
        return null;
    }

    public Appointments Delete(int id)
    {
        SqlDataAdapter adapter = new SqlDataAdapter("proc_FetchAndDisplaySpecificAppointmentByAppointmentUsingSP", conn);
        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        adapter.SelectCommand.Parameters.AddWithValue("@Aid", id);

        DataSet ds = new DataSet();
        adapter.Fill(ds);
        Appointments appointments = new Appointments(Convert.ToInt32(ds.Tables[0].Rows[0][0]), Convert.ToInt32(ds.Tables[0].Rows[0][1]),
        Convert.ToInt32(ds.Tables[0].Rows[0][2]), Convert.ToInt32(ds.Tables[0].Rows[0][3]));

        SqlCommand cmdDelete = new SqlCommand("proc_DeleteAppointmentByAppointmentUsingSp", conn);
        cmdDelete.CommandType = CommandType.StoredProcedure;
        cmdDelete.Parameters.AddWithValue("@Aid", id);
        if (conn.State == ConnectionState.Open)
            conn.Close();
        conn.Open();
        int result = cmdDelete.ExecuteNonQuery();
        conn.Close();
        if (result > 0)
        {
            return appointments;
        }
        return null;
    }

    public Appointments Get(int id)
    {
        var appointments = GetAll();
        if (appointments == null)
            return null;
        foreach (var item in appointments)
        {
            if (item.AID == id)
            {
                return item;
            }
        }
        return null;
    }

    public bool Check(int Aid)
    {
        SqlCommand cmdCount = new SqlCommand("proc_CheckAppointmentIDSP", conn);
        cmdCount.CommandType = CommandType.StoredProcedure;
        cmdCount.Parameters.AddWithValue("@Aid", Aid);
        if (conn.State == ConnectionState.Open)
            conn.Close();
        conn.Open();
        if ((int)cmdCount.ExecuteScalar() > 0)
            return true;
        conn.Close();
        return false;
    }

    public bool CheckPIDFromAppointments(int Pid)
    {
        SqlCommand cmdCount = new SqlCommand("proc_CheckPatientIDFromAppointmentSP", conn);
        cmdCount.CommandType = CommandType.StoredProcedure;
        cmdCount.Parameters.AddWithValue("@Pid", Pid);
        if (conn.State == ConnectionState.Open)
            conn.Close();
        conn.Open();
        if ((int)cmdCount.ExecuteScalar() > 0)
            return true;
        conn.Close();
        return false;
    }
    public bool CheckDIDFromAppointments(int Did)
    {
        SqlCommand cmdCount = new SqlCommand("proc_CheckDocIDFromAppointmentSP", conn);
        cmdCount.CommandType = CommandType.StoredProcedure;
        cmdCount.Parameters.AddWithValue("@Did", Did);
        if (conn.State == ConnectionState.Open)
            conn.Close();
        conn.Open();
        if ((int)cmdCount.ExecuteScalar() > 0)
            return true;
        conn.Close();
        return false;
    }

    public ICollection<Appointments> GetAll()
    {
        List<Appointments> appointments = new List<Appointments>();
        SqlCommand cmdSelect = new SqlCommand("proc_FetchAndDisplayAppointmentsUsingSP", conn);
        cmdSelect.CommandType = CommandType.StoredProcedure;
        if (conn.State == ConnectionState.Open)
            conn.Close();
        conn.Open();
        Appointments appointment;
        SqlDataReader reader = cmdSelect.ExecuteReader();
        if (!reader.HasRows)
            return null;
        while (reader.Read())
        {
            appointment = new Appointments();
            appointment.AID = Convert.ToInt32(reader[0].ToString());
            appointment.PID = Convert.ToInt32(reader[1].ToString());
            appointment.DID = Convert.ToInt32(reader[2].ToString());
            appointment.SID = Convert.ToInt32(reader[3].ToString());
            appointments.Add(appointment);
        }
        conn.Close();
        return appointments;
    }
}

