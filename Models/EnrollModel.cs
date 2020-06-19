using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LMSProfile.Models
{
    public class EnrollModel
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Course Id")]
        public int courseId { get; set; }

        [DisplayName("Course Name")]
        public string courseName { get; set; }

        [DisplayName("Category Id")]
        public int catId { get; set; }

        [DisplayName("Category Name")]
        public string catName { get; set; }

        [DisplayName("Instructor Id")]
        public int instructorId { get; set; }

        [DisplayName("Instructor Name")]
        public string instructorName { get; set; }

        [DisplayName("User Id")]
        public int userId { get; set; }

        [DisplayName("User Name")]
        public string userName { get; set; }

        [DisplayName("Enrollment Status")]
        public int enrollmentStatus { get; set; }

        [DisplayName("Enrollment Type")]
        public string enrollmentType { get; set; }

        public List<EnrollModel> CategoryList { get; set; }

        public List<EnrollModel> CourseList { get; set; }

        public List<EnrollModel> InstructorList { get; set; }
        public List<EnrollModel> EnrollmentList { get; set; }
    }
}