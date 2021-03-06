﻿using System;
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
    public partial class окно_рейсов : Window
    {
        private string dbName = "Bus_station";
        public окно_рейсов()
        {
            InitializeComponent();
            DGrefresh();
        }
       
        private void DGrefresh()
        {
            dataGrid1.ItemsSource = null;
            DBInteractive db = new DBInteractive(dbName);
            dataGrid1.ItemsSource = db.DGFill($"SELECT * FROM `{dbName}`.`Routes`");
        }

        private void Add_route(object sender, RoutedEventArgs e)
        {
            Изменение_рейсов form = new Изменение_рейсов(-1);
            form.ShowDialog();
            DGrefresh();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите любую запись для изменения");
                return;
            }
            object item = dataGrid1.SelectedItem;
            string ID = (dataGrid1.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            Изменение_рейсов form = new Изменение_рейсов(Convert.ToInt32(ID));
            form.ShowDialog();
            DGrefresh();
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите любую запись для удаления");
                return;
            }
            object item = dataGrid1.SelectedItem;
            string ID = (dataGrid1.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            DBInteractive db = new DBInteractive(dbName);
            using (MySqlDataReader reader = db.ExecuteReader($"SELECT COUNT(*) FROM `{dbName}`.`Tickets` WHERE `Route_ID`='{ID}'"))
            {
                reader.Read();
                if(reader.GetInt32(0)!=0)
                {
                    MessageBox.Show("Удаление невозможно, т.к. есть билет(ы), где используется удаляемая запись");
                    return;
                }
                reader.Close();
            }
            db.ExecuteCommand($"DELETE FROM `{dbName}`.`Routes` WHERE `id`='{ID}'");
            DGrefresh();
        }

        private void dataGrid1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dataGrid1.SelectedIndex = -1;
        }
    }
}
