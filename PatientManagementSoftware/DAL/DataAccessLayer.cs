using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using PatientManagementSoftware.Models;
//using System.Configuration;


namespace PatientManagementSoftware.DAL
{
    public class DataAccessLayer
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DbPMSConnection"].ConnectionString;


        public DataAccessLayer()
        {

        }


        public DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataTable);
            }


            return dataTable;
        }


        public SqlDataReader ExecuteReader(string query)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }


        public DataSet ExecuteDataSet(string query)
        {
            DataSet dataSet = new DataSet();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataSet);
            }


            return dataSet;
        }


        public DataTable ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(procedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                dataAdapter.Fill(dataTable);
            }


            return dataTable;
        }

       /* public DoctorViewModel GetDoctorById(int id)
        {
            DoctorViewModel doctor = null;

            string query = "SELECT * FROM Doctors WHERE DoctorID = @DoctorID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DoctorID", id);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            doctor = new DoctorViewModel
                            {
                                DoctorID = Convert.ToInt32(reader["DoctorID"]),
                                Name = reader["Name"].ToString(),
                                Specialization = reader["Specialization"].ToString(),
                                ContactNumber = reader["ContactNumber"].ToString(),
                                Availability = reader["Availability"].ToString()
                                // Map other properties as needed
                            };
                        }
                    }
                }
            }

            return doctor;
        }*/
    }
}
    


