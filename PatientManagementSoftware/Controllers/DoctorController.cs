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
    public class DoctorController : Controller
    {
        DataAccessLayer dal = new DataAccessLayer();

        // GET: Doctor
        public ActionResult Index()
        {
            List<DoctorViewModel> doctorsList = new List<DoctorViewModel>();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Action", "select")
            };

            DataTable dataTable = dal.ExecuteStoredProcedure("ManageDoctorsDML", parameters);

            foreach (DataRow data in dataTable.Rows)
            {
                DoctorViewModel doctor = new DoctorViewModel
                {
                    DoctorID = Convert.ToInt32(data["DoctorID"]),
                    Name = data["Name"].ToString(),
                    Specialization = data["Specialization"].ToString(),
                    ContactNumber = data["ContactNumber"].ToString(),
                    Availability = data["Availability"].ToString()
                };

                doctorsList.Add(doctor);
            }

            return View(doctorsList);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditDoctor(int doctorID)
        {
            DoctorViewModel model = null;

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbPMSConnection"].ConnectionString;

            string query = "SELECT * FROM Doctors WHERE DoctorID = @DoctorID";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DoctorID", doctorID);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                model = new DoctorViewModel
                {
                    DoctorID = Convert.ToInt32(reader["DoctorID"]),
                    Name = reader["Name"].ToString(),
                    Specialization = reader["Specialization"].ToString(),
                    ContactNumber = reader["ContactNumber"].ToString(),
                    Availability = reader["Availability"].ToString()
                };
            }

            reader.Close();
            connection.Close();

            if (model != null)
            {
                return View(model);
            }

            return RedirectToAction("Index");
        }


        /*[HttpGet]
        public ActionResult EditDoctor(int doctorID)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@DoctorID", doctorID),
                new SqlParameter("@Action", "Select")
            };

            DataTable dataTable = dal.ExecuteStoredProcedure("ManageDoctorsDML", parameters);

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                DoctorViewModel model = new DoctorViewModel
                {
                    DoctorID = Convert.ToInt32(row["DoctorID"]),
                    Name = row["Name"].ToString(),
                    Specialization = row["Specialization"].ToString(),
                    ContactNumber = row["ContactNumber"].ToString(),
                    Availability = row["Availability"].ToString()
                };

                return View(model);
            }

            return RedirectToAction("Index");
        }*/

        [HttpPost]
        public ActionResult EditDoctor(DoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@DoctorID", model.DoctorID),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@Specialization", model.Specialization),
                    new SqlParameter("@ContactNumber", model.ContactNumber),
                    new SqlParameter("@Availability", model.Availability),
                    new SqlParameter("@Action", "Update")
                };

                DataTable result = dal.ExecuteStoredProcedure("ManageDoctorsDML", parameters);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult SaveDoctor(DoctorViewModel model)
        {
            if (ModelState.IsValid)
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@DoctorID", model.DoctorID),
                    new SqlParameter("@Name", model.Name),
                    new SqlParameter("@Specialization", model.Specialization),
                    new SqlParameter("@ContactNumber", model.ContactNumber),
                    new SqlParameter("@Availability", model.Availability),
                    new SqlParameter("@Action", model.DoctorID > 0 ? "Update" : "Insert")
                };

                DataTable result = dal.ExecuteStoredProcedure("ManageDoctorsDML", parameters);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteDoctor(int doctorID)
        {
            string query = "ManageDoctorsDML";
            SqlParameter[] parameters = new SqlParameter[]
            {
                  new SqlParameter("@Action", "delete"),
                  new SqlParameter("@DoctorID", doctorID)
            };

            // Execute the delete stored procedure
            dal.ExecuteStoredProcedure(query, parameters);

            // Set a success message in TempData
            TempData["SuccessMessage"] = "Doctor deleted successfully.";

            // Redirect back to the index action to refresh the list of doctors
            return RedirectToAction("Index");
        }

    }
}
