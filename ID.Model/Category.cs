using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ID.Model
{
    public class Category : Base
    {
        public Int16 CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Int16 ParentCategory { get; set; }
    }
}