using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindThem.Models
{
    public class Payment : Shared
    {
        [ForeignKey("requestID")]
        [Required]
        public Request request { get; set; }

        [Required]
        public string type { get; set; }

        [Required]
        public float value { get; set; }

        public float tip { get; set; }

        public string transactionID { get; set; }

        public Int64 creditCardNumber { get; set; }

        public string document { get; set; }

        public DateTime dateBirth { get; set; }

        public int verificationNumber { get; set; }

        public string expirationDate { get; set; }

        public string nameCreditCard { get; set; }

        [Required]
        public bool enabled { get; set; }
    }
}
