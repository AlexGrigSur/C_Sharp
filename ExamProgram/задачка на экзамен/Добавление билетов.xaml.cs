using DataBase;
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
using MySql.Data.MySqlClient;
namespace задачка_на_экзамен
{
    public partial class Добавление_билетов : Window
    {
        private string dbName = "Bus_station";
        private int id = -1;
        public Добавление_билетов(int isNewRecord)
        {
            InitializeComponent();

            if (isNewRecord != -1)
            {
                id = isNewRecord;
                editMode();
            }
            else
            {
                depCityCBUpdate();
                CashierCBUpdate();
                arrCityCB.IsEnabled = false;
                depTimeCB.IsEnabled = false;
                arrTimeCB.IsEnabled = false;
                CostCB.IsEnabled = false;
            }
        }

        private void editMode()
        {
            DBInteractive db = new DBInteractive(dbName);
            string IJ1 = "INNER JOIN `Routes` on `Tickets`.`Route_ID`=`Routes`.`id`";
            string IJ2 = "INNER JOIN `Cashiers` on `Tickets`.`Cashier_ID`=`Cashiers`.`id`";
            using (MySqlDataReader reader = db.ExecuteReader($"SELECT `Routes`.`Departure`,`Routes`.`Arrival`,`Routes`.`Departure_Time`,`Routes`.`Arrival_Time`,`Routes`.`Cost`,`Cashiers`.`Cashier_FIO` FROM `{dbName}`.`Tickets` {IJ1} {IJ2} WHERE `Tickets`.`id`='{id}'"))
            {
                reader.Read();
                depCityCBUpdate();
                ComboBoxSearch(depCityCB, reader.GetString(0));
                
                arrCityCBUpdate();
                ComboBoxSearch(arrCityCB, reader.GetString(1));
                
                depTimeCBUpdate();
                ComboBoxSearch(depTimeCB, reader.GetString(2));
                
                arrTimeCBUpdate();
                ComboBoxSearch(arrTimeCB, reader.GetString(3));
                
                CostCBUpdate();
                ComboBoxSearch(CostCB, reader.GetString(4));

                CashierCBUpdate();
                ComboBoxSearch(FIOCB, reader.GetString(5));
            }
        }

        private void ComboBoxSearch(ComboBox CB, string res)
        {
            CB.SelectedIndex = CB.Items.IndexOf(res);
        }
        private bool isNoRepeats(ComboBox CB, string res)
        {
            if (CB.Items.IndexOf(res) == -1)
                return true;
            else
                return false;
        }

