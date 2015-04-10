using RescueAnimalsUniversal.Stages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RescueAnimalsUniversal.stage_1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class xStage_1 : Page
    {
        public List<Animal> x1, y1, x2, y2, x3, y3;
        private bool isStop = false;
        public xStage_1()
        {
            this.InitializeComponent();

            StaticObjects.currentUser.ListPoint[0] = 0;
            score.Text = StaticObjects.currentUser.ListPoint[0].ToString();

            x1 = StaticObjects.animalControll.GetAnimals(4);
            y1 = StaticObjects.animalControll.TronAnimalsFromLists(x1);
            x2 = StaticObjects.animalControll.GetAnimals(9);
            y2 = StaticObjects.animalControll.TronAnimalsFromLists(x2);
            x3 = StaticObjects.animalControll.GetAnimals(16);
            y3 = StaticObjects.animalControll.TronAnimalsFromLists(x3);

            flipView1.ItemsSource = x1;
            gridView1.ItemsSource = y1;
            flipView2.ItemsSource = x2;
            gridView2.ItemsSource = y2;
            flipView3.ItemsSource = x3;
            gridView3.ItemsSource = y3;

            helpImage.Visibility = Windows.UI.Xaml.Visibility.Visible;
       
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

        }

        private void clickOK(object sender, RoutedEventArgs e)
        {
            if (isStop)
                Stage_1.thisPage.Frame.GoBack();
            //Stage_1.storyboard.Begin();
        }

        private void clickGoBack(object sender, RoutedEventArgs e)
        {
            Stage_1.thisPage.Frame.GoBack();
        }

        private async void selectItem(object sender, SelectionChangedEventArgs e)
        {
            if (e != null && e.AddedItems != null && e.AddedItems.Count > 0)
            {
                FlipView f = (groupFlipView.SelectedItem as FlipViewItem).Content as FlipView;
                GridView g = (groupGridView.SelectedItem as FlipViewItem).Content as GridView;
                Debug.WriteLine("123");

                if (f != null && g != null)
                {
                    Debug.WriteLine(">>>>>> " + groupGridView.SelectedIndex);

                    Animal a = f.SelectedItem as Animal;
                    Animal b = g.SelectedItem as Animal;


                    if (a != null && b != null)
                    {
                        if (a.Equals(b))
                        {
                            media.Source = StaticLink.SoundCorrectUri;
                            StaticObjects.currentUser.ListPoint[0]++;
                            score.Text = StaticObjects.currentUser.ListPoint[0].ToString();
                        }
                        else
                        {
                            media.Source = StaticLink.SoundWrongUri;
                        }
                        media.Play();
                        //Nếu vẫn còn item ở flipview thì chuyển sang cái tiếp theo
                        //Nếu hết tthì
                        //Nếu chưa đến tab cuối
                        //chuyển Grid và Flip sang tab tiếp theo
                        //Nếu đến tab cuối rồi thì hiển thị điểm và thoát ra
                        if (f.SelectedIndex < f.Items.Count - 1)
                        {
                            f.SelectedIndex++;
                        }
                        else
                        {
                            if (groupFlipView.SelectedIndex < groupFlipView.Items.Count - 1)
                            {
                                await StaticObjects.userControll.SaveDataAsync();
                                groupFlipView.SelectedIndex++;
                                groupGridView.SelectedIndex++;
                            }
                            else
                            {
                                await ShowFinishAsync();
                            }
                        }
                  
                    }
                    g.SelectedItem = null;
                }
                else
                {
                    Debug.WriteLine(">>>>>> NULL");
                }

            }

        }

  



   
      
      
        public async Task ShowFinishAsync()
        {
            media.Stop();
            border.Visibility = Windows.UI.Xaml.Visibility.Visible;
            txtHuongDan.Visibility = Windows.UI.Xaml.Visibility.Visible;
            okButton.Visibility = Windows.UI.Xaml.Visibility.Visible;

            isStop = true;
            txtHuongDan.Text = "Your point is " + StaticObjects.currentUser.ListPoint[0];



            int k = StaticObjects.stageControll.ListStage[0].TinhSao(StaticObjects.currentUser.ListPoint[0]);
            for (int i = 1; i <= k; i++)
            {
                Image x = new Image();
                x.Height = 100;
                x.Width = 100;
                x.Source = new BitmapImage(StaticLink.GoldStarUri);
                stackStar.Children.Add(x);
            }


            for (int i = 1; i <= (3 -k); i++)
            {
                Image x = new Image();
                x.Height = 100;
                x.Width = 100;
                x.Source = new BitmapImage(StaticLink.BlackStarUri);
                stackStar.Children.Add(x);
            }

            await StaticObjects.userControll.SaveDataAsync();

        }

        private async void selectGridView(object sender, SelectionChangedEventArgs e)
        {
            if (groupGridView!= null && groupFlipView != null)
            {
                if (groupGridView.SelectedIndex != groupFlipView.SelectedIndex)
                {
                    await StaticObjects.userControll.SaveDataAsync();
                    groupGridView.SelectedIndex = groupFlipView.SelectedIndex;
                }
            }
        }

        private void clickHelp(object sender, RoutedEventArgs e)
        {
            helpButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            helpImage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Stage_1.thisPage.Frame.GoBack();
        }

    

    



    }
}
