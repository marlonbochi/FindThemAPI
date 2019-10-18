using System;
using System.Collections.Generic;
using System.Text;

namespace FindThem.Models
{
    public class Shared
    {
        public Shared()
        {
            dateInserted = DateTime.Now;
            dateUpdated = DateTime.Now;
        }

        public Int64 id { get; set; }

        public DateTime dateInserted { get; set; }

        public DateTime dateUpdated { get; set; }
    }
}
