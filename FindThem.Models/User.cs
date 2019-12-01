using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;

namespace FindThem.Models
{
    [Table("user")]
    public class User : Shared
    {
        public string name { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string kind { get; set; }

        public string photo { get; set; }

        public static string getKindUser(long id)
        {
            string kind = "user";
            using (var db = new FindThemContext())
            {
                kind = db.Users.Where(x => x.id == id).FirstOrDefault().kind;
            }

            return kind;

        }

    }
}
