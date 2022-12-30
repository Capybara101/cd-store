using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CD_Store.Models
{
    public class Category
    {
        public int categoryId { get; set; }
        public string name { get; set; }
        public DateTime registerDate { get; set; }
        public DateTime lastUpdate { get; set; }
        public int status { get; set; }
        public Category(int categoryId, string name, DateTime registerDate, DateTime lastUpdate, int status)
        {
            this.categoryId = categoryId;
            this.name = name;
            this.registerDate = registerDate;
            this.lastUpdate = lastUpdate;
            this.status = status;
        }
    }
}
