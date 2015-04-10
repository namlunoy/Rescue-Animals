using RescueAnimalsUniversal.Common;
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
    public sealed partial class xSelectUser : Page
    {


        public xSelectUser()
        {
            this.InitializeComponent();

          
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            await StaticObjects.userControll.LoadDataAsync();
            listViewUser.ItemsSource = StaticObjects.userControll.ListUser;
        }

        private void selectUser(object sender, SelectionChangedEventArgs e)
        {
            StaticObjects.currentUser = StaticObjects.userControll.SelectUser((e.AddedItems[0] as User).Name);
            SelectUser.thisPage.Frame.Navigate(typeof(MenuPage));
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SelectUser.thisPage.Frame.GoBack();
        }

        private void clickCreateUser(object sender, RoutedEventArgs e)
        {
            SelectUser.thisPage._xFrame.Navigate(typeof(xCreateUser));
        }

        private void score_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SelectUser.thisPage._xFrame.Navigate(typeof(xCreateUser));
        }

     
    }
}
