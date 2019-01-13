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
using static StoreMongo.AddGood;
using System.Collections;

namespace StoreMongo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty CurrentTypeProperty = DependencyProperty.Register("CurrentType", typeof(Good_types), typeof(MainWindow), new PropertyMetadata(Good_types.Default));
        public Good_types CurrentType
        {
            get { return (Good_types)GetValue(CurrentTypeProperty); }
            set { SetValue(CurrentTypeProperty, value); }
        }

        public static readonly DependencyProperty CurrentGoodProperty = DependencyProperty.Register("CurrentGood", typeof(GoodDocument), typeof(MainWindow), new PropertyMetadata());
        public GoodDocument CurrentGood
        {
            get { return (GoodDocument)GetValue(CurrentGoodProperty); }
            set { SetValue(CurrentGoodProperty, value); }
        }

        public static readonly DependencyProperty GoodsProperty = DependencyProperty.Register("Goods", typeof(IList), typeof(MainWindow), new PropertyMetadata());
        public IList Goods
        {
            get { return (IList)GetValue(GoodsProperty); }
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

                    var goodsBson = mongodb.GetCollection<BsonDocument>(Collection);
                    var filter = new BsonDocument();
                    var allgoods = goodsBson.Find(filter).ToList();

                    switch (CurrentType)
                    {
                        case Good_types.Default:
                            Goods = allgoods.Where(r => (Good_types)int.Parse(r["Type"].ToString()) == Good_types.Default).Select(r => new GoodDocument() { Id = (ObjectId)r["_id"], Name = r["Name"].ToString(), Value = (double)r["Value"], Type = (int)r["Type"] }).ToList();
                            break;
                        case Good_types.Prom:
                            Goods = allgoods.Where(r => (Good_types)int.Parse(r["Type"].ToString()) == Good_types.Prom).Select(r => new Prom() { Id = (ObjectId)r["_id"], Name = r["Name"].ToString(), Value = (double)r["Value"], Type = (int)r["Type"], SizeGood = (int)r["SizeGood"] }).ToList();
                            break;
                        case Good_types.Prod:
                            Goods = allgoods.Where(r => (Good_types)int.Parse(r["Type"].ToString()) == Good_types.Prod).Select(r => new Prod() { Id = (ObjectId)r["_id"], Name = r["Name"].ToString(), Value = (double)r["Value"], Type = (int)r["Type"], ExpDate = (DateTime)r["ExpDate"] }).ToList();
                            break;
                        case Good_types.Alkogol:
                            Goods = allgoods.Where(r => (Good_types)int.Parse(r["Type"].ToString()) == Good_types.Alkogol).Select(r => new Alkogol() { Id = (ObjectId)r["_id"], Name = r["Name"].ToString(), Value = (double)r["Value"], Type = (int)r["Type"], ExpDate = (DateTime)r["ExpDate"], Alco = (int)r["Alco"] }).ToList();
                            break;
                    }
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

        //add ButtonUpdate


        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                var updateGood = new UpdateGood(Connection, DataBase, Collection);
                updateGood.ShowDialog();
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
                    var filter = Builders<BsonDocument>.Filter.Eq("_id", CurrentGood.Id);
                    await goods.DeleteOneAsync(filter);
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

        private void ComboBoxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch (CurrentType)
                {
                    case Good_types.Default:
                        break;
                    case Good_types.Prom:
                        break;
                    case Good_types.Prod:
                        break;
                    case Good_types.Alkogol:
                        break;
                }
            }
            catch
            {

            }
        }

    }
}
