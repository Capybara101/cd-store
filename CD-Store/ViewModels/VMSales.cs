using CD_Store.Controls;
using CD_Store.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace CD_Store.ViewModels
{
    class VMSales : VMBase
    {
        #region atributes
        private ObservableCollection<ItemControl> items = new ObservableCollection<ItemControl>();
        private ObservableCollection<ItemControl> itemsSelected;

        public ObservableCollection<ItemControl> ItemsSelected
        {
            get { return itemsSelected; }
            set { itemsSelected = value; OnPropertyChanged("ItemsSelected"); }
        }

        #endregion

        #region Properties
        public ObservableCollection<ItemControl> Items
        {
            get { return items; }
            set { items = value; }
        }
        #endregion

        #region Constructors
        public VMSales()
        {
            foreach (Product prod in new Product().ReadProductTable())
            {
                string prodPath = $@"{Directory.GetCurrentDirectory()}\imagenes\{prod.productId}.jpg";
                if (!File.Exists(prodPath))
                {
                    prodPath = $@"{Directory.GetCurrentDirectory()}\imagenes\product.jpg";
                }
                items.Add(new ItemControl(new Product { productId = prod.productId, categoryId = prod.categoryId, name = prod.name, unitPrice = prod.unitPrice, productPath = prodPath }));
            }
        }
        #endregion

        public ICommand SellCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var numQuery =
                        from item in items
                        where item.IsSelected == true
                        select item;
                    List<SaleDetail> saleDetails = new List<SaleDetail>();
                    double total = 0;
                    string message = "";
                    foreach (var num in numQuery)
                    {
                        if(num.Quantity > 0)
                        {
                            message += "Producto: " + num.ProductName + " - Cantidad: " + num.Quantity + " - Monto: " + num.UnitPrice + " bs.\n";
                            saleDetails.Add(new SaleDetail(num.ProductID, num.UnitPrice, num.Quantity));
                            total += num.Quantity * num.UnitPrice;
                        }
                    }
                    message += $"TOTAL: {total}";
                    if(total > 0)
                    {
                        if (MessageBox.Show(message, "¿Quiere realizar la compra?", MessageBoxButton.YesNo).ToString() == "Yes")
                        {
                            new Sale().InsertSale(new Sale(total), saleDetails);
                            MessageBox.Show("Compra registrada");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debes elegir al menos un producto.", "¿Quiere realizar la compra?", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                });
            }
        }

    }
}
