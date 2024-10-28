using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.DtoLayer.IdentityDtos.RoleDtos
{
    public class UpdateRoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
