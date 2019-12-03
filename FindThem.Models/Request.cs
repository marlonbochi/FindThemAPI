using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindThem.Models
{
    [Table("request")]
    public class Request : Shared
    {
        [ForeignKey("clientID")]
        public Client client { get; set; }

        [ForeignKey("providerID")]
        public Provider provider { get; set; }

        [ForeignKey("serviceID")]
        public Service service { get; set; }

        public DateTime dateStart { get; set; }

        public DateTime dateEnd{ get; set; }

        public string description { get; set; }
        
        public long cep { get; set; }

        public string address { get; set; }

        public string numberAddress { get; set; }

        public string complementAddress { get; set; }

        public string neighborhoodAddress { get; set; }

        public string city { get; set; }

        public bool enabled { get; set; }

        public char status { get; set; }

        public float value { get; set; }
    }
}
