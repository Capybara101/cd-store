using System;
using System.Collections.Generic;
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
    }
}
