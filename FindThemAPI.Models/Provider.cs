using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindThemAPI.Models
{
    [Table("provider")]
    public class Provider
    {
        public Int64 id { get; set; }

        public User user { get; set; }
    }
}
