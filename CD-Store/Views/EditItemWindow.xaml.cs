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

namespace CD_Store.Views
{
    /// <summary>
    /// Lógica de interacción para EditItemWindow.xaml
    /// </summary>
    public partial class EditItemWindow : Window
    {
        public EditItemWindow(int ProductID, string ProductName, double UnitPrice, string ProductPath)
        {
            InitializeComponent();
            ProductNameEdit.Text = ProductName;
            ProductUnitPriceEdit.Text = UnitPrice.ToString();
            ShowUpdateProductImage.Source = new BitmapImage(new Uri(ProductPath));
        }
    }
}
