﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace CD_Store.Models
{
    public class Product
    {
        public int productId { get; set; }
        public int categoryId { get; set; }
        public string name { get; set; }
        public double unitPrice { get; set; }
        public DateTime registerDate { get; set; }
        public DateTime lastUpdate { get; set; }
        public int status { get; set; }

        public Product()
        {

        }

        public Product(int productId, int categoryId, string name, double unitPrice, DateTime registerDate, DateTime lastUpdate, int status)
        {
            this.productId = productId;
            this.categoryId = categoryId;
            this.name = name;
            this.unitPrice = unitPrice;
            this.registerDate = registerDate;
            this.lastUpdate = lastUpdate;
            this.status = status;
        }

        string dbFile = "URI=file:CD-Store-DB.db";

        public void CreateProductTable()
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(@"CREATE TABLE IF NOT EXISTS product (productId INTEGER PRIMARY KEY AUTOINCREMENT, categoryId INTEGER,
                    name TEXT, unitPrice REAL, registerDate DATE DEFAULT (datetime('now','localtime')), lastUpdate DATE, status INTEGER DEFAULT 1)", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