        delegate void Updater();
        private void depCityCBUpdate()
        {
            DBInteractive db = new DBInteractive(dbName);
            using (MySqlDataReader reader = db.ExecuteReader($"SELECT `Departure` FROM `{dbName}`.`Routes`"))
            {
                while (reader.Read())
                    if (isNoRepeats(depCityCB, reader.GetString(0)))
                        depCityCB.Items.Add(reader.GetString(0));
                reader.Close();
            }
        }
        private void arrCityCBUpdate()
        {
            DBInteractive db = new DBInteractive(dbName);
            string result1 = (depCityCB.SelectedIndex == -1) ? "" : depCityCB.SelectedValue.ToString();
            MySqlDataReader reader = db.ExecuteReader($"SELECT `Arrival` FROM `{dbName}`.`Routes` WHERE `Departure`='{result1}'");
            arrCityCB.Items.Clear();
            while (reader.Read())
                if (isNoRepeats(arrCityCB, reader.GetString(0)))
                    arrCityCB.Items.Add(reader.GetString(0));
        }
        private void depTimeCBUpdate()
        {
            DBInteractive db = new DBInteractive(dbName);
            string result1 = (depCityCB.SelectedIndex == -1) ? "" : depCityCB.SelectedValue.ToString();
            string result2 = (arrCityCB.SelectedIndex == -1) ? "" : arrCityCB.SelectedValue.ToString();
            MySqlDataReader reader = db.ExecuteReader($"SELECT `Departure_Time` FROM `{dbName}`.`Routes` WHERE `Departure`='{result1}' AND `Arrival`='{result2}'");
            depTimeCB.Items.Clear();
            while (reader.Read())
                if (isNoRepeats(depTimeCB, reader.GetString(0)))
                    depTimeCB.Items.Add(reader.GetString(0));
        }
        private void arrTimeCBUpdate()
        {
            DBInteractive db = new DBInteractive(dbName);
            string result1 = (depCityCB.SelectedIndex == -1) ? "" : depCityCB.SelectedValue.ToString();
            string result2 = (arrCityCB.SelectedIndex == -1) ? "" : arrCityCB.SelectedValue.ToString();
            string result3 = (depTimeCB.SelectedIndex == -1) ? "" : depTimeCB.SelectedValue.ToString();
            MySqlDataReader reader = db.ExecuteReader($"SELECT `Arrival_Time` FROM `{dbName}`.`Routes` WHERE `Departure`='{result1}' AND `Arrival`='{result2}' AND `Departure_Time`='{result3}'");
            arrTimeCB.Items.Clear();
            while (reader.Read())
                if (isNoRepeats(arrTimeCB, reader.GetString(0)))
                    arrTimeCB.Items.Add(reader.GetString(0));
        }
        private void CostCBUpdate()
        {
            DBInteractive db = new DBInteractive(dbName);
            string result1 = (depCityCB.SelectedIndex == -1) ? "" : depCityCB.SelectedValue.ToString();
            string result2 = (arrCityCB.SelectedIndex == -1) ? "" : arrCityCB.SelectedValue.ToString();
            string result3 = (depTimeCB.SelectedIndex == -1) ? "" : depTimeCB.SelectedValue.ToString();
            string result4 = (arrTimeCB.SelectedIndex == -1) ? "" : arrTimeCB.SelectedValue.ToString();
            MySqlDataReader reader = db.ExecuteReader($"SELECT `Cost` FROM `{dbName}`.`Routes` WHERE `Departure`='{result1}' AND `Arrival`='{result2}' AND `Departure_Time`='{result3}' AND `Arrival_Time`='{result4}'");
            CostCB.Items.Clear();
            while (reader.Read())
                if (isNoRepeats(CostCB, reader.GetString(0)))
                    CostCB.Items.Add(reader.GetString(0));
        }
        private void CashierCBUpdate()
        {
            FIOCB.Items.Clear();
            DBInteractive db = new DBInteractive(dbName);
            using (MySqlDataReader reader = db.ExecuteReader($"SELECT `Cashier_FIO` FROM `{dbName}`.`Cashiers`"))
            {
                while (reader.Read())
                    FIOCB.Items.Add(reader.GetString(0));
            }
        }

        private void depCityCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            arrCityCB.IsEnabled = true;
            Updater upd = arrCityCBUpdate;
            upd();
            depTimeCB.IsEnabled = false;
            arrTimeCB.IsEnabled = false;
            CostCB.IsEnabled = false;
        }
        private void arrCityCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            depTimeCB.IsEnabled = true;
            Updater upd = depTimeCBUpdate;
            upd();
            arrTimeCB.IsEnabled = false;
            CostCB.IsEnabled = false;
        }
        private void depTimeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            arrTimeCB.IsEnabled = true;
            Updater upd = arrTimeCBUpdate;
            upd();
            CostCB.IsEnabled = false;
        }
        private void arrTimeCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CostCB.IsEnabled = true;
            Updater upd = CostCBUpdate;
            upd();
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (CostCB.IsEnabled == true && CostCB.SelectedIndex != -1 && FIOCB.SelectedIndex != -1)
            {
                DBInteractive db = new DBInteractive(dbName);
                int Route_ID;
                int Cashier_ID;
                using (MySqlDataReader reader = db.ExecuteReader($"SELECT `id` FROM `{dbName}`.`Routes` WHERE `Departure`='{depCityCB.SelectedValue.ToString()}' AND `Arrival`='{arrCityCB.SelectedValue.ToString()}' AND `Departure_Time`='{depTimeCB.SelectedValue.ToString()}' AND `Arrival_Time`='{arrTimeCB.SelectedValue.ToString()}' AND `Cost`='{CostCB.SelectedValue.ToString()}'"))
                {
                    reader.Read();
                    Route_ID = reader.GetInt32(0);
                    reader.Close();
                }
                using (MySqlDataReader reader = db.ExecuteReader($"SELECT `id` FROM `{dbName}`.`Cashiers` WHERE `Cashier_FIO`='{FIOCB.SelectedValue.ToString()}'"))
                {
                    reader.Read();
                    Cashier_ID = reader.GetInt32(0);
                    reader.Close();
                }

                if (id == -1)
                    db.ExecuteCommand($"INSERT INTO `{dbName}`.`Tickets` (`id`,`Route_ID`,`Cashier_ID`) VALUES (NULL,'{Route_ID}','{Cashier_ID}')");
                else
                    db.ExecuteCommand($"UPDATE `{dbName}`.`Tickets` SET `Route_ID`='{Route_ID}',`Cashier_ID`='{Cashier_ID}' WHERE `id`='{id}'");
                this.Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
