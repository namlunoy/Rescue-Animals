﻿using RescueAnimalsUniversal.Stages;
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

namespace RescueAnimalsUniversal.stage_3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class xStage_3 : Page
    {

        public List<Animal> x1, y1, x2, y2, x3, y3;
        DispatcherTimer timer = new DispatcherTimer();
        double space = 0;
        private bool isStop = false;

        public xStage_3()
        {
            this.InitializeComponent();


            x1 = StaticObjects.animalControll.GetAnimalsWithSoundA(4);
            y1 = StaticObjects.animalControll.TronAnimalsFromLists(x1);
            x2 = StaticObjects.animalControll.GetAnimalsWithSoundA(9);
            y2 = StaticObjects.animalControll.TronAnimalsFromLists(x2);
            x3 = StaticObjects.animalControll.GetAnimalsWithSoundA(16);
            y3 = StaticObjects.animalControll.TronAnimalsFromLists(x3);

            flipView1.ItemsSource = x1;
            gridView1.ItemsSource = y1;
            flipView2.ItemsSource = x2;
            gridView2.ItemsSource = y2;
            flipView3.ItemsSource = x3;
            gridView3.ItemsSource = y3;

            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += timer_Tick;
            space = xSlider.Width / 150;
            StaticObjects.currentUser.ListPoint[2] = 0;
            score.Text = StaticObjects.currentUser.ListPoint[2].ToString();

        }

        void timer_Tick(object sender, object e)
        {
            if ((slider.Width - space) > 0)
            {
                slider.Width -= space;
                Thickness a = new Thickness(slider.Width, 0, 0, 0);
                snail.Margin = a;
            }
            else
            {
                FlipView f = (groupFlipView.SelectedItem as FlipViewItem).Content as FlipView;
                if (f != null)
                    Next(f);
            }
        }
        public async void Next(FlipView f)
        {
            slider.Width = xSlider.Width;
            if (f.SelectedIndex < f.Items.Count - 1)
                f.SelectedIndex++;
            else
            {
                if (groupFlipView.SelectedIndex < groupFlipView.Items.Count - 1)
                {
                    await StaticObjects.userControll.SaveDataAsync();
                    groupGridView.SelectedIndex++;
                    groupFlipView.SelectedIndex++;
                    if ( groupFlipView.SelectedIndex == 2)
                    {
                        space = xSlider.Width / 170;
                    }
                }
                else
                {
                   await ShowFinishAsync();
                }
            }
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
         
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Stage_3.thisPage.Frame.GoBack();
        }

        private async void selectGridView(object sender, SelectionChangedEventArgs e)
        {
            //Không cho người dùng chuyển tab
            if (groupFlipView != null && groupGridView != null)
            {
                if (groupGridView.SelectedIndex != groupFlipView.SelectedIndex)
                {
                    await StaticObjects.userControll.SaveDataAsync();
                    groupGridView.SelectedIndex = groupFlipView.SelectedIndex;
                }
            }
        }

        private async void selectItem(object sender, SelectionChangedEventArgs e)
        {
            //chọn item bên phải
            if (e != null && e.AddedItems.Count > 0)
            {
                FlipView f = (groupFlipView.SelectedItem as FlipViewItem).Content as FlipView;
                GridView g = (groupGridView.SelectedItem as FlipViewItem).Content as GridView;

                if (f != null && g != null)
                {
                    Animal a = f.SelectedItem as Animal;
                    Animal b = g.SelectedItem as Animal;
                    g.SelectedItem = null;

                    if (a != null && b != null)
                    {
                        if (a.Equals(b))
                        {
                            media.Source = StaticLink.SoundCorrectUri;
                            StaticObjects.currentUser.ListPoint[2]++;
                        }
                        else media.Source = StaticLink.SoundWrongUri;
                        score.Text = StaticObjects.currentUser.ListPoint[2].ToString();
                        media.Play();
                        Next(f);
                    }
                    //f.SelectedItem = null;
                }
            }
        }

        private async Task ShowFinishAsync()
        {
           

            timer.Stop();
            media.Stop();

            border.Visibility = Windows.UI.Xaml.Visibility.Visible;
            txtHuongDan.Visibility = Windows.UI.Xaml.Visibility.Visible;
            okButton.Visibility = Windows.UI.Xaml.Visibility.Visible;

            isStop = true;
            txtHuongDan.Text = "Your point is " + StaticObjects.currentUser.ListPoint[2];
            int k = StaticObjects.stageControll.ListStage[2].TinhSao(StaticObjects.currentUser.ListPoint[2]);
            for (int i = 1; i <=k ; i++)
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

        private void clickOK(object sender, RoutedEventArgs e)
        {
            if (isStop)
                Stage_3.thisPage.Frame.GoBack();
        }

        private void clickSound(object sender, RoutedEventArgs e)
        {
            Animal x = GetAnimalFromFlipView();
            if (x != null)
            {
                media.Source = x.SoundAnimalUri;
                media.Play();
            }
        }

        public Animal GetAnimalFromFlipView()
        {
            if (groupFlipView != null && groupFlipView.SelectedItem != null)
            {
                FlipView f = (groupFlipView.SelectedItem as FlipViewItem).Content as FlipView;
                Animal a = f.SelectedItem as Animal;
                if (a != null)
                    return a;
            }
            return null;
        }

        public Animal GetAnimalFromGridView()
        {
            if (groupGridView != null && groupGridView.SelectedItem != null)
            {
                GridView g = (groupGridView.SelectedItem as FlipViewItem).Content as GridView;
                Animal a = g.SelectedItem as Animal;
                if (a != null)
                    return a;
            }
            return null;
        }

        private void clickHelp(object sender, RoutedEventArgs e)
        {
            helpButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            helpImage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            timer.Start();
        }
    }
}
