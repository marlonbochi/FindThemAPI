using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindThemAPI.Models
{
    [Table("client")]
    public class Client
    {
        public Int64 id { get; set; }

        [ForeignKey("userID")]
        public virtual User user { get; set; }
    }
}
