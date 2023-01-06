using CD_Store.Models;
using CD_Store.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CD_Store.Controls
{
    /// <summary>
    /// Lógica de interacción para ItemControl.xaml
    /// </summary>
    public partial class ItemEditDeleteControl : UserControl
    {
        private bool isSelected;
        private int productId;
        private double unitPrice;
        private string productName;
        private BitmapImage productPath;
        private string productPathString;

        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        public double UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }

        public int ProductID
        {
            get { return productId; }
            set { productId = value; }
        }

        public BitmapImage ProductPath
        {
            get { return productPath; }
            set { productPath = value; }
        }

        public string ProductPathString
        {
            get { return productPathString; }
            set { productPathString = value; }
        }

        public ItemEditDeleteControl(Product product)
        {
            InitializeComponent();
            UnitPrice = product.unitPrice;
            ProductID = product.productId;
            ProductName = product.name;

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(product.productPath);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            ProductPath = bitmap;
            //ProductPath = new BitmapImage(new Uri(product.productPath), new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore));

            ProductPathString = product.productPath;
            isSelected = false;
            DataContext = this;
        }


        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            EditItemWindow editItemWindow = new EditItemWindow(ProductID, ProductName, UnitPrice, ProductPathString);
            editItemWindow.ShowDialog();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            var res=MessageBox.Show("¿Está seguro de eliminar el producto?", "Eliminar Producto", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                Product product = new Product();
                product.DeleteProduct(productId);
                this.Visibility = Visibility.Collapsed;
                MessageBox.Show("Producto Eliminado", "Eliminar Producto");
            }
    
        }

        public void Seleccionar()
        {
            isSelected = true;
            stackOcultar.Visibility = Visibility.Visible;
        }

        public void Deseleccionar()
        {
            isSelected = false;
            stackOcultar.Visibility = Visibility.Hidden;
        }

        private void bordeItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (stackOcultar.Visibility == Visibility.Visible)
            {
                Deseleccionar();
            }
            else
            {
                Seleccionar();
            }
        }
    }
}
