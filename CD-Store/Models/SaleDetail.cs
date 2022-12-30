using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace CD_Store.Models
{
    public class SaleDetail
    {
        public int saleDetailId { get; set; }
        public int saleId { get; set; }
        public int productId { get; set; }
        public double unitPrice { get; set; }
        public int quantity { get; set; }
        public DateTime registerDate { get; set; }
        public DateTime lastUpdate { get; set; }
        public int status { get; set; }

        public SaleDetail()
        {

        }

        public SaleDetail(int saleDetailId, int saleId, int productId, double unitPrice, int quantity, DateTime registerDate, DateTime lastUpdate, int status)
        {
            this.saleDetailId = saleDetailId;
            this.saleId = saleId;
            this.productId = productId;
            this.unitPrice = unitPrice;
            this.quantity = quantity;
            this.registerDate = registerDate;
            this.lastUpdate = lastUpdate;
            this.status = status;
        }

        string dbFile = "URI=file:CD-Store-DB.db";

        public void CreateSaleDetailTable()
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(@"CREATE TABLE IF NOT EXISTS saleDetail (saleDetailId INTEGER PRIMARY KEY AUTOINCREMENT, saleId INTEGER,
                    productId INTEGER, unitPrice REAL, quantity INTEGER, registerDate DATE DEFAULT (datetime('now','localtime')), lastUpdate DATE, status INTEGER DEFAULT 1)", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
