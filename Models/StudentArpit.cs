using System;
using System.Collections.Generic;

#nullable disable

namespace StudentServer.Models
{
    public partial class StudentArpit
    {
        public int Rollno { get; set; }
        public string Name { get; set; }
        public DateTime? Dob { get; set; }
        public string Branch { get; set; }
        public double? Cgpa { get; set; }
        public string Email { get; set; }
    }
}
