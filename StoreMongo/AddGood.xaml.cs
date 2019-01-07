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
        public enum Good_types
        {
            Default = 0,
            Prod = 1,
            Prom = 2,
            Alkogol = 3
        }

        public static readonly DependencyProperty CurrentTypeProperty = DependencyProperty.Register("CurrentType", typeof(Good_types), typeof(AddGood), new PropertyMetadata(Good_types.Default));
        public Good_types CurrentType
        {
            get { return (Good_types)GetValue(CurrentTypeProperty); }
            set { SetValue(CurrentTypeProperty, value); }
        }

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
            //ComboBoxType.ItemsSource = Enum.GetValues(typeof(Good_types)).Cast<Good_types>();
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

        private void AddCtrl_Loaded(object sender, RoutedEventArgs e)
        {
            //Height = Row0.Height.Value + Row1.Height.Value + Row2.Height.Value + Row3.Height.Value + Row4.Height.Value;
        }

        private void ComboBoxType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                switch(CurrentType)
                {
                    case Good_types.Default:
                        StackPanelTypeDefault.Visibility = Visibility.Visible;
                        StackPanelTypeProm.Visibility = Visibility.Collapsed;
                        StackPanelTypeProd.Visibility = Visibility.Collapsed;
                        StackPanelTypeAlkogol.Visibility = Visibility.Collapsed;
                        break;
                    case Good_types.Prom:
                        StackPanelTypeDefault.Visibility = Visibility.Collapsed;
                        StackPanelTypeProm.Visibility = Visibility.Visible;
                        StackPanelTypeProd.Visibility = Visibility.Collapsed;
                        StackPanelTypeAlkogol.Visibility = Visibility.Collapsed;
                        break;
                    case Good_types.Prod:
                        StackPanelTypeDefault.Visibility = Visibility.Collapsed;
                        StackPanelTypeProm.Visibility = Visibility.Collapsed;
                        StackPanelTypeProd.Visibility = Visibility.Visible;
                        StackPanelTypeAlkogol.Visibility = Visibility.Collapsed;
                        break;
                    case Good_types.Alkogol:
                        StackPanelTypeDefault.Visibility = Visibility.Collapsed;
                        StackPanelTypeProm.Visibility = Visibility.Collapsed;
                        StackPanelTypeProd.Visibility = Visibility.Collapsed;
                        StackPanelTypeAlkogol.Visibility = Visibility.Visible;
                        break;
                }
            }
            catch
            {

            }
        }
    }
}
