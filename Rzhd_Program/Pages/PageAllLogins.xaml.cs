using System.Windows.Controls;

namespace Rzhd_Program.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAllLogins.xaml
    /// </summary>
    public partial class PageAllLogins : Page
    {
        Entities entities = new Entities();
        public PageAllLogins()
        {
            InitializeComponent();
            foreach (var zapis in entities.Users)
                dGridlogin.Items.Add(zapis);
        }
    }
}
