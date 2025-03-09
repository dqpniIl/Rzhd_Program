using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Rzhd_Program.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddWagon.xaml
    /// </summary>
    public partial class PageAddWagon : Page
    {
        Entities entities = new Entities();
        private Wagons wagon = new Wagons();
        BitmapImage bitmap;
        public PageAddWagon(Wagons selectWagon)
        {
            InitializeComponent();
            if (selectWagon != null)
            {
                wagon = selectWagon;
            }
            DataContext = wagon;
            comboVidWagon.ItemsSource = Entities.GetContext().VidWagon.ToList();
            foreach (VidWagon item in comboVidWagon.Items)
                if (item.Id_VidWagon == wagon.id_VidWagon)
                    comboVidWagon.SelectedItem = item;
        }
        private void btnChooseImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if (dlg.ShowDialog() == true && !string.IsNullOrWhiteSpace(dlg.FileName))
                dlg.FileName.ToString();
            wagon.image_Wagon = dlg.FileName;
            string selectedImage = dlg.FileName;
            bitmap = new BitmapImage(new Uri(selectedImage));
            imageWagon.Source = bitmap;
        }
        private void btnSaveWagon_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(wagon.code_Wagon))
                errors.AppendLine("Не введен код вагона");
            if (comboVidWagon.SelectedItem == null)
                errors.AppendLine("Не выбран род вагона");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (wagon.Id_Wagon == 0)
            {
                Entities.GetContext().Wagons.Add(wagon);
                Entities.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены!");
            }
            else
            {
                try
                {
                    var existingWagon = entities.Wagons.FirstOrDefault(w => w.Id_Wagon == wagon.Id_Wagon);
                    if (existingWagon != null)
                    {
                        existingWagon.code_Wagon = wagon.code_Wagon;
                        existingWagon.id_VidWagon = (comboVidWagon.SelectedItem as VidWagon).Id_VidWagon;
                        existingWagon.image_Wagon = wagon.image_Wagon;
                        entities.SaveChanges();
                        Entities.GetContext().SaveChanges();
                        MessageBox.Show("Изменения успешно сохранены!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            Navigation.MainFrame.Navigate(new PageWagons());
        }
    }
}
