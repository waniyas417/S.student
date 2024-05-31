using S.student.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S.student.Models
{
    public class SearchViewModel
    {
        public string SearchQuery { get; set; }
        public List<StudentsDetailsViewModel> SearchResults { get; set; }


    }

}