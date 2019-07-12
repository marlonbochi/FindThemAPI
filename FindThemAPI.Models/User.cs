using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FindThemAPI.Models
{
    [Table("user")]
    public class User
    {
        public Int64 id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

    }
}
