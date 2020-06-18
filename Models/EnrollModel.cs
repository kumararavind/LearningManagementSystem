using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSProfile.Models
{
    public class EnrollModel
    {
        public int Id { get; set; }

        public int courseId { get; set; }

        public string courseName { get; set; }

        public int catId { get; set; }

        public string catName { get; set; }

        public int instructorId { get; set; }

        public string instructorName { get; set; }

        public int userId { get; set; }

        public string userName { get; set; }

        public int enrollmentStatus { get; set; }

        public string enrollmentType { get; set; }

        public List<EnrollModel> CategoryList { get; set; }

        public List<EnrollModel> CourseList { get; set; }

        public List<EnrollModel> InstructorList { get; set; }
        public List<EnrollModel> EnrollmentList { get; set; }
    }
}