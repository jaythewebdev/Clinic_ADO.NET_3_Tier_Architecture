using ClinicBALLibrary;
using ClinicDALLibrary;
using ClinicModelLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClinicFEApp
{
    internal class ClinicProvider
    {
        ManageDoctor manageDoctor;
        IRepo<Doctor, int> DoctorRepo;
        ManagePatient managePatient;
        IRepo<Patient, int> PatientRepo;
        ManageAppointment manageAppointment;
        IRepo<Appointments, int> AppointmentRepo;
        public ClinicProvider() {
            DoctorRepo = new DoctorADORepository();
            manageDoctor = new ManageDoctor(DoctorRepo);
            PatientRepo = new PatientADORepository();
            managePatient = new ManagePatient(PatientRepo);
            AppointmentRepo = new AppointmentADORepository();
            manageAppointment = new ManageAppointment(AppointmentRepo);
            InitializeClinic();
        }
        void PrintMenu()
        {
            Console.WriteLine("--------------------------------");
            Console.WriteLine("\n      Doppolo Hospitals       ");
            Console.WriteLine("\n--------------------------------");
            Console.WriteLine("1. Add Patient");
            Console.WriteLine("2. View All Patient");
            Console.WriteLine("3. View Single Patient Detail");
            Console.WriteLine("4. Book Appointment");
            Console.WriteLine("5. Add Doctor");
            Console.WriteLine("6. View All Doctor");
            Console.WriteLine("7. View Single Doctor");
            Console.WriteLine("8. Delete Doctor Details");
            Console.WriteLine("9. Delete Appointment Details");
            Console.WriteLine("10. Delete Patient Details");
            Console.WriteLine("11. View All Appointments ");
            Console.WriteLine("0. Exit");
        }

        private void InitializeClinic()
        {
            int choice = 0;
            do
            {
                PrintMenu();
                Console.WriteLine("Select an option");
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0) ;
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("***Thanks for visiting our hospital***");
                        Console.WriteLine("*** Eat healthy , Stay healthy ***");
                        break;
                    case 1:
                        AddPatient();
                        break;
                    case 2:
                        ViewAllPatient();
                        break;
                    case 3:
                        ViewSinglePatient();
                        break;
                    case 4:
                        BookAppointment();
                        break;
                    case 5:
                        AddDoctor();
                        break;
                    case 6:
                        ViewAllDoctor();
                        break;
                    case 7:
                        ViewSingleDoctor();
                        break;
                    case 8:
                        DeleteDoctor();
                        break;
                    case 9:
                        DeleteAppointment();
                        break;
                    case 10:
                        DeletePatient();
                        break;
                    case 11:
                        ViewAllAppointments();
                        break;
                    default:
                        Console.WriteLine("***Please Enter a Valid Option***");
                        break;
                }

            } while (choice != 0);
        }

        private void DeletePatient()
        {
            int PID = 0;

            try
            {
                var patients = managePatient.GetAll().ToList();

                while (true)
                {
                    while (!int.TryParse(Console.ReadLine(), out PID))
                    {
                        Console.Write("Enter ID (Only Numbers are allowed): ");
                    }
                    try
                    {
                        if (managePatient.Check(PID) == false)
                            throw new InvalidEnteredID();
                        if(!manageAppointment.CheckPIDFromAppointments(PID) && managePatient.Check(PID) == true)
                        {
                            Console.WriteLine("---------------------------");
                            Console.WriteLine("The deleted Patient is ");
                            Console.WriteLine(managePatient.Delete(PID));
                            Console.WriteLine("***Patient Deleted Successfully***");
                            break;
                        }

                    }
                    catch (InvalidEnteredID iei)
                    {
                        Console.WriteLine(iei.Message);
                    }
                    catch (InvalidRemoveException ire)
                    {
                        Console.WriteLine(ire.Message);
                    }
                    catch (ArgumentNullException ane)
                    {
                        Console.WriteLine(ane.Message);
                    }
                    catch (ArgumentException ae)
                    {
                        Console.WriteLine(ae.Message);
                    }
                    catch(SqlException se)
                    {
                        Console.WriteLine("This Patient cannot be removed as he has pending appointments");
                    }
                }
            }
            catch(ArgumentNullException ane)
            {
                Console.WriteLine(ane.Message);
            }
            
        }

        private void DeleteAppointment()
        {
            try
            {
                var appointments = manageAppointment.GetAll().ToList();

                int AID = 0;
                while (true)
                {
                    while (!int.TryParse(Console.ReadLine(), out AID))
                    {
                        Console.Write("Enter ID (Only Numbers are allowed): ");
                    }
                    try
                    {
                        if (manageAppointment.Check(AID) == false)
                            throw new InvalidEnteredID();
                        else
                            Console.WriteLine("---------------------------");
                            Console.WriteLine("The deleted Appointment is ");
                            Console.WriteLine(manageAppointment.Delete(AID));
                            Console.WriteLine("***Appointment Deleted Successfully***");

                        break;
                    }
                    catch (InvalidEnteredID iei)
                    {
                        Console.WriteLine(iei.Message);
                    }
                    catch (ArgumentNullException ane)
                    {
                        Console.WriteLine(ane.Message);
                    }
                    catch (ArgumentException ae)
                    {
                        Console.WriteLine("There is No Appointment To Remove");
                    }
                }
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine(ane.Message);
            }
        }


        private void DeleteDoctor()
        {
            int DID = 0;
            try
            {
                var doctors = manageDoctor.GetAll().ToList();

                while (true)
                {
                    while (!int.TryParse(Console.ReadLine(), out DID))
                    {
                        Console.Write("Enter ID (Only Numbers are allowed): ");
                    }
                    try
                    {
                        if (manageDoctor.Check(DID) == false)
                            throw new InvalidEnteredID();
                        else if (manageAppointment.CheckDIDFromAppointments(DID) == true)
                            throw new InvalidRemoveException();
                        else
                            Console.WriteLine("---------------------------");
                            Console.WriteLine("The deleted Doctor is ");
                            Console.WriteLine(manageDoctor.Delete(DID));
                            Console.WriteLine("***Doctor Deleted Successfully***");
                            break;
                    }
                    catch (InvalidEnteredID iei)
                    {
                        Console.WriteLine(iei.Message);
                    }
                    catch (InvalidRemoveException ire)
                    {
                        Console.WriteLine(ire.Message);
                    }
                    catch (ArgumentNullException ane)
                    {
                        Console.WriteLine(ane.Message);
                    }
                    catch (ArgumentException ae)
                    {
                        Console.WriteLine(ae.Message);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            catch(ArgumentNullException ane)
            {
                Console.WriteLine(ane.Message);
            }
         }  

        private void ViewSingleDoctor()
        {
            int DID = 0;
            while (true)
            {
                while (!int.TryParse(Console.ReadLine(), out DID))
                {
                    Console.Write("Enter ID (Only Numbers are allowed): ");
                }
                try
                {
                    if (manageDoctor.Check(DID) == false)
                        throw new InvalidEnteredID();
                    else
                        Console.WriteLine(manageDoctor.Get(DID));
                        break;
                }
                catch (InvalidEnteredID iei)
                {
                    Console.WriteLine(iei.Message);
                }
            }
        }

        private void ViewAllDoctor()
        {
            try
            {
                var doctors = manageDoctor.GetAll().ToList();
                if (doctors != null)
                {
                    foreach (var item in doctors)
                    {
                        Console.WriteLine("---------------------------");
                        Console.WriteLine(item);
                    }
                }
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("***No Doctors Available***");
                Console.WriteLine("---------------------------");
                Console.WriteLine(ane);
            }
        }

        private void AddDoctor()
        {
            Doctor doctor = new Doctor();
            doctor.TakeDoctorDetails();
            try
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine(manageDoctor.Add(doctor));
                Console.WriteLine("***Doctor Added Successfully***");

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine(ane.Message);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }

        private void BookAppointment()
        {
            Appointments appointments = new Appointments();
            while (true)
            {
                try
                {
                    appointments.TakeAppointmentDetails();
                    
                    if (((manageDoctor.Check(appointments.DID) == false) && managePatient.Check(appointments.PID) == false && appointments.SID>5 || appointments.SID <= 0))
                    {
                            Console.WriteLine("The doctor is busy with the other appointment!");
                    }       
                    else
                    {
                        Console.WriteLine("---------------------------");
                        Console.WriteLine(manageAppointment.Add(appointments));
                        Console.WriteLine("***Appointment booked Successfully***");
                        break;
                    }
                }
                catch (InvalidEnteredID iei)
                {
                    Console.WriteLine(iei.Message);
                }
                catch (DuplicateValueException dve)
                {
                    Console.WriteLine(dve.Message);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Duplicates values are not accepted check with Appointment table before booking");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

     
        private void ViewSinglePatient()
        {
            int PID = 0;
            while (true)
            {
                while (!int.TryParse(Console.ReadLine(), out PID))
                {
                    Console.Write("Enter ID (Only Numbers are allowed): ");
                }
                try
                {
                    if (managePatient.Check(PID) == false)
                        throw new InvalidEnteredID();
                    else
                        Console.WriteLine("---------------------------");
                        Console.WriteLine(managePatient.Get(PID));

                        break;
                }
                catch (InvalidEnteredID iei)
                {
                    Console.WriteLine(iei.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void ViewAllPatient()
        {
            try
            {
                var patients = managePatient.GetAll().ToList();
                if (patients != null)
                {
                    foreach (var item in patients)
                    {
                        Console.WriteLine("---------------------------");
                        Console.WriteLine(item);
                    }
                }
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("No Patients Available");
                Console.WriteLine(ane);
            }
        }
        private void ViewAllAppointments()
        {
            try
            {
                var appointments = manageAppointment.GetAll().ToList();
                if (appointments != null)
                {
                    foreach (var item in appointments)
                    {
                        Console.WriteLine("---------------------------");
                        Console.WriteLine(item);
                    }
                }
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("*** No Appointments has been booked ***");
                Console.WriteLine(ane);
            }
        }
        private void AddPatient()
        {
            Patient patient = new Patient();
            patient.TakePatientDetails();
            try
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine(managePatient.Add(patient));
                Console.WriteLine("***Patient Added Successfully***");
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine(ane.Message);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }
    }
}

