using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindThem.Models
{
    public class Rate : Shared
    {
        [ForeignKey("requestID")]
        [Required]
        public Request request { get; set; }

        [ForeignKey("clientID")]
        [Required]
        public Client client { get; set; }

        [ForeignKey("providerID")]
        [Required]
        public Provider provider { get; set; }

        [Required]
        public int rate { get; set; }

        [Required]
        public bool enabled { get; set; }
    }
}
