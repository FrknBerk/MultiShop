using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.CargoOperationDtos
{
    public class UpdateCargoOperationDto
    {
        public int CargoOpertaionId { get; set; }
        public string Barcode { get; set; }
        public string Decsription { get; set; }
        public DateTime OperatioDate { get; set; }
    }
}
