using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindThem.Models
{
    [Table("client")]
    public class Client : Shared
    {

        [ForeignKey("userID")]
        public virtual User user { get; set; }

        public float? rateAVG { get; set; }

        public bool enabled { get; set; }        

        public static Client Update(Client client, string key, string value)
        {
            switch(key)
            {
                case "name":
                    client.user.name = value;
                    client.user.dateUpdated = DateTime.Now;
                    break;
                case "email":
                    client.user.email = value;
                    client.user.dateUpdated = DateTime.Now;
                    break;
                case "password":
                    client.user.password = Utils.GetMd5HashPassword(client.user.password);
                    client.user.dateUpdated = DateTime.Now;
                    break;
                case "rateAVG":
                    client.rateAVG = float.Parse(value);
                    client.dateUpdated = DateTime.Now;
                    break;
                default:
                    throw new Exception("Key not found.");
            }
            client.dateUpdated = DateTime.Now;

            return client;
        }
    }
}
