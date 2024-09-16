using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.EntityLayer.Concrete
{
    public class CargoOperation
    {
        [Key]
        public int CargoOpertaionId { get; set; }
        public string Barcode { get; set; }
        public string Decsription { get; set; }
        public DateTime OperatioDate { get; set; }
    }
}
