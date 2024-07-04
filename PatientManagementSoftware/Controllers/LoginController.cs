using PatientManagementSoftware.DAL;
using PatientManagementSoftware.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Security;

namespace PatientManagementSoftware.Controllers
{

  
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        private bool AuthenticateUser(string username, string password)
        {
            // Call the stored procedure to authenticate user
            DataAccessLayer dal = new DataAccessLayer();

            string query = "SPLoginUser";
            SqlParameter[] parameters = new SqlParameter[]
            {
             new SqlParameter("@Username", username),
             new SqlParameter("@Password", password)
            };


            DataTable result = dal.ExecuteStoredProcedure(query, parameters);
            if (result.Rows.Count > 0)
            {
                return Convert.ToBoolean(result.Rows[0][0]);
            }
            return false;
        }
        // GET: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel model)
        {
            // GET: /Account/Login
            if (ModelState.IsValid)
            {
                // Call stored procedure to authenticate user
                bool isAuthenticated = AuthenticateUser(model.Username, model.Password);
                if (isAuthenticated)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);

                    Session["UserID"] = model.Username;
                    Session["User"] = model.Username;

                    return RedirectToAction("Index", "Admin_Dashboard");

                }
                else
                {
                    // If authentication fails, return to login page with error
                    ViewBag.ErrorMessage = "Invalid Login Credentials!";
                    ModelState.AddModelError(string.Empty, "Invalid username or password");
                }

            }
            // If login fails or ModelState is not valid, return to login page with errors
            model.Types = GetTypesFromDatabase();
            return View(model);
        }
        private List<string> GetTypesFromDatabase()
        {
            // Method to fetch types from the database
            // Replace this with your actual implementation
            return new List<string> { "Doctor", "Staff", "Admin", "Patient" };
        }



        public ActionResult Logout()
        {
          FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Admin_Registration(LoginViewModel model)
        {
            DataAccessLayer dataAccessLayer = new DataAccessLayer();

            string Query = $"INSERT INTO tbl_users(username,Email,Password,MobileNo) values('{model.Username}','{model.Email}','{model.Password}','{model.MobileNo}')";

            if (ModelState.IsValid)
            {
                DataTable result = dataAccessLayer.ExecuteQuery(Query);
                return RedirectToAction("Index");
            }

            return View();
        }



    }
}
