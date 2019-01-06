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

namespace StoreMongo
{
    /// <summary>
    /// Interaction logic for AddGood.xaml
    /// </summary>
    public partial class AddGood : Window
    {
        public static readonly DependencyProperty NameGoodProperty = DependencyProperty.Register("NameGood", typeof(string), typeof(AddGood), new PropertyMetadata());
        public string NameGood
        {
            get { return (string)GetValue(NameGoodProperty); }
            set { SetValue(NameGoodProperty, value); }
        }

        public static readonly DependencyProperty ValueGoodProperty = DependencyProperty.Register("ValueGood", typeof(double), typeof(AddGood), new PropertyMetadata());
        public double ValueGood
        {
            get { return (double)GetValue(ValueGoodProperty); }
            set { SetValue(ValueGoodProperty, value); }
        }

        public static readonly DependencyProperty TypeGoodProperty = DependencyProperty.Register("TypeGood", typeof(int), typeof(AddGood), new PropertyMetadata());
        public int TypeGood
        {
            get { return (int)GetValue(TypeGoodProperty); }
            set { SetValue(TypeGoodProperty, value); }
        }

        public string DataBase { get; set; }
        public string Collection { get; set; }
        public MongoClient MongodbClient { get; set; }
        public AddGood(string connection, string dataBase, string collection)
        {
            InitializeComponent();
            DataContext = this;
            MongodbClient = new MongoClient(connection);
            DataBase = dataBase;
            Collection = collection;
        }

        private async void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            IMongoDatabase mongodb = MongodbClient.GetDatabase(DataBase);

            var goods = mongodb.GetCollection<BsonDocument>(Collection);

            var document = new BsonDocument
            {
                {"Name", NameGood },
                {"Value", ValueGood.ToString() },
                {"Type", TypeGood.ToString() },
                //{"Name", BsonValue.Create("Peter")},
                //{"lastname", new BsonString("Mbanugo")},
                //{ "subjects", new BsonArray(new[] {"English", "Mathematics", "Physics"}) },
                //{ "class", "JSS 3" },
                //{ "age", 45}
            };

            await goods.InsertOneAsync(document);

            Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
