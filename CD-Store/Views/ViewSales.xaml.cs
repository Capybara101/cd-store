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
using CD_Store.ViewModels;

namespace CD_Store.Views
{
    /// <summary>
    /// Lógica de interacción para ViewSales.xaml
    /// </summary>
    public partial class ViewSales : UserControl
    {
        public ViewSales()
        {
            InitializeComponent();
            DataContext = new VMSales();
        }
    }
}
