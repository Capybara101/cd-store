using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Linq;

namespace CD_Store.Models
{
    public class Sale
    {
        public int saleId { get; set; }
        public double total { get; set; }
        public DateTime registerDate { get; set; }
        public DateTime lastUpdate { get; set; }
        public int status { get; set; }
        public List<SaleDetail> saleDetails { get; set; }

        public Sale() { }

        public Sale(double total) {
            this.total = total;
        }

        public Sale(int saleId, double total, DateTime registerDate, int status) {
            this.saleId = saleId;
            this.total = total;
            this.registerDate = registerDate;
            this.status = status;
        }

        public Sale(int saleId, double total, DateTime registerDate, int status, List<SaleDetail> saleDetails)
        {
            this.saleId = saleId;
            this.total = total;
            this.registerDate = registerDate;
            this.status = status;
            this.saleDetails = saleDetails;
        }

        public Sale(int saleId, double total, DateTime registerDate, DateTime lastUpdate, int status) {
            this.saleId = saleId;
            this.total = total;
            this.registerDate = registerDate;
            this.lastUpdate = lastUpdate;
            this.status = status;
        }

        public Sale(int saleId, double total, DateTime registerDate, DateTime lastUpdate, int status, List<SaleDetail> saleDetails)
        {
            this.saleId = saleId;
            this.total = total;
            this.registerDate = registerDate;
            this.status = status;
            this.saleDetails = saleDetails;
        }

        SQLiteClass sqliteClass = new SQLiteClass();
        string dbFile = "URI=file:CD-Store-DB.db";

        public void CreateSaleTable() {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(dbFile))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand(@"CREATE TABLE IF NOT EXISTS sale (saleId INTEGER PRIMARY KEY AUTOINCREMENT, total REAL,
                    registerDate DATE DEFAULT (datetime('now','localtime')), lastUpdate DATE, status INTEGER DEFAULT 1)", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public List<Sale> ReadSaleTable() {
            try
            {
                sqliteClass.CheckSQLite();
                using (SQLiteConnection connection = new SQLiteConnection(dbFile))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("SELECT * FROM sale", connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    List<Sale> allSales = new List<Sale>();
                    while (reader.Read())
                    {
                        Sale sale = new Sale();
                        sale.saleId = int.Parse(reader["saleId"].ToString());
                        sale.total = double.Parse(reader["total"].ToString());
                        sale.registerDate = DateTime.Parse(reader["registerDate"].ToString());
                        if (reader["lastUpdate"].ToString() != "")
                        {
                            sale.lastUpdate = DateTime.Parse(reader["lastUpdate"].ToString());
                        }
                        sale.status = int.Parse(reader["status"].ToString());
                        allSales.Add(sale);
                    }
                    connection.Close();
                    return allSales;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }

        public List<Sale> ReadSaleAndSaleDetailTables()
        {
            try
            {
                sqliteClass.CheckSQLite();
                using (SQLiteConnection connection = new SQLiteConnection(dbFile))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand($@"SELECT * FROM sale", connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    List<Sale> allSales = new List<Sale>();
                    while (reader.Read())
                    {
                        Sale sale = new Sale();
                        sale.saleId = int.Parse(reader["saleId"].ToString());
                        sale.total = double.Parse(reader["total"].ToString());
                        sale.registerDate = DateTime.Parse(reader["registerDate"].ToString());
                        if (reader["lastUpdate"].ToString() != "")
                        {
                            sale.lastUpdate = DateTime.Parse(reader["lastUpdate"].ToString());
                        }
                        sale.status = int.Parse(reader["status"].ToString());
                        sale.saleDetails = new SaleDetail().GetSaleDetails(sale.saleId);
                        allSales.Add(sale);
                    }
                    connection.Close();
                    return allSales;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }

        public void InsertSale(Sale sale, List<SaleDetail> saleDetails) {
            try
            {
                sqliteClass.CheckSQLite();
                using (SQLiteConnection connection = new SQLiteConnection(dbFile))
                {
                    int lastId = 0;
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand($@"INSERT INTO sale (total) VALUES ({sale.total})", connection);
                    command.ExecuteNonQuery();
                    SQLiteCommand command1 = new SQLiteCommand("SELECT seq FROM sqlite_sequence WHERE name = 'sale'", connection);
                    SQLiteDataReader reader = command1.ExecuteReader();
                    while (reader.Read())
                    {
                        lastId = int.Parse(reader["seq"].ToString());
                    }
                    foreach (SaleDetail saleDetail in saleDetails)
                    {
                        saleDetail.saleId = lastId;
                        new SaleDetail().InsertSaleDetail(saleDetail);
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void UpdateSale(int saleId) {
            try
            {
                sqliteClass.CheckSQLite();
                using (SQLiteConnection connection = new SQLiteConnection(dbFile))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand($@"UPDATE sale SET total = 0, lastUpdate = datetime('now','localtime'), status = 0
                    WHERE saleId = {saleId}", connection);
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
