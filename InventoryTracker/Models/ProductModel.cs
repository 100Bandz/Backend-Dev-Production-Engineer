using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryTracker.Models
{
    public class ProductModel
    {

        [DisplayName("Product Id")]
        public int Id { get; set; }

        [Required]
        [StringLength(40, MinimumLength =3)]
        [DisplayName("Product Name")]
        public string Name { get; set; }

        [DisplayName("Quantity")]
        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        [DisplayName("Price")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Date of Entry")]
        public DateTime Date { get; set; }


        public ProductModel()
        {

        }
    }
}
