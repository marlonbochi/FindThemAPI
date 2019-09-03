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

        [ForeignKey("servideID")]
        [Required]
        public Service service { get; set; }

        [Required]
        public DateTime dateStart { get; set; }

        [Required]
        public DateTime dateEnd{ get; set; }

        public string description { get; set; }


        [Required]
        public Int64 cep { get; set; }

        [Required]
        public string address { get; set; }

        public Int64? numberAddress { get; set; }

        [Required]
        public string city { get; set; }

        public bool enabled { get; set; }

        [Required]
        public char status { get; set; }
    }
}
