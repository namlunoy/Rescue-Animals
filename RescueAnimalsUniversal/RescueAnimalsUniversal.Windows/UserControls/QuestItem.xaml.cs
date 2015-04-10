using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace RescueAnimalsUniversal.UserControls
{
    public sealed partial class QuestItem : UserControl, INotifyPropertyChanged
    {
        private Animal animal_1;
        public class KetQuaArg : EventArgs
        {
            public bool KetQua { get; set; }
            public KetQuaArg(bool kq)
            { KetQua = kq; }
        }
        public Animal Animal_1
        {
            get { return animal_1; }
            set
            {
                animal_1 = value;
                Notify("Animal_1");
            }
        }

        private Animal animal_2;

        public Animal Animal_2
        {
            get { return animal_2; }
            set { animal_2 = value; Notify("Animal_2"); }
        }

        public Uri ImageUri { get {

            int k = Helper.Rand.Next(0, 2);
            if (k == 0)
                return Animal_1.RealImageUri;
            else return Animal_1.CartoonImageUri;
        
        } }
        private string _name1;

        public string Name1
        {
            get { return _name1; }
            set { _name1 = value; Notify("Name1"); }
        }
        private string _name2;

        public string Name2
        {
            get { return _name2; }
            set { _name2 = value; Notify("Name2"); }
        }

        public bool Check { get; set; }
        private int _case;

        public QuestItem(Animal a1, Animal a2)
        {
            this.InitializeComponent();
            Animal_1 = a1;
            Animal_2 = a2;

            _case = Helper.Rand.Next(0, 2);
            switch (_case)
            {
                case 0:
                    Name1 = Animal_1.Name;
                    Name2 = Animal_2.Name;
                    break;
                case 1:
                    Name2 = Animal_1.Name;
                    Name1 = Animal_2.Name;
                    break;
                default:
                    break;
            }
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Notify(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public event EventHandler<KetQuaArg> CheckedEvent;
        public KetQuaArg kqArg;
       

        private void ClickChon(object sender, SelectionChangedEventArgs e)
        {
            if (dapan != null && dapan.SelectedItem != null)
            {
                Check = _case == dapan.SelectedIndex;
                kqArg = new KetQuaArg(Check);
                CheckedEvent(this, kqArg);
                Debug.WriteLine("Check = " + Check);
                dapan.SelectedItem = null;
            }
        }
    }
}
