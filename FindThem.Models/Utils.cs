using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using System.Net;
using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Web;
using System.Linq;

namespace FindThem.Models
{
    public class Utils
    {
        protected static string wordpass = "findthemapp2019!";
        
        public static string GetMd5Hash(string input)
        {
            StringBuilder sBuilder = new StringBuilder();

            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            
                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        public static string GetMd5HashPassword(string input)
        {
            var password = GetMd5Hash(input + wordpass);

            // Return the hexadecimal string.
            return password;
        }

        public Geometry getLatitudeLongitude(string key, string address)
        {

            var root = new RootObject();
            var url =
                string.Format(
                    "https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}", HttpUtility.UrlEncode(address), key);
            var req = (HttpWebRequest)WebRequest.Create(url);

            var res = (HttpWebResponse)req.GetResponse();

            using (var streamreader = new StreamReader(res.GetResponseStream()))
            {
                var result = streamreader.ReadToEnd();

                if (!string.IsNullOrWhiteSpace(result))
                {
                    root = JsonConvert.DeserializeObject<RootObject>(result);
                }
            }

            return root.results.First().geometry;
        }

        public class Location
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        public class Geometry
        {
            public Location location { get; set; }
        }

        public class Result
        {
            public Geometry geometry { get; set; }
        }

        public class RootObject
        {
            public List<Result> results { get; set; }
            public string status { get; set; }
        }
    }
}
