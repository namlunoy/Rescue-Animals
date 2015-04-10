using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace RescueAnimalsUniversal
{
    public class Avatar
    {
        [XmlElement("ID")]
        public string ID { get; set; }

        [XmlElement("Link")]
        public string LinkString { get; set; }

        [XmlIgnore]
        public Uri ImageUri
        {
            get { return new Uri(LinkString); }
        }
    }
}
