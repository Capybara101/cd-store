using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using CD_Store.Views;

namespace CD_Store.Views
{
    /// <summary>
    /// Lógica de interacción para MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        ViewSales viewSales;
        ItemPage itemPage;
        public MainPage()
        {
            InitializeComponent();
            viewSales = new ViewSales();
            itemPage = new ItemPage();
        }

        private void SalesBttn_Click(object sender, RoutedEventArgs e)
        {
            MainContainer.Children.Clear();
            MainContainer.Children.Add(viewSales);
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            MainContainer.Children.Clear();
            MainContainer.Children.Add(itemPage);
        }
    }
}
