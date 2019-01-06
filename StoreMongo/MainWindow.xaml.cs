using MongoDB.Driver;
using MongoDB.Bson;
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

namespace StoreMongo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Connection { get; set; }
        public string DataBase { get; set; }
        public string Collection { get; set; }
        public MongoClient mongodbClient { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            //mongodbClient = new MongoClient("mongodb://localhost");
            //IMongoDatabase mongodb = mongodbClient.GetDatabase(DataBase);

            //var goods = mongodb.GetCollection<BsonDocument>(Collection);
            //var allgoods = goods.Find(new BsonDocument()).ToList();
            //DataGridGoods.ItemsSource = allgoods.Select(r => new GoodDocument() { Name = r["Name"].ToString(), Value = double.Parse(r["Value"].ToString()) }).ToList();
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            var settings = new Settings();
            settings.ShowDialog();
            Connection = settings.Connection;
            DataBase = settings.DataBase;
            Collection = settings.Collection;
            mongodbClient = new MongoClient("mongodb://localhost");
        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IMongoDatabase mongodb = mongodbClient.GetDatabase(DataBase);

                var goods = mongodb.GetCollection<BsonDocument>(Collection);
                var allgoods = goods.Find(new BsonDocument()).ToList();
                DataGridGoods.ItemsSource = allgoods.Select(r => new GoodDocument() { Name = r["Name"].ToString(), Value = double.Parse(r["Value"].ToString()), Type = int.Parse(r["Type"].ToString()) }).ToList();
            }
            catch
            {

            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var addGood = new AddGood(Connection, DataBase, Collection);
                addGood.ShowDialog();
            }
            catch
            {

            }
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
