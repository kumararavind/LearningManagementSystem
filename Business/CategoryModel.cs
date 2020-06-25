using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Business
{
    public class CategoryModel
    {
        [DisplayName("Category Id")]
        public int catId { get; set; }

        [DisplayName("Category Name")]
        public string catName { get; set; }

        [DisplayName("Category Type")]
        public string catType { get; set; }
    }
}
