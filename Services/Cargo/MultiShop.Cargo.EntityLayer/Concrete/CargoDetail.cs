using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.EntityLayer.Concrete
{
    public class CargoDetail
    {
        [Key]
        public int CargoDetailId { get; set; }
        public string SenderCustomer { get; set; } //Gönderen Müşteri
        public string ReceiverCustomer { get; set; } //Alıcı Müşteri
        public int Barcode { get; set; }
        public int CargoCompanyId { get; set; }
        public CargoCompany CargoCompany { get; set; }
    }
}
