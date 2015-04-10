using RescueAnimalsUniversal.MainViews;
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
    public sealed partial class xHightScores : Page
    {
        public xHightScores()
        {
            this.InitializeComponent();


            ListHightScore.ItemsSource = StaticObjects.userControll.ListUserSortByScore();
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HightScores.thisPage.Frame.GoBack();
        }


        private async void clickClear(object sender, RoutedEventArgs e)
        {
            await Helper.ResetFile();
            ListHightScore.ItemsSource = null;
            //ListHightScore.Items.Clear();
            StaticObjects.userControll.ListUser.Clear();

        }
    }
}
