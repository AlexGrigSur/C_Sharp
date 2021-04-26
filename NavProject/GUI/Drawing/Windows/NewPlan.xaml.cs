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
using NavProject_Drawing.Structures;
namespace NavProject_Drawing.Windows
{
    /// <summary>
    /// Interaction logic for NewPlan.xaml
    /// </summary>
    public partial class NewPlan : Window
    {
        public NewPlan()
        {
            InitializeComponent();
            GetCountriesSource();
        }

        private void EncryptionComboBoxChanged(bool flag)
        {
            EncryptionLabel.IsEnabled = flag;
            EncryptionTextBox.IsEnabled = flag;
            EncryptionLabelInfo.IsEnabled = flag;
            Visibility vis = (flag) ? Visibility.Visible : Visibility.Hidden;
            EncryptionLabel.Visibility = vis;
            EncryptionTextBox.Visibility = vis;
            EncryptionLabelInfo.Visibility = vis;
        }
        private void EncryptionComboBox_Checked(object sender, RoutedEventArgs e) => EncryptionComboBoxChanged(true);
        private void EncryptionComboBox_Unchecked(object sender, RoutedEventArgs e) => EncryptionComboBoxChanged(false);

        private void GetCountriesSource() => CountryComboBox.ItemsSource = CountriesCitiesEnumTable.GetCountries();
        private void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            CitiesComboBox.ItemsSource = CountriesCitiesEnumTable.GetCities(CountryComboBox.SelectedItem.ToString());
            AdressTextBox.IsEnabled = false;
        }
        private void CitiesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => AdressTextBox.IsEnabled = true;
    }
}
