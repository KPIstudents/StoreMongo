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
    /// Interaction logic for UpdateGood.xaml
    /// </summary>
    public partial class UpdateGood : Window
    {
        public enum Good_types
        {
            Default = 0,
            All = 1,
            Prod = 2,
            Prom = 3,
            Alkogol = 4
        }

        public static readonly DependencyProperty CurrentTypeProperty = DependencyProperty.Register("CurrentType", typeof(Good_types), typeof(AddGood), new PropertyMetadata(Good_types.Default));
        public Good_types CurrentType
        {
            get { return (Good_types)GetValue(CurrentTypeProperty); }
            set { SetValue(CurrentTypeProperty, value); }
        }

        public string DataBase { get; set; }
        public string Collection { get; set; }
        public MongoClient MongodbClient { get; set; }
        public UpdateGood(string connection, string dataBase, string collection)
        {
            InitializeComponent();
            DataContext = this;
            MongodbClient = new MongoClient(connection);
            DataBase = dataBase;
            Collection = collection;
        }


        //private async void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        //{try
        //    {
        //        if (!string.IsNullOrEmpty(NameGood))
        //        {
        //            IMongoDatabase mongodb = MongodbClient.GetDatabase(DataBase);

        //            var goods = mongodb.GetCollection<BsonDocument>(Collection);

        //            var document = new BsonDocument();
        //           switch (CurrentType)
        //            {
        //                case Good_types.Default:
        //                    document["Name"] = NameGood;
        //                    document["Value"] = ValueGood;
        //                    document["Type"] = CurrentType;
        //                    break;
        //                case Good_types.Prom:
        //                    document["Name"] = NameGood;
        //                    document["Value"] = ValueGood;
        //                    document["Type"] = CurrentType;
        //                    document["SizeGood"] = SizeGood;
        //                    break;
        //                case Good_types.Prod:
        //                    document["Name"] = NameGood;
        //                    document["Value"] = ValueGood;
        //                    document["Type"] = CurrentType;
        //                    document["ExpDate"] = ExpDate;
        //                    break;
        //                case Good_types.Alkogol:
        //                   document["Name"] = NameGood;
        //                    document["Value"] = ValueGood;
        //                    document["Type"] = CurrentType;
        //                    document["ExpDate"] = ExpDate;
        //                    document["Alco"] = Alco;
        //                    break;
        //            }

        //            await goods.InsertOneAsync(document);
        //        }
        //    }
        //    catch
        //    {

        //    }
        //    finally
        //    {
        //        Close();
        //    }
        //}


        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
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
                        StackPanelTypeDefault.Visibility = Visibility.Visible;
                        StackPanelTypeProm.Visibility = Visibility.Collapsed;
                        StackPanelTypeProd.Visibility = Visibility.Collapsed;
                        StackPanelTypeAlkogol.Visibility = Visibility.Collapsed;
                        break;
                        // All product
                    case Good_types.All:
                        StackPanelTypeDefault.Visibility = Visibility.Collapsed;
                        StackPanelTypeProm.Visibility = Visibility.Visible;
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
