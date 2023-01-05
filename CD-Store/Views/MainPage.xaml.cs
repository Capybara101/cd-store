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
using System.Runtime.InteropServices.ComTypes;
using CD_Store.Models;

namespace CD_Store.Views
{
    /// <summary>
    /// Lógica de interacción para MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        ViewSales viewSales;
        ItemPage itemPage;
        ReportsSales reportsSales;
        EditDeleteItem editDeleteItem;
        SQLiteClass sqliteClass = new SQLiteClass();

        public MainPage()
        {
            InitializeComponent();
            viewSales = new ViewSales();
            itemPage = new ItemPage();
            reportsSales = new ReportsSales();
            editDeleteItem = new EditDeleteItem();
            sqliteClass.CheckSQLite();
            Title = "CD STORE";
        }

        private void SalesBttn_Click(object sender, RoutedEventArgs e)
        {
            MainContainer.Children.Clear();
            MainContainer.Children.Add(new ViewSales());
            Title = "Realizar Venta";
        }

        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            MainContainer.Children.Clear();
            MainContainer.Children.Add(new ItemPage());
            Title = "Agregar Producto";
        }

        private void ReportsSales_Click(object sender, RoutedEventArgs e)
        {
            MainContainer.Children.Clear();
            MainContainer.Children.Add(new ReportsSales());
            Title = "Reporte de Ventas";
        }

        private void UpdateDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            MainContainer.Children.Clear();
            MainContainer.Children.Add(editDeleteItem);
            Title = "Editar y Eliminar Producto";
        }
    }
}
