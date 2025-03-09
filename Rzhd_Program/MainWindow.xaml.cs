using Rzhd_Program.Pages;
using System.Windows;

namespace Rzhd_Program
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Navigation.MainFrame = MainFrame;
            if (GlobalUser.admin == false)
            {
                MainFrame.Navigate(new PageUserRepair());
                btnWagons.Visibility = Visibility.Collapsed;
                btnAddWagon.Visibility = Visibility.Collapsed;
                btnVidRepair.Visibility = Visibility.Collapsed;
                btnRepair.Visibility = Visibility.Collapsed;
            }
            else
            {
                MainFrame.Navigate(new PageWagons());
                btnUserRepair.Visibility = Visibility.Collapsed;
            }
        }
        private void btnWagons_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageWagons());
        }
        private void btnAddWagon_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageAddWagon(null));
        }
        private void btnVidRepair_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageVidRepair());
        }
        private void btnRepair_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageRepair());
        }
        private void btnUserRepair_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageUserRepair());
        }
        private void btnUchetnyaZapis_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageUchetnyaZapis());
        }
        private void btnAllLogins_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PageAllLogins());
        }
        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            var windowLogin = new WindowLogin();
            windowLogin.Show();
            Window currentWindow = Window.GetWindow(this);
            currentWindow.Close();
        }
    }
}
