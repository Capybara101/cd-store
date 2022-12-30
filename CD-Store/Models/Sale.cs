using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CD_Store.Models
{
    public class Sale
    {
        public int saleId { get; set; }
        public double total { get; set; }
        public DateTime registerDate { get; set; }
        public DateTime lastUpdate { get; set; }
        public int status { get; set; }
        public Sale(int saleId, double total, DateTime registerDate, DateTime lastUpdate, int status)
        {
            this.saleId = saleId;
            this.total = total;
            this.registerDate = registerDate;
            this.lastUpdate = lastUpdate;
            this.status = status;
        }
    }
}
