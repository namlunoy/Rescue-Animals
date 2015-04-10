using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RescueAnimalsUniversal
{
    [XmlRoot("root")]
    public class AvatarController
    {
        [XmlArray("ListAvatar")]
        [XmlArrayItem("Avatar")]
        public List<Avatar> ListAvatar = new List<Avatar>();

        public async Task LoadDataAsync()
        {
            String content = await Helper.ReadFileAsync("data\\avatars.xml");
            XDocument xDoc = XDocument.Parse(content);
            XmlSerializer se = new XmlSerializer(typeof(AvatarController));
            this.ListAvatar = (se.Deserialize(xDoc.CreateReader()) as AvatarController).ListAvatar;
        }
    }
}
