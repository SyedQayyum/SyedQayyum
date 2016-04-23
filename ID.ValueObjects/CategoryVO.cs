using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ID.ValueObjects
{
    public class CategoryVO : BaseVO
    {
        public Int16 CategoryId { get; set; }
        [Required]
        public String CategoryName { get; set; }
        public Int16 ParentCategory { get; set; }
        public String ParentCategoryName { get; set; }
    }
}