using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiemensAssignment.BusinessModels
{
    public class UserDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}