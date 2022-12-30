using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows;

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
        public int quantity { get; set; }

        public Product() { }

        public Product(int categoryId, string name, double unitPrice)
        {
            this.categoryId = categoryId;
            this.name = name;
            this.unitPrice = unitPrice;
        }

        public Product(int productId, int categoryId, string name, double unitPrice)
        {
            this.productId = productId;
            this.categoryId = categoryId;
            this.name = name;
            this.unitPrice = unitPrice;
        }

        public Product(int productId, int categoryId, string name, double unitPrice, DateTime registerDate, DateTime lastUpdate, int status) {
            this.productId = productId;
            this.categoryId = categoryId;
            this.name = name;
            this.unitPrice = unitPrice;
            this.registerDate = registerDate;
            this.lastUpdate = lastUpdate;
            this.status = status;
            quantity = 0;
        }

        SQLiteClass sqliteClass = new SQLiteClass();
        string dbFile = "URI=file:CD-Store-DB.db";

        public void CreateProductTable() {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public List<Product> ReadProductTable() {
            try
            {
                sqliteClass.CheckSQLite();
                using (SQLiteConnection connection = new SQLiteConnection(dbFile))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("SELECT * FROM product", connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    List<Product> allProducts = new List<Product>();
                    while (reader.Read())
                    {
                        Product product = new Product();
                        product.productId = int.Parse(reader["productId"].ToString());
                        product.categoryId = int.Parse(reader["categoryId"].ToString());
                        product.name = reader["name"].ToString();
                        product.unitPrice = double.Parse(reader["unitPrice"].ToString());
                        product.registerDate = DateTime.Parse(reader["registerDate"].ToString());
                        if (reader["lastUpdate"].ToString() != "")
                        {
                            product.lastUpdate = DateTime.Parse(reader["lastUpdate"].ToString());
                        }
                        product.status = int.Parse(reader["status"].ToString());
                        allProducts.Add(product);
                    }
                    connection.Close();
                    return allProducts;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }

        public void InsertProduct(Product product) {
            try
            {
                sqliteClass.CheckSQLite();
                using (SQLiteConnection connection = new SQLiteConnection(dbFile))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand($@"INSERT INTO product (categoryId, name, unitPrice) VALUES ({product.categoryId}, '{product.name}', {product.unitPrice})", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void UpdateProduct(Product product) {
            try
            {
                sqliteClass.CheckSQLite();
                using (SQLiteConnection connection = new SQLiteConnection(dbFile))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand($@"UPDATE product SET categoryId = {product.categoryId}, name = '{product.name}', unitPrice = {product.unitPrice},
                    lastUpdate = datetime('now','localtime') WHERE productId = {product.productId}", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void DeleteProduct(int productId) {
            try
            {
                sqliteClass.CheckSQLite();
                using (SQLiteConnection connection = new SQLiteConnection(dbFile))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand($@"UPDATE product SET status = 0 WHERE productId = {productId}", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
