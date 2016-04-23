using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.Model
{
    public class User : Base
    {
        public Int16 UserId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String UserEmail { get; set; }
        public String UserPassword { get; set; }


    }
}
