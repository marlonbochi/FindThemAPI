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

        public Int64 providerID { get; set; }

        public bool enabled { get; set; }

        public static Service Update(Service service, string key, string value)
        {
            switch (key)
            {
                case "name":
                    service.name = value;
                    break;
                case "description":
                    service.description = value;
                    break;
                case "materials":
                    service.materials = value;
                    break;
                case "price":
                    service.price = float.Parse(value);
                    break;
                case "timeExecution":
                    service.timeExecution = float.Parse(value);
                    break;
                default:
                    throw new Exception("Key not found.");
            }

            service.dateUpdated = DateTime.Now;

            return service;
        }
    }
}
