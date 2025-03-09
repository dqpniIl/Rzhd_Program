using System.Windows;
using System.Windows.Controls;

namespace Rzhd_Program.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageUserRepair.xaml
    /// </summary>
    public partial class PageUserRepair : Page
    {
        Entities entities = new Entities();
        public PageUserRepair()
        {
            InitializeComponent();
            foreach (var zapis in entities.Repair)
                if (zapis.status_Repair == "Ожидание")
                    dGridRepair.Items.Add(zapis);
        }
        private void BtnPerform_Click(object sender, RoutedEventArgs e)
        {
            var zapis = dGridRepair.SelectedItem as Repair;
            zapis.status_Repair = "Выполнено";
            entities.SaveChanges();
            dGridRepair.Items.Remove(dGridRepair.SelectedItem);
        }
    }
}
