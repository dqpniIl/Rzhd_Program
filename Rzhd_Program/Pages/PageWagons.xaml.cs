using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Rzhd_Program.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageWagons.xaml
    /// </summary>
    public partial class PageWagons : Page
    {
        Entities entities = new Entities();
        private List<Wagons> Zapisi;
        private Wagons wagon = new Wagons();
        public PageWagons()
        {
            InitializeComponent();
            LoadingZapisi();
            comboVidWagon.ItemsSource = Entities.GetContext().VidWagon.ToList();
            DataContext = wagon;
            comboVidWagon.SelectionChanged += Filter_comboVidWagon;
            comboVidWagon.SelectedIndex = -1;
        }
        private void LoadingZapisi()
        {
            using (var newContext = new Entities())
            {
                Zapisi = newContext.Wagons.ToList();
                ListViewWagons.ItemsSource = Zapisi;
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Navigation.MainFrame.Navigate(new PageAddWagon((sender as Button).DataContext as Wagons));
        }
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы точно хотите удалить выбранные элементы? {ListViewWagons.SelectedItems.Count}", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (var newContext = new Entities())
                    {
                        foreach (var zapis in ListViewWagons.SelectedItems.Cast<Wagons>().ToList())
                        {
                            var remove_zapis = newContext.Wagons.Find(zapis.Id_Wagon);
                            var exist = entities.Repair.Any(repair => repair.id_Wagon == remove_zapis.Id_Wagon);
                            if (exist)
                            {
                                MessageBox.Show("Запись удалить нельзя!\nСуществуют заявки на ремонт этих вагонов!\nУдалите заявки и повторите попытку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                                ListViewWagons.SelectedIndex = -1;
                                return;
                            }
                            else
                            {
                                if (remove_zapis != null)
                                    newContext.Wagons.Remove(remove_zapis);
                            }
                        }
                        newContext.SaveChanges();
                        MessageBox.Show("Данные удалены!");
                        LoadingZapisi();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void Filter_comboVidWagon(object sender, EventArgs e)
        {
            if (Zapisi == null)
                return;
            if (comboVidWagon.SelectedIndex == -1)
                ListViewWagons.ItemsSource = Zapisi;
            else
            {
                var selectedVidWagon = (comboVidWagon.SelectedItem as VidWagon).vid_VidWagon;
                Zapisi.Clear();
                foreach (var zapis in entities.Wagons)
                    if (zapis.VidWagon.vid_VidWagon == selectedVidWagon)
                        Zapisi.Add(zapis);
                ListViewWagons.ItemsSource = Zapisi;
                Zapisi = entities.Wagons.ToList();
            }
        }
        private void tbsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = tbsearch.Text.ToLower();
            var collectionView = CollectionViewSource.GetDefaultView(ListViewWagons.ItemsSource);
            if (collectionView != null)
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    collectionView.Filter = null;
                }
                else
                {
                    collectionView.Filter = item =>
                    {
                        Wagons zapis_Wagon = item as Wagons;

                        if (zapis_Wagon != null)
                        {
                            return zapis_Wagon.code_Wagon.ToLower().Contains(searchText) || zapis_Wagon.VidWagon.vid_VidWagon.ToLower().Contains(searchText);
                        }
                        return false;
                    };
                }
            }
        }
        private void btnClearCombo_Click(object sender, RoutedEventArgs e)
        {
            tbsearch.Clear();
            comboVidWagon.SelectedIndex = -1;
        }
    }
}
