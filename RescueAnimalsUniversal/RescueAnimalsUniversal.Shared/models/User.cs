using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Windows.ApplicationModel;
using Windows.Storage;

namespace RescueAnimalsUniversal
{
    public class User : IEquatable<User>, INotifyPropertyChanged, IComparable<User>
    {



        #region Mô tả cơ sở dữ liệu
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("LinkAvatar")]
        public string LinkAvatar { get; set; }

        [XmlIgnore]
        public Uri AvatarUri { get { return new Uri(LinkAvatar); } }

        //Điểm của các cửa
        //chỗ này cần sửa khi thay đối số cửa
        [XmlArray("ListPoint")]
        [XmlArrayItem("Point")]
        public List<int> ListPoint
        {
            get { return _listPoint; }
            set
            {
                _listPoint = value;
                Notify("ListPoint");
            }
        }

        private List<int> _listPoint = new List<int>();

        #endregion




        //chỗ này cần sửa khi thay đối số cửa
        //Cái này dùng để tìm kiềm xstage cho dễ



        public User()
        {

        }
        /// <summary>
        /// Phải dụng cái này khi tạo người dùng mới trong code
        /// </summary>
        public User(string name, string linkAvatar)
        {
            Name = name;
            LinkAvatar = linkAvatar;
            ListPoint = new List<int>(new int[StaticObjects.stageControll.ListStage.Count]);
        }


        #region Hàm và thuộc tính chức năng
        public int HightScoreX
        {
            get
            {
                int t = 0;
                for (int i = 0; i < ListPoint.Count; i++)
                    t += ListPoint[i];

                return t;
            }
        }

     

        #endregion


        #region Các hàm so sánh
        public bool Equals(User other)
        { return this.Name.ToLower().Equals(other.Name.ToLower()); }

        public static bool operator ==(User a, User b)
        { return a.Equals(b); }

        public static bool operator !=(User a, User b)
        { return !a.Equals(b); }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public int CompareTo(User other)
        {
            return this.HightScoreX.CompareTo(other.HightScoreX);
        }
    }




}
