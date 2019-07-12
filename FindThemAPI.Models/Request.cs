using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindThemAPI.Models
{
    [Table("request")]
    public class Request
    {
        public Int64 id { get; set; }
        public Client client { get; set; }
        public Provider provider { get; set; }
    }
}
