using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FindThem.Models
{
    [Table("user")]
    public class User : Shared
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string email { get; set; }

        public string password { get; set; }

        public string photo { get; set; }

    }
}
