using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindThem.Models
{
    [Table("rate")]
    public class Rate : Shared
    {
        [ForeignKey("requestID")]
        [Required]
        public Request request { get; set; }

        [ForeignKey("clientID")]
        public Client client { get; set; }

        [ForeignKey("providerID")]
        public Provider provider { get; set; }

        [Required]
        public int rate { get; set; }

        [Required]
        public bool enabled { get; set; }
    }
}
