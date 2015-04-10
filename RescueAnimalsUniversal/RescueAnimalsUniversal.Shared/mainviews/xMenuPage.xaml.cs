using RescueAnimalsUniversal.MainViews;
using RescueAnimalsUniversal.Stages;
using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RescueAnimalsUniversal.mainviews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class xMenuPage : Page
    {
        public xMenuPage()
        {
            this.InitializeComponent();
        }
        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MenuPage.thisPage.Frame.GoBack();
        }


        protected  override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            txtName.Text = "Hi " + StaticObjects.currentUser.Name;

            //Đồng bộ các cửa với người dùng
            StaticObjects.stageControll.SyncDataWithUser(StaticObjects.currentUser);
            listStage.ItemsSource = StaticObjects.stageControll.ListStage;
        }

        private void selectStage(object sender, SelectionChangedEventArgs e)
        {
            Stage s = listStage.SelectedItem as Stage;
            if (s != null)
            {
                switch (s.ID)
                {
                    case 1:
                        MenuPage.thisPage.Frame.Navigate(typeof(Stage_1));
                        break;
                    case 2:
                        MenuPage.thisPage.Frame.Navigate(typeof(Stage_2));
                        break;
                    case 3:
                        MenuPage.thisPage.Frame.Navigate(typeof(Stage_3));
                        break;
                    case 4:
                        MenuPage.thisPage.Frame.Navigate(typeof(Stage_4));
                        break;
           
                    case 5:
                        MenuPage.thisPage.Frame.Navigate(typeof(Stage_5));
                        break;
                    case 6:
                        MenuPage.thisPage.Frame.Navigate(typeof(Stage_6));
                        break;
                    case 7:
                        MenuPage.thisPage.Frame.Navigate(typeof(Stage_7));
                        break;
                    case 8:
                        MenuPage.thisPage.Frame.Navigate(typeof(Stage_8));
                        break;
                    case 9:

                        break;
                    default:

                        break;
                }
            }
        }
    }
}
