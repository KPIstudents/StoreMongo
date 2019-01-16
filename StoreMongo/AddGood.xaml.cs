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

        public static readonly DependencyProperty ExpDateProperty = DependencyProperty.Register("ExpDate", typeof(DateTime), typeof(AddGood), new PropertyMetadata(DateTime.Today));
        public DateTime ExpDate
        {
            get { return (DateTime)GetValue(ExpDateProperty); }
            set { SetValue(ExpDateProperty, value); }
        }

        public static readonly DependencyProperty SizeGoodProperty = DependencyProperty.Register("SizeGood", typeof(int), typeof(AddGood), new PropertyMetadata());
        public int SizeGood
        {
            get { return (int)GetValue(SizeGoodProperty); }
            set { SetValue(SizeGoodProperty, value); }
        }

        public static readonly DependencyProperty AlcoProperty = DependencyProperty.Register("Alco", typeof(int), typeof(AddGood), new PropertyMetadata());
        public int Alco
        {
            get { return (int)GetValue(AlcoProperty); }
            set { SetValue(AlcoProperty, value); }
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
                            document["ExpDate"] = new BsonDateTime(ExpDate);
                            break;
                        case Good_types.Alkogol:
                            document["Name"] = NameGood;
                            document["Value"] = ValueGood;
                            document["Type"] = CurrentType;
                            document["ExpDate"] = new BsonDateTime(ExpDate);
                            document["Alco"] = Alco;
                            break;
                    }

                    await goods.InsertOneAsync(document);
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
