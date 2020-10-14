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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataBase;
using MySql.Data.MySqlClient;

namespace задачка_на_экзамен
{
    interface ILetter
    {
        void Fst();
        void Scnd();
        void Thrt();
    }
    class InterfaceShow: ILetter
    {
        public void Fst()
        {
            MessageBox.Show("Поздравляю","1/3");
        }
        public void Scnd()
        {
            MessageBox.Show("Вы открыли самое удобное и не надоедливое письмо","2/3");
        }
        public void Thrt()
        {
            MessageBox.Show("В котором реализован интерфейс","3/3");
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DBInitialize();
            DGrefresh();
        }

        private string dbName = "Bus_station";

        private void DBInitialize()
        {
            DBInteractive db = new DBInteractive();
            db.ExecuteCommand($"CREATE DATABASE IF NOT EXISTS `{dbName}`");
            db = new DBInteractive(dbName);
            db.ExecuteCommand("CREATE TABLE IF NOT EXISTS `Tickets` (`id` INT(11) NOT NULL primary key AUTO_INCREMENT UNIQUE,`Route_ID` INT(11),`Cashier_ID` INT(11))");
            db.ExecuteCommand("CREATE TABLE IF NOT EXISTS `Cashiers` (`id` INT(11) NOT NULL primary key AUTO_INCREMENT UNIQUE,`Cashier_FIO` VARCHAR(150))");
            db.ExecuteCommand("CREATE TABLE IF NOT EXISTS `Routes` (`id` INT(11) NOT NULL primary key AUTO_INCREMENT UNIQUE, `Departure` VARCHAR(150),`Arrival` VARCHAR(150),`Departure_Time` TIME, `Arrival_Time` TIME, `Cost` INT(11))");
        }
        private void DGrefresh()
        {
            dataGrid1.ItemsSource = null;
            DBInteractive db = new DBInteractive(dbName);
            string IJ1 = $"INNER JOIN `Routes` ON `Tickets`.`Route_ID`=`Routes`.`id`";
            string IJ2 = $"INNER JOIN `Cashiers` ON `Tickets`.`Cashier_id`=`Cashiers`.`id`";
            dataGrid1.ItemsSource = db.DGFill($"SELECT `Tickets`.`id`,`Routes`.`Departure`,`Routes`.`Arrival`, `Routes`.`Departure_Time`,`Routes`.`Arrival_Time`,`Routes`.`Cost`,`Cashiers`.`Cashier_FIO` FROM `{dbName}`.`Tickets` {IJ1} {IJ2}");
        }
        private void EditCashiers_Click(object sender, RoutedEventArgs e)
        {
            окно_сотрудников form = new окно_сотрудников();
            this.Hide();
            dataGrid1.ItemsSource = null;
            form.ShowDialog();
            this.Show();
            DGrefresh();
        }
        private void Edit_Routes(object sender, RoutedEventArgs e)
        {
            окно_рейсов form = new окно_рейсов();
            this.Hide();
            form.ShowDialog();
            this.Show();
            DGrefresh();
        }

        private bool isContinue()
        {
            DBInteractive db = new DBInteractive(dbName);
            using (MySqlDataReader reader = db.ExecuteReader($"SELECT COUNT(*) FROM `{dbName}`.`Routes`"))
            {
                reader.Read();
                if(reader.GetInt32(0)==0)
                {
                    MessageBox.Show("Добавьте как минимум один маршрут для продолжения");
                    return false;
                }
                reader.Close();
            }
            using (MySqlDataReader reader = db.ExecuteReader($"SELECT COUNT(*) FROM `{dbName}`.`Cashiers`"))
            {
                reader.Read();
                if (reader.GetInt32(0) == 0)
                {
                    MessageBox.Show("Добавьте как минимум одного сотрудника для продолжения");
                    return false;
                }
                reader.Close();
            }
            return true;
        }
        private void Add_Tickets(object sender, RoutedEventArgs e)
        {
            if (!isContinue())
                return;
            Добавление_билетов form = new Добавление_билетов(-1);
            this.Hide();
            form.ShowDialog();
            this.Show();
            DGrefresh();
        }
        private void Edit_Tickets(object sender, RoutedEventArgs e)
        {
            if(dataGrid1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите любую запись для изменения");
                return;
            }
            object item = dataGrid1.SelectedItem;
            string ID = (dataGrid1.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            Добавление_билетов form = new Добавление_билетов(Convert.ToInt32(ID));
            this.Hide();
            form.ShowDialog();
            this.Show();
            DGrefresh();
        }
        private void Delete_Tickets(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите любую запись для удаления");
                return;
            }
            DBInteractive db = new DBInteractive(dbName);
            object item = dataGrid1.SelectedItem;
            string ID = (dataGrid1.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            db.ExecuteCommand($"DELETE FROM `{dbName}`.`Tickets` WHERE id='{ID}'");
            DGrefresh();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            мое_окно form = new мое_окно();
            form.Show();
        }

        private void dataGrid1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dataGrid1.SelectedIndex = -1;
        }
        private void interfaceButton_Click(object sender, RoutedEventArgs e)
        {
            ILetter obj = new InterfaceShow();
            obj.Fst();
            obj.Scnd();
            obj.Thrt();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите закрыть программу?", "Caution", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
                e.Cancel = true;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)// && e.Key == Key.O) 
                MenuItem_Click(null, null);
        }
    }
}
