using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace RescueAnimalsUniversal
{
    [XmlRoot("root")]
    public class StageController
    {
        [XmlArray("ListStage")]
        [XmlArrayItem("Stage")]
        public List<Stage> ListStage = new List<Stage>();

        public StageController() { }
  
      
        public void SyncDataWithUser(User u)
        {
            for (int i = 0; i < ListStage.Count; i++)
                ListStage[i].Point = u.ListPoint[i];
        }

        public async Task LoadDataAsync()
        {
            String content = await Helper.ReadFileAsync("data\\stages.xml");
            XDocument xDoc = XDocument.Parse(content);
            XmlSerializer se = new XmlSerializer(typeof(StageController));
            this.ListStage = (se.Deserialize(xDoc.CreateReader()) as StageController).ListStage;
        }
    }
}
