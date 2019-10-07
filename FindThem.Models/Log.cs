using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FindThem.Models
{
    [Table("log")]
    public class Log
    {
        public Int64 id { get; set; }

        [ForeignKey("userID")]
        [Required]
        public User user { get; set; }

        public string description { get; set; }

        public DateTime dateInserted { get; set; }
    }
}
