using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows;

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

        public SaleDetail() { }

        public SaleDetail(int productId, double unitPrice, int quantity)
        {
            this.saleId = saleId;
            this.productId = productId;
            this.unitPrice = unitPrice;
            this.quantity = quantity;
        }

        public SaleDetail(int saleId, int productId, double unitPrice, int quantity)
        {
            this.saleId = saleId;
            this.productId = productId;
            this.unitPrice = unitPrice;
            this.quantity = quantity;
        }

        public SaleDetail(int saleDetailId, int saleId, int productId, double unitPrice, int quantity, DateTime registerDate, DateTime lastUpdate, int status) {
            this.saleDetailId = saleDetailId;
            this.saleId = saleId;
            this.productId = productId;
            this.unitPrice = unitPrice;
            this.quantity = quantity;
            this.registerDate = registerDate;
            this.lastUpdate = lastUpdate;
            this.status = status;
        }

        SQLiteClass sqliteClass = new SQLiteClass();
        string dbFile = "URI=file:CD-Store-DB.db";

        public void CreateSaleDetailTable() {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public List<SaleDetail> ReadSaleDetailTable(string startingDate, string endingDate, string categoryId)
        {
            try
            {
                sqliteClass.CheckSQLite();
                using (SQLiteConnection connection = new SQLiteConnection(dbFile))
                {
                    connection.Open();
                    if (startingDate == "" || startingDate == null) startingDate = "01/01/0001";
                    if (endingDate == "" || endingDate == null) endingDate = "31/12/3000";
                    SQLiteCommand command = new SQLiteCommand($"SELECT saleDetail.* FROM saleDetail INNER JOIN product ON saleDetail.productId = product.productId " +
                            $"WHERE strftime('%d/%m/%Y', saleDetail.registerDate) BETWEEN '{startingDate}' AND '{endingDate}'", connection);
                    if (categoryId != "")
                    {
                        command = new SQLiteCommand($"SELECT saleDetail.* FROM saleDetail INNER JOIN product ON saleDetail.productId = product.productId " +
                            $"WHERE product.categoryId = {categoryId} AND strftime('%d/%m/%Y', saleDetail.registerDate) BETWEEN '{startingDate}' AND '{endingDate}'", connection);
                    }
                    SQLiteDataReader reader = command.ExecuteReader();
                    List<SaleDetail> allSaleDetails = new List<SaleDetail>();
                    while (reader.Read())
                    {
                        SaleDetail saleDetail = new SaleDetail();
                        saleDetail.saleDetailId = int.Parse(reader["saleDetailId"].ToString());
                        saleDetail.saleId = int.Parse(reader["saleId"].ToString());
                        saleDetail.productId = int.Parse(reader["productId"].ToString());
                        saleDetail.unitPrice = double.Parse(reader["unitPrice"].ToString());
                        saleDetail.quantity = int.Parse(reader["quantity"].ToString());
                        saleDetail.registerDate = DateTime.Parse(reader["registerDate"].ToString());
                        if (reader["lastUpdate"].ToString() != "")
                        {
                            saleDetail.lastUpdate = DateTime.Parse(reader["lastUpdate"].ToString());
                        }
                        saleDetail.status = int.Parse(reader["status"].ToString());
                        allSaleDetails.Add(saleDetail);
                    }
                    connection.Close();
                    return allSaleDetails;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }

        public List<SaleDetail> GetSaleDetails(int saleId)
        {
            try
            {
                sqliteClass.CheckSQLite();
                using (SQLiteConnection connection = new SQLiteConnection(dbFile))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand($@"SELECT * FROM saleDetail WHERE saleId = {saleId}", connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    List<SaleDetail> allSaleDetails = new List<SaleDetail>();
                    while (reader.Read())
                    {
                        SaleDetail saleDetail = new SaleDetail();
                        saleDetail.saleDetailId = int.Parse(reader["saleDetailId"].ToString());
                        saleDetail.saleId = int.Parse(reader["saleId"].ToString());
                        saleDetail.productId = int.Parse(reader["productId"].ToString());
                        saleDetail.unitPrice = double.Parse(reader["unitPrice"].ToString());
                        saleDetail.quantity = int.Parse(reader["quantity"].ToString());
                        saleDetail.registerDate = DateTime.Parse(reader["registerDate"].ToString());
                        if (reader["lastUpdate"].ToString() != "")
                        {
                            saleDetail.lastUpdate = DateTime.Parse(reader["lastUpdate"].ToString());
                        }
                        saleDetail.status = int.Parse(reader["status"].ToString());
                        allSaleDetails.Add(saleDetail);
                    }
                    connection.Close();
                    return allSaleDetails;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }

        public void InsertSaleDetail(SaleDetail saleDetail) {
            try
            {
                sqliteClass.CheckSQLite();
                using (SQLiteConnection connection = new SQLiteConnection(dbFile))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand($@"INSERT INTO saleDetail (saleId, productId, unitPrice, quantity)
                    VALUES ({saleDetail.saleId}, {saleDetail.productId}, {saleDetail.unitPrice}, {saleDetail.quantity})", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void UpdateSaleDetail(int saleDetailId) {
            try
            {
                sqliteClass.CheckSQLite();
                using (SQLiteConnection connection = new SQLiteConnection(dbFile))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand($@"UPDATE saleDetail SET unitPrice = 0, quantity = 0, lastUpdate = datetime('now','localtime'), status = 0
                    WHERE saleDetailId = {saleDetailId}", connection);
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
