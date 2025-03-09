using System.Windows;
using System.Windows.Controls;

namespace Rzhd_Program.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageRepair.xaml
    /// </summary>
    public partial class PageRepair : Page
    {
        Entities entities = new Entities();
        public PageRepair()
        {
            InitializeComponent();
            foreach (var repair in entities.Repair)
                lboxRepair.Items.Add(repair);
            foreach (var combo in entities.Wagons)
                comboWagon.Items.Add(combo);
            comboVid.IsEnabled = false;
        }
        private void lboxRepair_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_zapis = lboxRepair.SelectedItem as Repair;
            if (selected_zapis != null)
            {
                comboWagon.SelectedItem = selected_zapis.Wagons;
                comboVid.SelectedItem = selected_zapis.VidRepair;
            }
            else
            {
                comboWagon.SelectedIndex = -1;
                comboVid.SelectedIndex = -1;
            }
        }
        private void comboWagon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboVid.Items.Clear();
            if (comboWagon.SelectedItem != null)
            {
                comboVid.IsEnabled = true;
                foreach (var zapis in entities.VidRepair)
                    if ((comboWagon.SelectedItem as Wagons).VidWagon.Id_VidWagon == zapis.id_VidWagon)
                        comboVid.Items.Add(zapis);
                comboVid.SelectedIndex = 0;
            }
            else
                comboVid.IsEnabled = false;
        }
        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var zapis = lboxRepair.SelectedItem as Repair;
            if (comboWagon.SelectedIndex == -1 || comboVid.SelectedIndex == -1)
                MessageBox.Show("Заполни все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (zapis == null) //Проверка для создания нового объекта
                {
                    zapis = new Repair();
                    entities.Repair.Add(zapis);
                    lboxRepair.Items.Add(zapis);
                }
                zapis.id_Wagon = (comboWagon.SelectedItem as Wagons).Id_Wagon; // Изменение данных
                zapis.id_VidRepair = (comboVid.SelectedItem as VidRepair).Id_VidRepair;
                zapis.status_Repair = "Ожидание";//Занесение данных, чтобы не возникало ошибки NOT NULL
                entities.SaveChanges();//Сохранение записей в базе данных
                lboxRepair.Items.Refresh();//Обновление ListBox для просмотра обновленной информации и записей
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var rezult = MessageBox.Show("Вы действительно хотите удалить заявку?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (rezult == MessageBoxResult.No)
                return;
            var delete_zapis = lboxRepair.SelectedItem as Repair;
            if (delete_zapis != null)
            {
                entities.Repair.Remove(delete_zapis);
                comboWagon.SelectedIndex = -1;
                comboVid.SelectedIndex = -1;
                entities.SaveChanges();
                lboxRepair.Items.Remove(delete_zapis);
                MessageBox.Show("Заявка удалёна!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Question);
            }
            else
                MessageBox.Show("Нет удаляемых заявок!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            lboxRepair.SelectedIndex = -1;
            comboWagon.SelectedIndex = -1;
            comboVid.SelectedIndex = -1;
            comboWagon.IsDropDownOpen = true;
        }
    }
}
