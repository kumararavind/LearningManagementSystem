﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Business
{
    public class CourseModel
    {
        [DisplayName("Course Id")]
        public int courseId { get; set; }

        [DisplayName("Course Name")]
        public string courseName { get; set; }

        [DisplayName("Course Length")]
        public int courseLength { get; set; }

        [DisplayName("Course Description")]
        public string courseDesc { get; set; }

        [DisplayName("Course Technology")]
        public string courseTech { get; set; }

        [DisplayName("Course Start Date")]
        public DateTime courseStartDate { get; set; }

        [DisplayName("Course End Date")]
        public DateTime courseEndDate { get; set; }

        [DisplayName("Course Instructor Id")]
        public int courseInstructorId { get; set; }

        [DisplayName("Category Id")]
        public int catId { get; set; }

        [DisplayName("Category Name")]
        public string categoryName { get; set; }
        public List<CourseModel> CategoryList { get; set; }

    }
}