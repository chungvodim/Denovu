using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Denovu.DataSnatching
{
    [XmlRoot(ElementName = "locations", Namespace = "http://www.ebayclassifiedsgroup.com/schema/location/v1")]
    public class Location
    {
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }
        [XmlElement(ElementName = "id-name", Namespace = "http://www.ebayclassifiedsgroup.com/schema/location/v1")]
        public string Name { get; set; }

        [XmlElement(ElementName = "localized-name", Namespace = "http://www.ebayclassifiedsgroup.com/schema/location/v1")]
        public string LocalizedName { get; set; }

        [XmlElement(ElementName = "longitude", Namespace = "http://www.ebayclassifiedsgroup.com/schema/location/v1")]
        public string Longitude { get; set; }

        [XmlElement(ElementName = "latitude", Namespace = "http://www.ebayclassifiedsgroup.com/schema/location/v1")]
        public string Latitude { get; set; }
        [XmlElement(ElementName = "location", Namespace = "http://www.ebayclassifiedsgroup.com/schema/location/v1")]
        public Location[] ChildLocations { get; set; }
    }

    [XmlRoot(ElementName = "locations", Namespace = "http://www.ebayclassifiedsgroup.com/schema/location/v1")]
    public class Locations
    {
        [XmlElement(ElementName = "location", Namespace = "http://www.ebayclassifiedsgroup.com/schema/location/v1")]
        public Location Location { get; set; }
    }
}
