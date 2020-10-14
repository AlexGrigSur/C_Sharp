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
using DataBase;

namespace задачка_на_экзамен
{
    public partial class Добавление_сотрудников : Window
    {
        private string dbName = "Bus_station";
        private int id = -1;
        public Добавление_сотрудников(int isNewRecord) // 1
        {
            InitializeComponent();
            if (isNewRecord != -1)
            {
                id = isNewRecord;
                inputData(isNewRecord);
            }
        }

        private void inputData(int id)
        {
            DBInteractive db = new DBInteractive(dbName);
            MySqlDataReader reader = db.ExecuteReader($"SELECT `Cashier_FIO` FROM `{dbName}`.`Cashiers` WHERE `id`='{id}'");
            reader.Read();
            textBox1.Text = reader.GetString(0);
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if(textBox1.Text.Length==0)
            {
                MessageBox.Show("Введите фио в текстовое поле для продолжения");
                return;
            }
            DBInteractive db = new DBInteractive(dbName);
            if(id==-1)
                db.ExecuteCommand($"INSERT INTO `Cashiers` (`id`,`Cashier_FIO`) VALUES(NULL, '{textBox1.Text}')");
            else
                db.ExecuteCommand($"UPDATE `Cashiers` set `Cashier_FIO`='{textBox1.Text}' WHERE `id`='{id}'");
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
