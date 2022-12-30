using System;
using System.Collections.Generic;
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
    }
}
