using CD_Store.Models;
using CD_Store.ViewModels;
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
            Product product = new Product();
            product.productId = ProductID;
            product.name = ProductName;
            product.unitPrice = UnitPrice;
            product.productPath = ProductPath;

            VMEditProduct vm = new VMEditProduct(product);
            this.DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);
        }
    }
}
