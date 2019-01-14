using MongoDB.Bson;
using MongoDB.Driver;
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
using static StoreMongo.AddGood;

namespace StoreMongo
{
    /// <summary>
    /// Interaction logic for UpdateGood.xaml
    /// </summary>
    public partial class UpdateGood : Window
    {
        public static readonly DependencyProperty CurrentTypeProperty = DependencyProperty.Register("CurrentType", typeof(Good_types), typeof(UpdateGood), new PropertyMetadata(Good_types.Default));
        public Good_types CurrentType
        {
            get { return (Good_types)GetValue(CurrentTypeProperty); }
            set { SetValue(CurrentTypeProperty, value); }
        }

        public static readonly DependencyProperty NameGoodProperty = DependencyProperty.Register("NameGood", typeof(string), typeof(UpdateGood), new PropertyMetadata());
        public string NameGood
        {
            get { return (string)GetValue(NameGoodProperty); }
            set { SetValue(NameGoodProperty, value); }
        }

        public static readonly DependencyProperty ValueGoodProperty = DependencyProperty.Register("ValueGood", typeof(double), typeof(UpdateGood), new PropertyMetadata());
        public double ValueGood
        {
            get { return (double)GetValue(ValueGoodProperty); }
            set { SetValue(ValueGoodProperty, value); }
        }

        public static readonly DependencyProperty ExpDateProperty = DependencyProperty.Register("ExpDate", typeof(DateTime), typeof(UpdateGood), new PropertyMetadata(DateTime.Today));
        public DateTime ExpDate
        {
            get { return (DateTime)GetValue(ExpDateProperty); }
            set { SetValue(ExpDateProperty, value); }
        }

        public static readonly DependencyProperty SizeGoodProperty = DependencyProperty.Register("SizeGood", typeof(int), typeof(UpdateGood), new PropertyMetadata());
        public int SizeGood
        {
            get { return (int)GetValue(SizeGoodProperty); }
            set { SetValue(SizeGoodProperty, value); }
        }

        public static readonly DependencyProperty AlcoProperty = DependencyProperty.Register("Alco", typeof(int), typeof(UpdateGood), new PropertyMetadata());
        public int Alco
        {
            get { return (int)GetValue(AlcoProperty); }
            set { SetValue(AlcoProperty, value); }
        }

        public string DataBase { get; set; }
        public ObjectId CurrentId { get; set; }
        public string Collection { get; set; }
        public MongoClient MongodbClient { get; set; }
        public BsonDocument bsonDocument { get; set; }

        public UpdateGood(string connection, string dataBase, string collection, ObjectId currentId)
        {
            InitializeComponent();
            DataContext = this;
            MongodbClient = new MongoClient(connection);
            DataBase = dataBase;
            Collection = collection;
            CurrentId = currentId;
        }

        private async void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(NameGood))
                {
                    IMongoDatabase mongodb = MongodbClient.GetDatabase(DataBase);

                    var goods = mongodb.GetCollection<BsonDocument>(Collection);

                    var document = new BsonDocument();
                    switch (CurrentType)
                    {
                        case Good_types.Default:
                            document["Name"] = NameGood;
                            document["Value"] = ValueGood;
                            document["Type"] = CurrentType;
                            break;
                        case Good_types.Prom:
                            document["Name"] = NameGood;
                            document["Value"] = ValueGood;
                            document["Type"] = CurrentType;
                            document["SizeGood"] = SizeGood;
                            break;
                        case Good_types.Prod:
                            document["Name"] = NameGood;
                            document["Value"] = ValueGood;
                            document["Type"] = CurrentType;
                            document["ExpDate"] = ExpDate;
                            break;
                        case Good_types.Alkogol:
                            document["Name"] = NameGood;
                            document["Value"] = ValueGood;
                            document["Type"] = CurrentType;
                            document["ExpDate"] = ExpDate;
                            document["Alco"] = Alco;
                            break;
                    }
                    await goods.ReplaceOneAsync(new BsonDocument("_id", CurrentId), document);
                }
            }
            catch
            {

            }
            finally
            {
                Close();
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MongodbClient is MongoClient mc)
                {
                    IMongoDatabase mongodb = mc.GetDatabase(DataBase);

                    var goodsBson = mongodb.GetCollection<BsonDocument>(Collection);
                    var filter = new BsonDocument();
                    var allgoods = goodsBson.Find(filter).ToList();
                    bsonDocument = allgoods.Where(r => (ObjectId)r["_id"] == CurrentId).FirstOrDefault();
                    CurrentType = (Good_types)(int) bsonDocument["Type"];

                    //var document = new BsonDocument();
                    switch (CurrentType)
                    {
                        case Good_types.Default:
                            NameGood = bsonDocument["Name"].ToString();
                            ValueGood = (double)bsonDocument["Value"];
                            break;
                        case Good_types.Prom:
                            NameGood = bsonDocument["Name"].ToString();
                            ValueGood = (double)bsonDocument["Value"];
                            SizeGood = (int)bsonDocument["SizeGood"];
                            break;
                        case Good_types.Prod:
                            NameGood = bsonDocument["Name"].ToString();
                            ValueGood = (double)bsonDocument["Value"];
                            ExpDate = (DateTime)bsonDocument["ExpDate"];
                            break;
                        case Good_types.Alkogol:
                            NameGood = bsonDocument["Name"].ToString();
                            ValueGood = (double)bsonDocument["Value"];
                            ExpDate = (DateTime)bsonDocument["ExpDate"];
                            Alco = (int)bsonDocument["Alco"];
                            break;
                    }
                }
            }
            catch
            {

            }
        }
    }
}
