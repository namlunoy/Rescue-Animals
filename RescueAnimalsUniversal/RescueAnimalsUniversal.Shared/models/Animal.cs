using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RescueAnimalsUniversal
{
    public class Animal : INotifyPropertyChanged, IEquatable<Animal>
    {
        #region Các nội dung trong file Xml
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("Cartoon")]
        public string CartoonImageUriString { get; set; }
        [XmlElement("Real")]
        public string RealImageUriString { get; set; }
        [XmlElement("Letter")]
        public string LetterUriString { get; set; }
        [XmlElement("ImageLetter")]
        public string LetterWithImageUriString { get; set; }
        [XmlElement("SoundHuman")]
        public string SoundHumanUriString { get; set; }
        [XmlElement("SoundAnimal")]
        public string SoundAnimalUriString { get; set; }
        #endregion

        #region Các thuộc tính lấy từ file xml
        [XmlIgnore]
        public Uri CartoonImageUri { get { return new Uri(CartoonImageUriString); } }
        [XmlIgnore]
        public Uri RealImageUri { get { return new Uri(RealImageUriString); } }
        [XmlIgnore]
        public Uri LetterUri { get { return new Uri(LetterUriString); } }
        [XmlIgnore]
        public Uri LetterWithImageUri { get { return new Uri(LetterWithImageUriString); } }
        [XmlIgnore]
        public Uri SoundHumanUri { get { return new Uri(SoundHumanUriString); } }
        [XmlIgnore]
        public Uri SoundAnimalUri { get { return new Uri(SoundAnimalUriString); } }
        #endregion


        //Kiểm tra xem nó đã được mở chưa
        [XmlIgnore]
        public bool isChecked = false;


        #region các thuộc tính khác

        private Uri _displayImageUri;
        [XmlIgnore]
        public Uri DisplayImageUri
        {
            get { return _displayImageUri; }
            set
            {
                _displayImageUri = value;
                Notify("DisplayImageUri");
            }
        }
        #endregion

        #region Các hàm cần sử dụng
        public event PropertyChangedEventHandler PropertyChanged;
        private void Notify(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion

        public Animal()
        {

        }

        /// <summary>
        /// Sao chép 1 đối tượng
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public Animal CopyFrom(Animal a)
        {
            Animal x = new Animal();
            x = a.MemberwiseClone() as Animal;
            return x;
        }

        public Animal GetCopy()
        {
            Animal x = this.MemberwiseClone() as Animal;
            return x;
        }


        public bool Equals(Animal other)
        {
            return this.Name.Equals(other.Name);
        }
    }


}
