using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsManager.DTO.Client
{
    public class ClientForCreationDto
    {
        public string PhoneNumber { get; set; }
        public Guid UserId { get; set; }
    }
}
