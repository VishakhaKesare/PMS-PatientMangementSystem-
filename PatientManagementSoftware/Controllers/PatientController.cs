using PatientManagementSoftware.DAL;
using PatientManagementSoftware.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PatientManagementSoftware.Controllers
{

    [Authorize]
    public class PatientController : Controller
    {
        DataAccessLayer dal = new DataAccessLayer();
        // GET: Doctor
        public ActionResult Index()
        {
            dal = new DataAccessLayer();

            List<PatientViewModel> patientsList = new List<PatientViewModel>();

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Action", "select")
            };

            DataTable dataTable = dal.ExecuteStoredProcedure("ManagePatientsDML", parameters);

            foreach (DataRow data in dataTable.Rows)
            {
                PatientViewModel patient = new PatientViewModel
                {
                    PatientID = Convert.ToInt32(data["PatientID"]),
                    Name = data["Name"].ToString(),
                    DateOfBirth = Convert.ToDateTime(data["DateOfBirth"]),
                    Address = data["Address"].ToString(),
                    ContactNumber = data["ContactNumber"].ToString(),
                    Gender = data["Gender"].ToString()
                };

                patientsList.Add(patient);
            }
            return View(patientsList);
        }


        public ActionResult PatientRegister()
        {
            return View();
        }
        // GET: Display form to edit patient details
        [HttpGet]
        public ActionResult EditPatient(int patientID)
        {
            PatientViewModel model = GetPatientById(patientID);
            if (model != null)
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        // POST: Save edited patient details
        [HttpPost]
        public ActionResult EditPatient(PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@PatientID", model.PatientID),
            new SqlParameter("@Name", model.Name),
            new SqlParameter("@DateOfBirth", model.DateOfBirth),
            new SqlParameter("@Address", model.Address),
            new SqlParameter("@ContactNumber", model.ContactNumber),
            new SqlParameter("@Gender", model.Gender),
            new SqlParameter("@Action", "Update")
                };

                DataTable result = dal.ExecuteStoredProcedure("ManagePatientsDML", parameters);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // Helper method to retrieve patient details by ID
        private PatientViewModel GetPatientById(int patientID)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbPMSConnection"].ConnectionString;
            string query = "SELECT * FROM Patients WHERE PatientID = @PatientID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@PatientID", patientID);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    PatientViewModel model = new PatientViewModel
                    {
                        PatientID = Convert.ToInt32(reader["PatientID"]),
                        Name = reader["Name"].ToString(),
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                        Address = reader["Address"].ToString(),
                        ContactNumber = reader["ContactNumber"].ToString(),
                        Gender = reader["Gender"].ToString()
                    };
                    reader.Close();
                    return model;
                }
            }
            return null;
        }

        // POST: Save new patient details
        [HttpPost]
        public ActionResult SavePatient(PatientViewModel model)
        {
            if (ModelState.IsValid)
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@DateOfBirth", model.DateOfBirth),
                    new SqlParameter("@Address", model.Address),
                    new SqlParameter("@ContactNumber", model.ContactNumber),
                    new SqlParameter("@Gender", model.Gender),
                    new SqlParameter("@Action", "Insert")
                };

                DataTable result = dal.ExecuteStoredProcedure("ManagePatientsDML", parameters);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult DeletePatient(int patientID)
        {
            string query = "ManagePatientsDML";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Action", "delete"),
        new SqlParameter("@PatientID", patientID)
            };

            // Execute the delete stored procedure
            dal.ExecuteStoredProcedure(query, parameters);

            // Set a success message in TempData
            TempData["SuccessMessage"] = "Patients deleted successfully.";

            // Redirect back to the index action to refresh the list of invoices
            return RedirectToAction("Index");
        }
    }
}