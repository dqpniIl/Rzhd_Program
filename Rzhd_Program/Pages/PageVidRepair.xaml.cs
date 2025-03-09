using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Rzhd_Program.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageVidRepair.xaml
    /// </summary>
    public partial class PageVidRepair : Page
    {
        Entities entities = new Entities();
        public PageVidRepair()
        {
            InitializeComponent();
            foreach (var vid in entities.VidRepair)
                lboxVidRepair.Items.Add(vid);
            foreach (var vid in entities.VidWagon)
                comboVidWagon.Items.Add(vid);
            comboVidWagon.SelectedIndex = 0;
        }
        private void lboxVidRepair_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected_vid = lboxVidRepair.SelectedItem as VidRepair;
            if (selected_vid != null)
            {
                comboVidWagon.IsEnabled = false;
                comboVidWagon.SelectedItem = selected_vid.VidWagon;
                tbVidRepair.Text = selected_vid.vid_VidRepair;
            }
            else
                tbVidRepair.Text = "";
        }
        private void comboVidWagon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedVid = comboVidWagon.SelectedItem as VidWagon;
            if (selectedVid != null)
            {
                lboxVidRepair.Items.Clear();
                foreach (var vid in entities.VidRepair)
                    if (vid.VidWagon.Id_VidWagon == selectedVid.Id_VidWagon)
                        lboxVidRepair.Items.Add(vid);
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbVidRepair.Text))
                MessageBox.Show("Заполните поле вида!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                var selectedVid = lboxVidRepair.SelectedItem as VidRepair;
                if (selectedVid == null)
                {
                    var newVid = new VidRepair();
                    newVid.id_VidWagon = (comboVidWagon.SelectedItem as VidWagon).Id_VidWagon;
                    newVid.vid_VidRepair = tbVidRepair.Text;
                    entities.VidRepair.Add(newVid);
                    entities.SaveChanges();
                    lboxVidRepair.Items.Add(newVid);
                    MessageBox.Show("Вид ремонта добавлен", "Добавление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    selectedVid.id_VidWagon = (comboVidWagon.SelectedItem as VidWagon).Id_VidWagon;
                    selectedVid.vid_VidRepair = tbVidRepair.Text;
                    lboxVidRepair.Items.Refresh();
                    MessageBox.Show("Вид ремонта вагона успешно измененён!", "Изменение", MessageBoxButton.OK, MessageBoxImage.Information);
                    entities.SaveChanges();
                }
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var delete_vid = lboxVidRepair.SelectedItem as VidRepair;
            if (delete_vid == null)
            {
                MessageBox.Show("Выберите вид для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var exist = entities.Repair.Any(repair => repair.id_VidRepair == delete_vid.Id_VidRepair);
            if (exist)
            {
                MessageBox.Show("Запись удалить нельзя!\nСуществуют связующие записи с заявками на ремонт вагонов!\nУдалите их и повторите попытку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                lboxVidRepair.SelectedItem = null;
            }
            else
            {
                var rezult = MessageBox.Show("Вы действительно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (rezult == MessageBoxResult.Yes)
                {
                    MessageBox.Show("Запись удалёна!", "Удаление", MessageBoxButton.OK, MessageBoxImage.Question);
                    entities.VidRepair.Remove(delete_vid);
                    entities.SaveChanges();
                    lboxVidRepair.Items.Remove(delete_vid);
                    lboxVidRepair.SelectedItem = null;
                    lboxVidRepair.Items.Refresh();
                    comboVidWagon.IsEnabled = true;
                }
            }
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            comboVidWagon.IsEnabled = true;
            lboxVidRepair.SelectedIndex = -1;
            tbVidRepair.Clear();
            tbVidRepair.Focus();
        }
    }
}
