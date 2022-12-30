using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CD_Store.Models;
using System.IO;
using System.Net.NetworkInformation;

namespace CD_Store.Models
{
    public class SQLiteClass
    {
        public void CheckSQLite()
        {
            new Category().CreateCategoryTable();
            new Product().CreateProductTable();
            new Sale().CreateSaleTable();
            new SaleDetail().CreateSaleDetailTable();
        }
    }
}
