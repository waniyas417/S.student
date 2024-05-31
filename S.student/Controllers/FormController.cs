using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using S.student.Models;
using student_management_systm.Models;

namespace student_management_systm.Controllers
{
    public class SearchesController : Controller
    {
        // GET: Searches
        public ActionResult Index()
        {
            return View(new SearchViewModel());
        }

        [HttpPost]
        public ActionResult SearchResults(string searchQuery)
        {
            string sqlQuery = @"
            SELECT 
                S.student_id,
                S.first_name,
                S.last_name,
                S.date_of_birth,
                S.gender,
                C.course_name,
                C.course_description,
                E.enrollment_date
            FROM 
                Students AS S
                INNER JOIN Enrollment AS E ON S.student_id = E.student_id
                INNER JOIN Courses AS C ON E.course_id = C.course_id
            WHERE 
                S.student_id = @searchQuery
                OR S.first_name LIKE '%' + @searchQuery + '%'
                OR S.last_name LIKE '%' + @searchQuery + '%'
        ";

            string connectionString = "Data Source=DESKTOP-3A0HSTU\\SQLEXPRESS;Initial Catalog=S.M.S;Integrated Security=True;";
            List<S.student.Models.StudentsDetailsViewModel> searchResults = new List<S.student.Models.StudentsDetailsViewModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@searchQuery", searchQuery);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    S.student.Models.StudentsDetailsViewModel studentDetails = new S.student.Models.StudentsDetailsViewModel
                    {
                        student_id = reader["student_id"] != DBNull.Value ? Convert.ToInt32(reader["student_id"]) : 0,
                        first_name = reader["first_name"] != DBNull.Value ? reader["first_name"].ToString() : string.Empty,
                        last_name = reader["last_name"] != DBNull.Value ? reader["last_name"].ToString() : string.Empty,
                        date_of_birth = (DateTime)(reader["date_of_birth"] != DBNull.Value ? Convert.ToDateTime(reader["date_of_birth"]) : (DateTime?)null),
                        gender = reader["gender"] != DBNull.Value ? reader["gender"].ToString() : string.Empty,
                        course_name = reader["course_name"] != DBNull.Value ? reader["course_name"].ToString() : string.Empty,
                        course_description = reader["course_description"] != DBNull.Value ? reader["course_description"].ToString() : string.Empty,
                        enrollment_date = reader["enrollment_date"] != DBNull.Value ? Convert.ToDateTime(reader["enrollment_date"]) : (DateTime?)null
                    };
                    searchResults.Add(studentDetails);
                }
            }

            var viewModel = new SearchViewModel
            {
                SearchQuery = searchQuery,
                SearchResults = searchResults
            };

            return View("Index", viewModel);
        }
    }
}
