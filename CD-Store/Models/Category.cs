using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows;

namespace CD_Store.Models
{
    public class Category
    {
        public int categoryId { get; set; }
        public string name { get; set; }
        public DateTime registerDate { get; set; }
        public DateTime lastUpdate { get; set; }
        public int status { get; set; }

        public Category() { }

        public Category(string name)
        {
            this.name = name;
        }

        public Category(int categoryId, string name) {
            this.categoryId = categoryId;
            this.name = name;
        }

        public Category(int categoryId, string name, DateTime registerDate, DateTime lastUpdate, int status) {
            this.categoryId = categoryId;
            this.name = name;
            this.registerDate = registerDate;
            this.lastUpdate = lastUpdate;
            this.status = status;
        }

        SQLiteClass sqliteClass = new SQLiteClass();
        string dbFile = "URI=file:CD-Store-DB.db";

        public void CreateCategoryTable() {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(dbFile))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand(@"CREATE TABLE IF NOT EXISTS category (categoryId INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT,
                    registerDate DATE DEFAULT (datetime('now','localtime')), lastUpdate DATE, status INTEGER DEFAULT 1)", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        public ObservableCollection<Category> ReadCategoryTable() {
            try
            {
                sqliteClass.CheckSQLite();
                using (SQLiteConnection connection = new SQLiteConnection(dbFile))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("SELECT * FROM category", connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    ObservableCollection<Category> allCategories = new ObservableCollection<Category>();
                    while (reader.Read())
                    {
                        Category category = new Category();
                        category.categoryId = int.Parse(reader["categoryId"].ToString());
                        category.name = reader["name"].ToString();
                        category.registerDate = DateTime.Parse(reader["registerDate"].ToString());
                        if (reader["lastUpdate"].ToString() != "")
                        {
                            category.lastUpdate = DateTime.Parse(reader["lastUpdate"].ToString());
                        }
                        category.status = int.Parse(reader["status"].ToString());
                        allCategories.Add(category);
                    }
                    connection.Close();
                    return allCategories;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }

        public void InsertCategory() {
            sqliteClass.CheckSQLite();
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand($@"INSERT INTO category (name) VALUES ('{name}')", connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void UpdateCategory(Category category) {
            try
            {
                sqliteClass.CheckSQLite();
                using (SQLiteConnection connection = new SQLiteConnection(dbFile))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand($@"UPDATE category SET name = '{category.name}', lastUpdate = datetime('now','localtime'), status = 1
                    WHERE categoryId = {category.categoryId}", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void DeleteCategory(int categoryId) {
            try
            {
                sqliteClass.CheckSQLite();
                using (SQLiteConnection connection = new SQLiteConnection(dbFile))
                {
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand($@"UPDATE category SET status = 0 WHERE categoryId = {categoryId}", connection);
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
