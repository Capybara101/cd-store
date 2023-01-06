using CD_Store.Models;
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
    public partial class ItemControl : UserControl, INotifyPropertyChanged
    {

        #region Properties
        private bool isSelected;
        private int productId;
        private double unitPrice;
        private int quantity;
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

        public int Quantity
        {
            get { return (int.TryParse(productQuantity.Text, out int num)) ? quantity : 0; }
            set { quantity = value; OnPropertyChanged("Quantity"); }
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


        #endregion

        #region constructor
        public ItemControl(Product product)
        {
            InitializeComponent();
            UnitPrice = product.unitPrice;
            Quantity = product.quantity;
            ProductID = product.productId;
            ProductName = product.name;

            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(product.productPath, UriKind.Absolute);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            ProductPath = bitmap;
            //ProductPath = new BitmapImage(new Uri(product.productPath, UriKind.RelativeOrAbsolute), new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore));

            ProductPathString = product.productPath;
            isSelected = false;
            DataContext = this;
        }
        #endregion


        #region Methods
        private void btnAumentarCantidad_Click(object sender, RoutedEventArgs e)
        {
            Quantity += 1;
        }

        private void btnQuitarCantidad_Click(object sender, RoutedEventArgs e)
        {
            if (quantity > 0) Quantity -= 1;
        }
        public void Seleccionar()
        {

            Quantity = 1;
            isSelected = true;
            stackOcultar.Visibility = Visibility.Visible;
            borderCantidad.Visibility = Visibility.Visible;

        }
        public void Deseleccionar()
        {
            isSelected = false;
            Quantity = 0;
            stackOcultar.Visibility = Visibility.Hidden;
            borderCantidad.Visibility = Visibility.Hidden;
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
        #endregion
        #region Implementacion de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
            if(quantity <= 0)
            {
                quantity = 0;
                productQuantity.Text = "0";
            }
        }
        #endregion
    }
}
