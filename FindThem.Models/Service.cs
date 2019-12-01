using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindThem.Models
{
    [Table("service")]
    public class Service : Shared
    {
        public string name { get; set; }

        public float timeExecution { get; set; }

        public float price { get; set; }

        public string description { get; set; }

        public string materials { get; set; }

        [ForeignKey("providerID")]
        public Provider provider { get; set; }

        public bool enabled { get; set; }

    }
}
