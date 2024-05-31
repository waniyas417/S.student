using System;

namespace S.student.Models
{
    public class StudentsDetailsViewModel
    {
       
            public int student_id { get; set; }

            public string first_name { get; set; }
            public string last_name { get; set; }
            public DateTime date_of_birth { get; set; }
            public string gender { get; set; }

            public string course_name { get; set; }
            public string course_description { get; set; }
            public Nullable<System.DateTime> enrollment_date { get; set; } = default(Nullable<System.DateTime>);


        
    }
}
