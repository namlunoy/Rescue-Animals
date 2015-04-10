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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RescueAnimalsUniversal.mainviews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class xStartPage : Page
    {
        public xStartPage()
        {
            this.InitializeComponent();

            sound.Play();
        }

  
     

        private void ClickQuit(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

    

        private void clickHighScore(object sender, RoutedEventArgs e)
        {
            StartPage.thisPage.Frame.Navigate(typeof(HightScores));

        }

        private void clickStart(object sender, RoutedEventArgs e)
        {
            StartPage.thisPage.Frame.Navigate(typeof(SelectUser)); 
        }
    }
}
