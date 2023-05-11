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

namespace Тестовое_задание
{
    /// <summary>
    /// Логика взаимодействия для Add_prod.xaml
    /// </summary>
    public partial class Add_prod : Window
    {
        DB.dbEntities db = new DB.dbEntities();
        public Add_prod()
        {
            InitializeComponent();
            cb_types.ItemsSource = db.TypesOfProd.ToList();
        }

        private void bt_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void bt_add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DB.TypesOfProd typesOfProd = db.TypesOfProd.SingleOrDefault(b => b.TypeName == cb_types.Text);
                DB.Products product = new DB.Products()
                {
                    ProdName = tb_name.Text,
                    ProdType = typesOfProd.IdType,
                    Status = 1,
                    StatChanged = DateTime.Now,
                    Quantity = Convert.ToInt32(tb_quantity.Text)

                };

                db.Products.Add(product);
                db.SaveChanges();
                MessageBox.Show("Товар добавлен", "Добавление данных", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
