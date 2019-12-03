using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindThem.Models
{
    [Table("request")]
    public class Request : Shared
    {
        [ForeignKey("clientID")]
        [Required]
        public Client client { get; set; }

        [ForeignKey("providerID")]
        [Required]
        public Provider provider { get; set; }

        [ForeignKey("serviceID")]
        [Required]
        public Service service { get; set; }

        [Required]
        public DateTime dateStart { get; set; }

        [Required]
        public DateTime dateEnd{ get; set; }

        public string description { get; set; }
        
        [Required]
        public long cep { get; set; }

        [Required]
        public string address { get; set; }

        public string numberAddress { get; set; }

        public string complementAddress { get; set; }

        public string neighborhoodAddress { get; set; }

        [Required]
        public string city { get; set; }

        public bool enabled { get; set; }

        [Required]
        public char status { get; set; }
    }
}
