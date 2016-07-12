using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Denovu.Data;

namespace Denovu.DataSnatching
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter file path please");
                var path = Console.ReadLine();
                //var path = @"C:\Users\SDEV\OneDrive\Documents\Temp\za-locations.txt";
                var inputString = File.ReadAllText(path);
                XmlSerializer serializer = new XmlSerializer(typeof(Locations));
                MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(inputString));
                Locations locations = (Locations)serializer.Deserialize(memStream);
                using (var db = new DenovuEntities())
                {
                    var location = locations.Location;
                    InsertLocationToDB(null, location, db);
                    db.SaveChanges();
                }
                Console.WriteLine(" Insert data to database successful, enter any key to quit");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void InsertLocationToDB(int? parentId, Location location, DenovuEntities db)
        {
            string lo = "";
            string la = "";
            string name = "";
            try
            {
                lo = location.Longitude;
                la = location.Latitude;
                name = location.Name;
                Denovu.Data.Location loc = new Data.Location()
                {
                    Id = Convert.ToInt32(location.Id),
                    Name = location.Name,
                    LocalizedName = location.LocalizedName,
                    Longitude = ToNullableDouble(location.Longitude),
                    Latitude = ToNullableDouble(location.Latitude),
                    ParentId = parentId
                };
                db.Locations.Add(loc);
                if (location.ChildLocations != null && location.ChildLocations.Length > 0)
                {
                    foreach (var childLocation in location.ChildLocations)
                    {
                        InsertLocationToDB(location.Id, childLocation, db);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("name/lo/la : {0}/{1}/{2}", name, lo, la);
            }
        }

        private static double? ToNullableDouble(string p)
        {
            if (string.IsNullOrEmpty(p))
            {
                return null;
            }
            else
            {
                return double.Parse(p);
            }
        }
    }
}
