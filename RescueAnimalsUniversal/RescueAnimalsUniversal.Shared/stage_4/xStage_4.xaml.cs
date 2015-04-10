using RescueAnimalsUniversal.Stages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RescueAnimalsUniversal.stage_4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class xStage_4 : Page
    {
        //Danh sách các list animal sử dụng
        public List<List<Animal>> list = new List<List<Animal>>();

        //đếm số animal được chọn
        public int count = 0;

        //đếm số cặp đúng để chuyển tab
        public int COUNT = 0;

        //Cái này đợi nó úp xong mới được chỉ tới cái tiếp theo
        public bool isReady = false;

        public int stage = 0;

        //2 Cái động vật đang được chọn
        public Animal a, b;

        GridView selectedGridView = null;

        double space = 0;
        DispatcherTimer timer = new DispatcherTimer();




        public xStage_4()
        {
            this.InitializeComponent();

            ChonSai.AutoReverse = true;
            Delay1s.AutoReverse = true;
            Delay2s.AutoReverse = true;
            Delay3s.AutoReverse = true;

            Delay1s.Completed += Delay_Completed;
            Delay2s.Completed += Delay_Completed;
            Delay3s.Completed += Delay_Completed;
            ChonSai.Completed += ChonSai_Completed;

            list.Add(StaticObjects.animalControll.GetAnimalsx2(4));
            list.Add(StaticObjects.animalControll.GetAnimalsx2(8));
            list.Add(StaticObjects.animalControll.GetAnimalsx2(16));

            MoToanBoCacAnh();

            gridView1.ItemsSource = list[0];
            gridView2.ItemsSource = list[1];
            gridView3.ItemsSource = list[2];

            StaticObjects.currentUser.ListPoint[3] = 0;
            score.Text = StaticObjects.currentUser.ListPoint[3].ToString();


            //   Delay1s.Begin();

            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(50);
            space = xSlider.Width / 50.0;
        }

        async void timer_Tick(object sender, object e)
        {
            if ((slider.Width - space) > 0)
            {
                slider.Width -= space;
                Thickness a = new Thickness(slider.Width, 0, 0, 0);
                snail.Margin = a;
            }
            else
            {
                //Hết thời gian
                timer.Stop();
                //Chuyển sang tab tiếp theo
                slider.Width = xSlider.Width;
                await StaticObjects.userControll.SaveDataAsync();
                if (groupGridView.SelectedIndex < groupGridView.Items.Count - 1)
                {
                    if (groupGridView.SelectedIndex == 0)
                        SetTab2();
                    else if (groupGridView.SelectedIndex == 1)
                        SetTab3();

                    //groupGridView.SelectedIndex++;
                }


                else
                    ShowFinishAsync();
            }
        }


        private async void ShowFinishAsync()
        {


            timer.Stop();
            media.Stop();

            border.Visibility = Windows.UI.Xaml.Visibility.Visible;
            txtHuongDan.Visibility = Windows.UI.Xaml.Visibility.Visible;
            okButton.Visibility = Windows.UI.Xaml.Visibility.Visible;


            txtHuongDan.Text = "Your point is " + StaticObjects.currentUser.ListPoint[3];

            int k = StaticObjects.stageControll.ListStage[3].TinhSao(StaticObjects.currentUser.ListPoint[3]);
            for (int i = 1; i <= k; i++)
            {
                Image x = new Image();
                x.Height = 100;
                x.Width = 100;
                x.Source = new BitmapImage(StaticLink.GoldStarUri);
                stackStar.Children.Add(x);
            }


            for (int i = 1; i <= (3 - k); i++)
            {
                Image x = new Image();
                x.Height = 100;
                x.Width = 100;
                x.Source = new BitmapImage(StaticLink.BlackStarUri);
                stackStar.Children.Add(x);
            }
            await StaticObjects.userControll.SaveDataAsync();

        }



        /// <summary>
        /// Khi delay xong thì úp hết các ảnh ở tab đang chọn lại
        /// </summary>
        void Delay_Completed(object sender, object e)
        {
            xWait.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            isReady = true;
            selectedGridView = (groupGridView.SelectedItem as FlipViewItem).Content as GridView;
            if (selectedGridView != null)
            {
                foreach (var item in selectedGridView.Items)
                    ((Animal)item).DisplayImageUri = StaticLink.BlackImageUri;
                timer.Start();
            }


        }



        void MoToanBoCacAnh()
        {
            foreach (var l in list)
                foreach (var item in l)
                {
                    item.DisplayImageUri = item.CartoonImageUri;
                    item.isChecked = false;
                }
        }

        void ChonSai_Completed(object sender, object e)
        {
            a.DisplayImageUri = StaticLink.BlackImageUri;
            b.DisplayImageUri = StaticLink.BlackImageUri;
            isReady = true;
        }

        bool exeption = false;

        private async void selectGridView(object sender, SelectionChangedEventArgs e)
        {
        

            if (exeption == false)
            {
                if (groupGridView != null && groupGridView.SelectedItem != null)
                {
                    if (stage == groupGridView.SelectedIndex)
                    {
                        if (xWait != null)
                            xWait.Visibility = Windows.UI.Xaml.Visibility.Visible;

                        selectedGridView = (groupGridView.SelectedItem as FlipViewItem).Content as GridView;
                        if (groupGridView.SelectedIndex == 0)
                            Delay1s.Begin();
                        else if (groupGridView.SelectedIndex == 1)
                            Delay2s.Begin();
                        else if (groupGridView.SelectedIndex == 2)
                            Delay3s.Begin();
                    }
                    else
                    {
                        //Người dung tự ý chuyên
                        exeption = true;
                        await StaticObjects.userControll.SaveDataAsync();
                        groupGridView.SelectedIndex = stage;

                    }
                }
            }
            else
            {
                exeption = false;
            }

        }



        private void selectItem(object sender, SelectionChangedEventArgs e)
        {
            if (e != null && e.AddedItems.Count > 0)
            {
                Animal x = e.AddedItems[0] as Animal;

                //Chỉ mở khi ảnh checked = false
                if (x != null && x.isChecked == false && isReady == true)
                {
                    count++;
                    x.DisplayImageUri = x.CartoonImageUri;

                    if (count == 1)
                    {
                        a = x;
                        a.isChecked = true;
                    }
                    if (count == 2)
                    {
                        //Cho bộ đếm về 0
                        count = 0;

                        b = x;
                        //Kiểm tra xem 2 animal có bằng nhau
                        if (a.Equals(b))
                        {
                            //Nếu đúng
                            media.Source = StaticLink.SoundCorrectUri;
                            StaticObjects.currentUser.ListPoint[3]++;
                            score.Text = StaticObjects.currentUser.ListPoint[3].ToString();
                            a.isChecked = true;
                            b.isChecked = true;
                            COUNT++;

                            ChuyenTiep();
                        }
                        else
                        {
                            //Nếu sai
                            media.Source = StaticLink.SoundWrongUri;
                            isReady = false;
                            a.isChecked = false;
                            b.isChecked = false;
                            ChonSai.Begin();

                        }
                        media.Play();
                    }
                }
                ((GridView)sender).SelectedItem = null;
            }
        }


        async void SetTab2()
        {
            COUNT = 2;
            timer.Stop();
            stage = 1;
            slider.Width = xSlider.Width;
            timer.Interval = TimeSpan.FromMilliseconds(250);
            MoToanBoCacAnh();
            await StaticObjects.userControll.SaveDataAsync();
            isReady = false;
            groupGridView.SelectedIndex = 1;
        }

        async void SetTab3()
        {
            COUNT = 6;
            stage = 2;
            timer.Stop();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            slider.Width = xSlider.Width;
            MoToanBoCacAnh();
            await StaticObjects.userControll.SaveDataAsync();
            isReady = false;
            groupGridView.SelectedIndex = 2;
        }

        private void ChuyenTiep()
        {
            if (COUNT == 2)
                SetTab2();
            if (COUNT == 6)
                SetTab3();
        }



        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Stage_4.thisPage.Frame.GoBack();
        }


        private void clickOK(object sender, RoutedEventArgs e)
        {
            Stage_4.thisPage.Frame.GoBack();
        }

        private void clickHelp(object sender, RoutedEventArgs e)
        {
            helpButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            helpImage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            Delay1s.Begin();
        }

    }
}
