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
        public static readonly DependencyProperty CurrentGoodProperty = DependencyProperty.Register("CurrentGood", typeof(GoodDocument), typeof(MainWindow), new PropertyMetadata());
        public GoodDocument CurrentGood
        {
            get { return (GoodDocument)GetValue(CurrentGoodProperty); }
            set { SetValue(CurrentGoodProperty, value); }
        }

        public static readonly DependencyProperty GoodsProperty = DependencyProperty.Register("Goods", typeof(List<GoodDocument>), typeof(MainWindow), new PropertyMetadata());
        public List<GoodDocument> Goods
        {
            get { return (List<GoodDocument>)GetValue(GoodsProperty); }
            set { SetValue(GoodsProperty, value); }
        }

        public string Connection { get; set; }
        public string DataBase { get; set; }
        public string Collection { get; set; }
        public MongoClient MongodbClient { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void UpdateGoods()
        {
            try
            {
                if (MongodbClient is MongoClient mc)
                {
                    IMongoDatabase mongodb = mc.GetDatabase(DataBase);

                    var goods = mongodb.GetCollection<BsonDocument>(Collection);
                    var filter = new BsonDocument();
                    //Goods = goods.Find(filter).ToList();
                    var allgoods = goods.Find(filter).ToList();
                    Goods = allgoods.Select(r => new GoodDocument() {Id = (ObjectId)r["_id"], Name = r["Name"].ToString(), Value = double.Parse(r["Value"].ToString()), Type = int.Parse(r["Type"].ToString()) }).ToList();
                }
            }
            catch
            {

            }
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {  
            try
            {
                var settings = new Settings();
                if (settings.ShowDialog() == true)
                {
                    Connection = settings.StartConnection + settings.Connection;
                    DataBase = settings.DataBase;
                    Collection = settings.Collection;
                    MongodbClient = new MongoClient(Connection);
                }
            }
            catch
            {

            }
        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateGoods();
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
                UpdateGoods();
            }
            catch
            {

            }
        }

        private async void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {            
            try
            {
                if (MongodbClient is MongoClient mc)
                {
                    IMongoDatabase mongodb = mc.GetDatabase(DataBase);

                    var goods = mongodb.GetCollection<BsonDocument>(Collection);
                    var document = new BsonDocument
                    {
                      {"Name", CurrentGood.Name},
                      {"Value", CurrentGood.Value.ToString()},
                      {"Type", CurrentGood.Type.ToString() }
                    };
                    await goods.DeleteOneAsync(document);
                    UpdateGoods();
                }
            }
            catch
            {

            }
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
