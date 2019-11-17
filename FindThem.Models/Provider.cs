using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FindThem.Models
{
    [Table("provider")]
    public class Provider : Shared
    {
        [ForeignKey("userID")]
        public User user { get; set; }

        public string name { get; set; }

        public string photo { get; set; }

        public float? rateAVG { get; set; }

        public string CEP { get; set; }

        public string address { get; set; }

        public string number { get; set; }

        public string neighborhood { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public bool enabled { get; set; }

        public static Provider Update(Provider provider, string key, string value)
        {
            switch (key)
            {
                case "name":
                    provider.name = value;
                    break;
                case "photo":
                    provider.photo = value;
                    break;
                case "rateAVG":
                    provider.rateAVG = float.Parse(value);
                    break;
                case "email":
                    provider.user.email = value;
                    provider.user.dateUpdated = DateTime.Now;
                    break;
                case "password":
                    provider.user.password = Utils.GetMd5HashPassword(value);
                    provider.user.dateUpdated = DateTime.Now;
                    break;
                default:
                    throw new Exception("Key not found.");
            }

            provider.dateUpdated = DateTime.Now;

            return provider;
        }
    }
}
