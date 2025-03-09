using Rzhd_Program.Pages;
using System.Windows;

namespace Rzhd_Program
{
    /// <summary>
    /// Логика взаимодействия для WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        public WindowLogin()
        {
            InitializeComponent();
            Navigation.MainFrame = FrameLogin;
            FrameLogin.Navigate(new PageLog());
        }
    }
}
