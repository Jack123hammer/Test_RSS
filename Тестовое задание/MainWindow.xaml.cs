using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Тестовое_задание.DB;

namespace Тестовое_задание
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DB.dbEntities db = new DB.dbEntities();
        DB.Products selected_product = null;
        DB.Products selected_product_on_storage = null;
        public MainWindow()
        {
            InitializeComponent();
            Update();

        }

        private void Update()
        {
            DB.dbEntities db = new DB.dbEntities();
            dgInbox.ItemsSource = db.Products.Where(b => b.Status == 1).ToList();
            dgStorage.ItemsSource = db.Products.Where(b => b.Status == 2).ToList();
            dgSold.ItemsSource = db.Products.Where(b => b.Status == 3).ToList();
            dgStat.ItemsSource = db.Products.ToList();
            dp_till.SelectedDate = DateTime.Today.AddDays(1);
            dp_from.SelectedDate = DateTime.Today.AddDays(-30);


        }
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            Add_prod add_Prod = new Add_prod();
            add_Prod.ShowDialog();
            Update();
        }

        private void dgInbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selected_product = (Products)dgInbox.SelectedItem;
            }
            catch (Exception)
            {

                MessageBox.Show("Ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void bt_toStorage_Click(object sender, RoutedEventArgs e)
        {
            DB.Products product = null;
            if (selected_product != null)
            {
                product = db.Products.SingleOrDefault(b => b.IdProduct == selected_product.IdProduct);
            }

            if (product != null)
            {
                product.Status = 2;
                product.StatChanged = DateTime.Now;
                db.SaveChanges();
                Update();
                try
                {

                    MessageBox.Show("Товар отправлен", "Изменение данных", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception)
                {

                    MessageBox.Show("Ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }



        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                DB.Products product = null;
                if (selected_product_on_storage != null)
                {
                    product = db.Products.SingleOrDefault(b => b.IdProduct == selected_product_on_storage.IdProduct);
                }
                if (product != null)
                {
                    product.Status = 3;
                    product.StatChanged = DateTime.Now;
                    db.SaveChanges();
                    Update();
                    MessageBox.Show("Товар продан", "Изменение данных", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgStorage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selected_product_on_storage != null)
            {
                mi_sold.Visibility = Visibility.Hidden;
            }
            else
            {
                mi_sold.Visibility = Visibility.Visible;
            }
            try
            {
                selected_product_on_storage = (Products)dgStorage.SelectedItem;
            }
            catch (Exception)
            {

                MessageBox.Show("Ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void bt_filt_all_Click(object sender, RoutedEventArgs e)
        {
            DB.dbEntities db = new DB.dbEntities();
            dgStat.ItemsSource = db.Products.Where(b => b.StatChanged >= dp_from.SelectedDate && b.StatChanged <= dp_till.SelectedDate).ToList();
        }

        private void bt_filt_inbox_Click(object sender, RoutedEventArgs e)
        {
            DB.dbEntities db = new DB.dbEntities();
            dgStat.ItemsSource = db.Products.Where(b => b.StatChanged >= dp_from.SelectedDate && b.StatChanged <= dp_till.SelectedDate && b.Status == 1).ToList();
        }

        private void bt_filt_storage_Click(object sender, RoutedEventArgs e)
        {
            DB.dbEntities db = new DB.dbEntities();
            dgStat.ItemsSource = db.Products.Where(b => b.StatChanged >= dp_from.SelectedDate && b.StatChanged <= dp_till.SelectedDate && b.Status == 2).ToList();
        }

        private void bt_filt_sold_Click(object sender, RoutedEventArgs e)
        {
            DB.dbEntities db = new DB.dbEntities();
            dgStat.ItemsSource = db.Products.Where(b => b.StatChanged >= dp_from.SelectedDate && b.StatChanged <= dp_till.SelectedDate && b.Status == 3).ToList();
        }
    }
}
