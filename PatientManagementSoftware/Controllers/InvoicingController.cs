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
    public class InvoicingController : Controller
    {
            // GET: Invoicing
            DataAccessLayer dal = new DataAccessLayer();
            public List<PatientViewModel> PatientDDL()
            {
                List<PatientViewModel> patientList = new List<PatientViewModel>();

                SqlParameter[] parameters = new SqlParameter[]
                {
                     new SqlParameter("@Action", "select")
                };

                DataTable patientdt = dal.ExecuteStoredProcedure("ManagePatientsDML", parameters);

                foreach (DataRow dr in patientdt.Rows)
                {
                    patientList.Add(new PatientViewModel
                    {
                        PatientID = Convert.ToInt32(dr["PatientID"]),
                        Name = dr["Name"].ToString(),
                    });
                }
                return patientList;
            }

            public ActionResult Index()
            {
                dal = new DataAccessLayer();

                string query = "ManageInvoiceDML";

                SqlParameter[] parameters = new SqlParameter[]
                {
         new SqlParameter("@Action", "select")
                };

                DataTable dt = dal.ExecuteStoredProcedure(query, parameters);

                List<BillingViewModel> BillingList = new List<BillingViewModel>();

                foreach (DataRow dr in dt.Rows)
                {
                    BillingViewModel model = new BillingViewModel()
                    {
                        BillID = Convert.ToInt32(dr["BillID"]),
                        PatientID = Convert.ToInt32(dr["PatientID"]),
                        PatientName = dr["PatientName"].ToString(),
                        BillDate = Convert.ToDateTime(dr["BillDate"]),
                        Amount = Convert.ToDecimal(dr["Amount"].ToString()),
                        PaymentStatus = dr["PaymentStatus"].ToString()
                    };
                    BillingList.Add(model);
                }
                ViewBag.list = BillingList;
                return View();
            }





            public ActionResult Invoice()
            {
                //Geting Dropdownlist Data

                ViewBag.PatientList = PatientDDL();


                return View();
            }

            // CreateInvoice

            /*public ActionResult Invoice()
            {
                return View();
            }*/

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Invoice(BillingViewModel model)
            {
                if (ModelState.IsValid)
                {
                    dal = new DataAccessLayer();

                    ViewBag.PatientList = PatientDDL();

                    string query = "ManageInvoiceDML";

                    SqlParameter[] parameters = new SqlParameter[]
                    {
                      new SqlParameter("@Action","insert"),
                      new SqlParameter("@PatientID",model.PatientID),
                      new SqlParameter("@BillDate",model.BillDate),
                      new SqlParameter("@Amount",model.Amount),
                      new SqlParameter("@PaymentStatus",model.PaymentStatus)
                    };

                    DataTable dt = dal.ExecuteStoredProcedure(query, parameters);
                    return RedirectToAction("Index");
                }
                return View(model);
            }


            public ActionResult PrintInvoice(int billId)
            {
                dal = new DataAccessLayer();

                BillingViewModel model = new BillingViewModel();

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@BillID", billId)
                };

                DataTable patientdt = dal.ExecuteStoredProcedure("GetInvoiceByPatient", parameters);

                foreach (DataRow dr in patientdt.Rows)
                {
                    model.BillID = Convert.ToInt32(dr["BillID"]);
                    model.BillDate = Convert.ToDateTime(dr["BillDate"].ToString());
                    model.Amount = Convert.ToDecimal(dr["Amount"].ToString());
                    model.PatientID = Convert.ToInt32(dr["PatientID"].ToString());
                    model.PatientName = dr["PatientName"].ToString();
                    model.PaymentStatus = dr["PaymentStatus"].ToString();
                };
                return View(model);
            }

            public ActionResult EditInvoice(int BillID)
            {
                dal = new DataAccessLayer();

                BillingViewModel billingViewModel = new BillingViewModel();

                string Query = "GetDataByID";

                ViewBag.list1 = PatientDDL();

                SqlParameter[] parameters = new SqlParameter[]
                {
                new SqlParameter("ChooseTable","Billing"),
                new SqlParameter("@BillingID",BillID),
                };

                DataTable dataTable = dal.ExecuteStoredProcedure(Query, parameters);

                foreach (DataRow row in dataTable.Rows)
                {
                    billingViewModel.BillID = Convert.ToInt32(row["BillID"]);
                    billingViewModel.PatientID = Convert.ToInt32(row["PatientID"]);
                    billingViewModel.BillDate = Convert.ToDateTime(row["BillDate"]);
                    billingViewModel.Amount = Convert.ToDecimal(row["Amount"]);
                    billingViewModel.PaymentStatus = row["PaymentStatus"].ToString();
                }
                return View(billingViewModel); //UpdateInvoice

            }

            public ActionResult UpdateInvoice(BillingViewModel model)
            {
                dal = new DataAccessLayer();

                string query = "[ManageInvoiceDML]";

                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                new SqlParameter("@Action","update"),
                new SqlParameter("@BillID",model.BillID),
                new SqlParameter("@PatientID",model.PatientID),
                new SqlParameter("@BillDate",model.BillDate),
                new SqlParameter("@Amount",model.Amount),
                new SqlParameter("@PaymentStatus",model.PaymentStatus),
                };

                DataTable dataTable = dal.ExecuteStoredProcedure(query, sqlParameters);

                return RedirectToAction("Index");


                return View(model);
            }

        [HttpPost]
        public ActionResult DeleteInvoice(int billId)
        {
            string query = "ManageInvoiceDML";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@Action", "delete"),
        new SqlParameter("@BillID", billId)
            };

            // Execute the delete stored procedure
            dal.ExecuteStoredProcedure(query, parameters);

            // Set a success message in TempData
            TempData["SuccessMessage"] = "Invoice deleted successfully.";

            // Redirect back to the index action to refresh the list of invoices
            return RedirectToAction("Index");
        }


    }

}
