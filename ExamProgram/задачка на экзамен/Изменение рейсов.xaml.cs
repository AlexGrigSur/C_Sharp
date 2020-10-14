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
using DataBase;
using MySql.Data.MySqlClient;

namespace задачка_на_экзамен
{
    public partial class Изменение_рейсов : Window
    {
        private int id = -1;
        private string dbName = "Bus_station";
        public Изменение_рейсов(int isNewRecord)
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
            MySqlDataReader reader = db.ExecuteReader($"SELECT `Departure`,`Arrival`,`Departure_Time`,`Arrival_Time`,`Cost` FROM `{dbName}`.`Routes` WHERE `id`='{id}'");
            reader.Read();
            textBox1.Text = reader.GetString(0);
            textBox2.Text = reader.GetString(1);
            mask1.Text = reader.GetTimeSpan(2).ToString().Split(':')[0] + reader.GetTimeSpan(2).ToString().Split(':')[1];
            mask2.Text = reader.GetTimeSpan(3).ToString().Split(':')[0] + reader.GetTimeSpan(3).ToString().Split(':')[1];
            Cost.Text = reader.GetString(4);
        }

        private bool maskCheck(string text)
        {
            string[] time = text.Split(':');
            try
            {
                int hours = Convert.ToInt32(time[0]);
                int min = Convert.ToInt32(time[1]);
                if (hours < 24 && min < 60)
                    return true;
                else
                {
                    MessageBox.Show("Некорректный формат времени");
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("Некорректный формат времени");
                return false;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(textBox1.Text.Length==0 || textBox2.Text.Length==0 || !maskCheck(mask1.Text) || !maskCheck(mask2.Text) || Cost.Text.Length==0)
            {
                MessageBox.Show("Заполните все поля");
                return;
            }
            DBInteractive db = new DBInteractive(dbName);
            if (id == -1)
                db.ExecuteCommand($"INSERT INTO `{dbName}`.`Routes` (`id`,`Departure`,`Arrival`,`Departure_Time`,`Arrival_Time`,`Cost`) VALUES (NULL,'{textBox1.Text}','{textBox2.Text}','{mask1.Text}','{mask2.Text}','{Cost.Text}')");
            else
                db.ExecuteCommand($"UPDATE `{dbName}`.`Routes` SET `id`='{textBox1.Text}',`Departure`='{textBox2.Text}',`Arrival`='{mask1.Text}',`Departure_Time`='{mask2.Text}',`Arrival_Time`='{Cost.Text}'");
            this.Close();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            int key = (int)e.Key;
            e.Handled = !(key >= 34 && key <= 43 || key >= 74 && key <= 83 || key == 2);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
