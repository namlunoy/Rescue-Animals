using RescueAnimalsUniversal.UserControls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace RescueAnimalsUniversal.Stages
{
    public sealed partial class Stage_8
    {
        int count = 0;

        DispatcherTimer timer = new DispatcherTimer();
        double space = 0;

        void Stage_8_Loaded(object sender, RoutedEventArgs e)
        {
            List<Animal> list = StaticObjects.animalControll.TronAnimalsFromLists(StaticObjects.animalControll.ListAnimal);
            foreach (var a in list)
            {
                QuestItem item = new QuestItem(a, StaticObjects.animalControll.GetAnotherAnimal(a));
                item.CheckedEvent += item_CheckedEvent;
                flipView.Items.Add(item);
            }

            media.Source = StaticLink.SoundCorrectUri;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 5);
            timer.Tick += timer_Tick;
            space = xSlider.Width / 50;

            Sao1Chay.Completed += Sao1Chay_Completed;
            Sao2Chay.Completed += Sao2Chay_Completed;
            Sao3Chay.Completed += Sao3Chay_Completed;
        }

        private void clickStart(object sender, RoutedEventArgs e)
        {
            help.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            timer.Start();
        }

        void Sao3Chay_Completed(object sender, object e)
        {
            okButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        void Sao2Chay_Completed(object sender, object e)
        {
            Sao3Chay.Begin();
        }

         void Sao1Chay_Completed(object sender, object e)
        {
            Sao2Chay.Begin();
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
                await DungGame();
            }
        }



        async void item_CheckedEvent(object sender, QuestItem.KetQuaArg e)
        {

            if (!e.KetQua)
            {
              await  DungGame();
            }
            else
            {
                media.Play();
                await NextAsync();
            }

        }

        private async Task DungGame()
        {


            timer.Stop();
            media.Source = StaticLink.SoundWrongUri;
            media.Play();

            border.Visibility = Windows.UI.Xaml.Visibility.Visible;
            txtHuongDan.Visibility = Windows.UI.Xaml.Visibility.Visible;
            //okButton.Visibility = Windows.UI.Xaml.Visibility.Visible;

            StaticObjects.currentUser.ListPoint[7] = count;
            txtHuongDan.Text = "Your point is " + count;
            int k = StaticObjects.stageControll.ListStage[7].TinhSao(StaticObjects.currentUser.ListPoint[7]);
            switch (k)
            {
                case 0:
                    sao1.Source = new BitmapImage(StaticLink.BlackStarUri);
                    sao2.Source = new BitmapImage(StaticLink.BlackStarUri);
                    sao3.Source = new BitmapImage(StaticLink.BlackStarUri);
                    break;
                case 1:
                    sao1.Source = new BitmapImage(StaticLink.GoldStarUri);
                    sao2.Source = new BitmapImage(StaticLink.BlackStarUri);
                    sao3.Source = new BitmapImage(StaticLink.BlackStarUri);
                    break;
                case 2:
                    sao1.Source = new BitmapImage(StaticLink.GoldStarUri);
                    sao2.Source = new BitmapImage(StaticLink.GoldStarUri);
                    sao3.Source = new BitmapImage(StaticLink.BlackStarUri);
                    break;
                case 3:
                    sao1.Source = new BitmapImage(StaticLink.GoldStarUri);
                    sao2.Source = new BitmapImage(StaticLink.GoldStarUri);
                    sao3.Source = new BitmapImage(StaticLink.GoldStarUri);
                    break;

                default:
                    break;
            }
            Sao1Chay.Begin();
            await StaticObjects.userControll.SaveDataAsync();
        }

        private async Task NextAsync()
        {
            count++;
            score.Text = count.ToString();
            slider.Width = xSlider.Width;
            await StaticObjects.userControll.SaveDataAsync();
            if (flipView.SelectedIndex + 1 < flipView.Items.Count)
                flipView.SelectedIndex++;
            else
              await  DungGame();
        }
        private void clickOK(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (flipView != null && flipView.SelectedItem != null)
            {
                if (flipView.SelectedIndex != count)
                {
                    flipView.SelectedIndex = count;
                }
            }
        }
    }
}
