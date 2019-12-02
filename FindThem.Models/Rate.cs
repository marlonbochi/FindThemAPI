using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindThem.Models
{
    [Table("rate")]
    public class Rate : Shared
    {
        [Required]
        public long requestID { get; set; }

        public long clientID { get; set; }

        public long providerID { get; set; }

        [Required]
        public int rate { get; set; }

        [Required]
        public bool enabled { get; set; }
    }
}
