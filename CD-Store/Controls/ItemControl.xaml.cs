using CD_Store.Models;
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
using System.Windows.Shapes;

namespace CD_Store.Controls
{
    /// <summary>
    /// Lógica de interacción para ItemControl.xaml
    /// </summary>
    public partial class ItemControl : UserControl
    {
        public ItemControl(Product product)
        {
            InitializeComponent();
            tbNombre.Text = product.name;
            tbPrecio.Text = product.unitPrice.ToString();
            tbCantidad.Text = product.quantity.ToString();

        }

        

        private void btnAumentarCantidad_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnQuitarCantidad_Click(object sender, RoutedEventArgs e)
        {

        }
        public void Seleccionar()
        {

            //ItemMostrar.Cantidad = 1;
            stackOcultar.Visibility = Visibility.Visible;
            borderCantidad.Visibility = Visibility.Visible;
            //tbCantidad.Text = itemMostrar.Cantidad.ToString();
            //ItemsVenta.AddItemVenta(ItemMostrar);
        }
        public void Deseleccionar()
        {
            //ItemMostrar.Cantidad = 0;
            stackOcultar.Visibility = Visibility.Hidden;
            borderCantidad.Visibility = Visibility.Hidden;
            //tbCantidad.Text = itemMostrar.Cantidad.ToString();
            //ItemsVenta.DeleteItem(ItemMostrar.Id);
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
