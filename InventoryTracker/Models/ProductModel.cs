using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryTracker.Models
{
    public class ProductModel
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public DateTime Date { get; set; }


        public ProductModel()
        {

        }
    }
}
