using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindThem.Models
{
    [Table("message")]
    public class Message : Shared
    {
        [ForeignKey("requestID")]
        [Required]
        public Request request { get; set; }

        [Required]
        public string text { get; set; }

        [Required]
        public bool received { get; set; }

        [Required]
        public bool enabled { get; set; }
    }
}
