using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindThem.Models
{
    [Table("request")]
    public class Request : Shared
    {
        public Client client { get; set; }
        public Provider provider { get; set; }
    }
}
