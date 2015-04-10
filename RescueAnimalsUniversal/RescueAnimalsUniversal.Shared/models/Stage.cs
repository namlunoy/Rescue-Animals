
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RescueAnimalsUniversal
{
    public class Stage
    {
        [XmlIgnore]
        public static User CurrentUser = null;

        #region mô tả file xml
        [XmlElement("ID")]
        public int ID { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Image")]
        public string ImageUriString { get; set; }
        [XmlElement("MaxPoint")]
        public int MaxPoint { get; set; }
        #endregion

        [XmlIgnore]
        public Uri ImageUri { get { return new Uri(ImageUriString); } }
        [XmlIgnore]
        public Uri Star1Uri { get; set; }
        [XmlIgnore]
        public Uri Star2Uri { get; set; }
        [XmlIgnore]
        public Uri Star3Uri { get; set; }


        private int _point = 0;
        //Mỗi lần được gán điểm thì thay đổi số sao vàng
        [XmlIgnore]
        public int Point
        {
            get { return _point; }
            set
            {
                _point = value;
                double x = _point * 1.0 / MaxPoint;
                if (x >= 0.8)
                {
                    //3 sao
                    Star1Uri = StaticLink.GoldStarUri;
                    Star2Uri = StaticLink.GoldStarUri;
                    Star3Uri = StaticLink.GoldStarUri;
                }
                else if (x >= 0.6 && x < 0.8)
                {
                    //2 sao
                    Star1Uri = StaticLink.GoldStarUri;
                    Star2Uri = StaticLink.GoldStarUri;
                    Star3Uri = StaticLink.BlackStarUri;
                }
                else if (x >= 0.4 && x < 0.6)
                {
                    //1 sao
                    Star1Uri = StaticLink.GoldStarUri;
                    Star2Uri = StaticLink.BlackStarUri;
                    Star3Uri = StaticLink.BlackStarUri;
                }
                else
                {
                    //0 sao
                    Star1Uri = StaticLink.BlackStarUri;
                    Star2Uri = StaticLink.BlackStarUri;
                    Star3Uri = StaticLink.BlackStarUri;
                }
            }
        }

        public int TinhSao(int point)
        {
            double x = point * 1.0 / MaxPoint;

            if (x >= 0.8)
                return 3;
            else if (x >= 0.6 && x < 0.8)
                return 2;
            else if (x >= 0.4 && x < 0.6)
                return 1;
            else
                return 0;

        }

    }
}
