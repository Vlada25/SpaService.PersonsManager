using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsManager.DTO.Master
{
    public class MasterForUpdateDto
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public int Rating { get; set; }
    }
}
