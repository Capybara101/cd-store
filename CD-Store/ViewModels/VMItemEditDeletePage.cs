using CD_Store.Controls;
using CD_Store.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD_Store.ViewModels
{
    class VMItemEditDeletePage : VMBase
    {
        private ObservableCollection<ItemEditDeleteControl> items = new ObservableCollection<ItemEditDeleteControl>();
        public ObservableCollection<ItemEditDeleteControl> Items
        {
            get { return items; }
            set { items = value; }
        }
        public VMItemEditDeletePage()
        {
            foreach (Product prod in new Product().ReadProductTable())
            {
                string prodPath = $@"{Directory.GetCurrentDirectory()}\imagenes\{prod.productId}.jpg";
                if (!File.Exists(prodPath))
                {
                    prodPath = $@"{Directory.GetCurrentDirectory()}\imagenes\product.jpg";
                }
                else
                {
                    prodPath = $@"{Directory.GetCurrentDirectory()}\imagenes\{prod.productId}.jpg";
                }
                items.Add(new ItemEditDeleteControl(new Product { productId = prod.productId, categoryId = prod.categoryId, name = prod.name, unitPrice = prod.unitPrice, productPath = prodPath }));
            }
        }
    }
}
