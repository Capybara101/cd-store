using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using CD_Store.Controls;

namespace CD_Store.ViewModels
{
    class VMSales : VMBase
    {
        private ObservableCollection<ItemControl> items = new ObservableCollection<ItemControl>();

        public ObservableCollection<ItemControl> Items
        {
            get { return items; }
            set { items = value; }
        }
        public VMSales()
        {
            items.Add(new ItemControl());
            items.Add(new ItemControl());
            items.Add(new ItemControl());
            items.Add(new ItemControl());
            items.Add(new ItemControl());
            items.Add(new ItemControl());
            items.Add(new ItemControl());
            items.Add(new ItemControl());
            items.Add(new ItemControl());
            items.Add(new ItemControl());
            items.Add(new ItemControl());
            items.Add(new ItemControl());
            items.Add(new ItemControl());
            items.Add(new ItemControl());

            items.Add(new ItemControl());
        }

    }
}
