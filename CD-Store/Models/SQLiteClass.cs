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
            Category category = new Category();
            Product product = new Product();
            Sale sale = new Sale();
            SaleDetail saleDetail = new SaleDetail();
            if (!File.Exists("CD-Store-DB.db"))
            {
                category.CreateCategoryTable();
                product.CreateProductTable();
                sale.CreateSaleTable();
                saleDetail.CreateSaleDetailTable();
            }
        }
    }
}
