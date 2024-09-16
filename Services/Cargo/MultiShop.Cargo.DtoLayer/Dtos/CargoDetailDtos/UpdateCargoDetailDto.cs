using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos
{
    public class UpdateCargoDetailDto
    {
        public int CargoDetailId { get; set; }
        public string SenderCustomer { get; set; } //Gönderen Müşteri
        public string ReceiverCustomer { get; set; } //Alıcı Müşteri
        public int Barcode { get; set; }
        public int CargoCompanyId { get; set; }
    }
}
