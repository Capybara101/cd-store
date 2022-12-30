using CD_Store.Controls;
using CD_Store.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

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
            items.Add(new ItemControl(new Product { productId = 1, categoryId = 1, name = "Helado", unitPrice = 2.5 }));
            items.Add(new ItemControl(new Product { productId = 1, categoryId = 2, name = "Pelicula", unitPrice = 2.5 }));
            items.Add(new ItemControl(new Product { productId = 1, categoryId = 2, name = "Pelicula 5x10", unitPrice = 10 }));
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
