using CD_Store.Controls;
using CD_Store.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
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
                items.Add(new ItemControl(new Product { productId = prod.productId, categoryId = prod.categoryId, name = prod.name, unitPrice = prod.unitPrice }));
            }
        }
        #endregion
        private void SayHello()
        {
            Console.WriteLine("Hello");
        }
       
        public ICommand SayHelloCommand
        {
            get
            {
                return new RelayCommand(()=>
                {
                    MessageBox.Show("sadasd");
                });
            }
        }

    }
}
