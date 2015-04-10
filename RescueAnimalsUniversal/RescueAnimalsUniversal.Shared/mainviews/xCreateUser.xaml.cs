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
using Windows.Storage;
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
    public sealed partial class xCreateUser : Page
    {
        public xCreateUser()
        {
            this.InitializeComponent();
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            listAvatar.ItemsSource = StaticObjects.avatarControll.ListAvatar;
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Avatar a = listAvatar.SelectedItem as Avatar;
            if (a != null)
                imgAvatar.Source = new BitmapImage(a.ImageUri);
        }

        private void textChange(object sender, TextChangedEventArgs e)
        {
            if (txtName.Text.Length > 0)
                txtTieuDe.Text = "Hi, " + txtName.Text;
            else
                txtTieuDe.Text = "New User";
        }

        private async void clickCreateUser(object sender, RoutedEventArgs e)
        {
            Avatar avatar = listAvatar.SelectedItem as Avatar;
            if (avatar != null && txtName.Text.Length > 0)
            {
                User u = new User(txtName.Text, avatar.LinkString);
                await StaticObjects.userControll.AddNewUserAsync(u);
                this.Frame.GoBack();
            }
            else
            {

            }
        }
    }
}
