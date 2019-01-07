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

namespace StoreMongo
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public static readonly DependencyProperty ConnectionProperty = DependencyProperty.Register("Connection", typeof(string), typeof(Settings), new PropertyMetadata("localhost"));
        public string Connection
        {
            get { return (string)GetValue(ConnectionProperty); }
            set { SetValue(ConnectionProperty, value); }
        }

        public static readonly DependencyProperty DataBaseProperty = DependencyProperty.Register("DataBase", typeof(string), typeof(Settings), new PropertyMetadata("Store"));
        public string DataBase
        {
            get { return (string)GetValue(DataBaseProperty); }
            set { SetValue(DataBaseProperty, value); }
        }

        public static readonly DependencyProperty CollectionProperty = DependencyProperty.Register("Collection", typeof(string), typeof(Settings), new PropertyMetadata("Goods"));
        public string Collection
        {
            get { return (string)GetValue(CollectionProperty); }
            set { SetValue(CollectionProperty, value); }
        }

        public string StartConnection { get; set; } = "mongodb://";

        public Settings()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
